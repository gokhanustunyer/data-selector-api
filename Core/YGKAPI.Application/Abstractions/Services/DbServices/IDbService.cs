using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.DTOs.DbTable;
using YGKAPI.Application.DTOs.Table;
using YGKAPI.Domain.Entities.DbCredentials;

namespace YGKAPI.Application.Abstractions.Services.DbServices
{
    public interface IDbService
    {
        Task<List<DbTable>> GetTablesAsync(DbCredentials dbCredentials);
        Task<TableData> GetTableDatasAsync(DbCredentials dbCredentials, string tableName);
        Task<DataTable> GetTableDatasAsDataTableAsync(DbCredentials dbCredentials, string tableName);
        Task<TableData> GetTableDatasBySqlQueryAsync(DbCredentials dbCredentials, string sqlQuery);
    }
}
