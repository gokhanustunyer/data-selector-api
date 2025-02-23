using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.DbServices;
using YGKAPI.Application.DTOs.DbTable;
using YGKAPI.Application.DTOs.Table;
using YGKAPI.Domain.Entities.DbCredentials;

namespace YGKAPI.Persistence.Services.DbServices
{
    public class MySqlService : IMySqlService
    {
        public async Task<List<DbTable>> GetTablesAsync(DbCredentials dbCredentials)
        {
            List<DbTable> tables = new();
            string connectionString = dbCredentials.ConnectionString;
            string databaseName = GetDatabaseNameFromConnectionString(connectionString);

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new Exception("database.connectionStringParseException.message");
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{databaseName}';";

                    // Retrieve all table names first
                    List<string> tableNames = new List<string>();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader tableReader = command.ExecuteReader())
                    {
                        while (tableReader.Read())
                        {
                            tableNames.Add(tableReader["TABLE_NAME"].ToString());
                        }
                    }

                    // Process each table separately
                    foreach (string tableName in tableNames)
                    {
                        int rowCount = GetRowCount(connection, tableName);
                        var columnInfo = GetColumnInfo(connection, databaseName, tableName);

                        tables.Add(new DbTable
                        {
                            Title = tableName,
                            RowCount = rowCount,
                            ColumnCount = columnInfo.Count,
                            Columns = columnInfo
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("database.notFoundException.message");
                }
            }
            return tables;
        }
        public async Task<TableData> GetTableDatasBySqlQueryAsync(DbCredentials dbCredentials, string sqlQuery)
        {
            List<TableRow> rows = new();
            string connectionString = dbCredentials.ConnectionString;
            List<string> columns = new();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    List<string> tableNames = new List<string>();
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (columns.Count == 0)
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    columns.Add(reader.GetName(i));
                                }

                            List<TableRowData> rowDatas = new();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                rowDatas.Add(new()
                                {
                                    ColumnName = reader.GetName(i),
                                    Data = reader.GetValue(i).ToString()
                                });
                            }
                            rows.Add(new()
                            {
                                TableRowDatas = rowDatas
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("database.notFoundException.message");
                }
            }
            return new()
            {
                Columns = columns,
                Rows = rows
            };
        }
        public async Task<TableData> GetTableDatasAsync(DbCredentials dbCredentials, string tableName)
        {
            List<TableRow> rows = new();
            string connectionString = dbCredentials.ConnectionString;
            List<string> columns = new();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM {tableName} LIMIT 100;";

                    List<string> tableNames = new List<string>();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (columns.Count == 0)
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    columns.Add(reader.GetName(i));
                                }

                            List<TableRowData> rowDatas = new();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                rowDatas.Add(new()
                                {
                                    ColumnName = reader.GetName(i),
                                    Data = reader.GetValue(i).ToString()
                                });
                            }
                            rows.Add(new()
                            {
                                TableRowDatas = rowDatas
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("database.notFoundException.message");
                }
            }
            return new()
            {
                Columns = columns,
                Rows = rows
            };
        }
        private static string GetDatabaseNameFromConnectionString(string connectionString)
        {
            try
            {
                var builder = new DbConnectionStringBuilder
                {
                    ConnectionString = connectionString
                };

                if (builder.TryGetValue("Database", out object databaseName) ||
                    builder.TryGetValue("Initial Catalog", out databaseName))
                {
                    return databaseName.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("database.connectionStringParseException.message");
            }
            return null;
        }
        private static int GetRowCount(MySqlConnection connection, string tableName)
        {
            try
            {
                string rowCountQuery = $"SELECT COUNT(*) FROM `{tableName}`;";
                using (MySqlCommand rowCountCommand = new MySqlCommand(rowCountQuery, connection))
                {
                    int rowCount = Convert.ToInt32(rowCountCommand.ExecuteScalar());
                    return rowCount;
                }
            }
            catch
            {
                return -1;
            }
        }
        private static List<string> GetColumnInfo(MySqlConnection connection, string databaseName, string tableName)
        {
            var columnNames = new List<string>();

            try
            {
                string columnQuery = $@"
                    SELECT COLUMN_NAME 
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_SCHEMA = '{databaseName}' AND TABLE_NAME = '{tableName}';";

                using (MySqlCommand columnCommand = new MySqlCommand(columnQuery, connection))
                using (MySqlDataReader columnReader = columnCommand.ExecuteReader())
                {
                    while (columnReader.Read())
                    {
                        columnNames.Add(columnReader["COLUMN_NAME"].ToString());
                    }
                }
            }
            catch
            {
                throw new Exception("database.connectionStringParseException.message");
            }
            return columnNames;
        }
        public async Task<DataTable> GetTableDatasAsDataTableAsync(DbCredentials dbCredentials, string tableName)
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(dbCredentials.ConnectionString))
            {
                string query = $"SELECT * FROM {tableName}";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
    }
}
