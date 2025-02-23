using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.DbServices;
using YGKAPI.Application.DTOs.DbTable;
using YGKAPI.Application.DTOs.Table;
using YGKAPI.Domain.Entities.DbCredentials;

namespace YGKAPI.Persistence.Services.DbServices
{
    public class DbService : IDbService
    {
        public Task<DataTable> GetTableDatasAsDataTableAsync(DbCredentials dbCredentials, string tableName)
        {
            throw new NotImplementedException();
        }

        public Task<TableData> GetTableDatasAsync(DbCredentials dbCredentials, string tableName)
        {
            throw new NotImplementedException();
        }

        public Task<TableData> GetTableDatasBySqlQueryAsync(DbCredentials dbCredentials, string sqlQuery)
        {
            throw new NotImplementedException();
        }

        public Task<List<DbTable>> GetTablesAsync(DbCredentials dbCredentials)
        {
            throw new NotImplementedException();
        }
    }
}