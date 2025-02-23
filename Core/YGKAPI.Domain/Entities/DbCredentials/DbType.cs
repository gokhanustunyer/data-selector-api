using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Domain.Entities.DbCredentials
{
    public enum DbType
    {
        SqlServer,        // Microsoft SQL Server
        MySql,            // MySQL
        PostgreSql,       // PostgreSQL
        Oracle,           // Oracle Database
        Sqlite,           // SQLite
        Db2,              // IBM Db2
        Sybase,           // Sybase ASE
        Firebird,         // Firebird Database
        Access,           // Microsoft Access (JET/OLEDB)
        Informix,         // IBM Informix
        MariaDb,          // MariaDB (MySQL'in bir türevi)
        NuoDb,            // NuoDB
        Snowflake,        // Snowflake Cloud Data Platform
        Teradata,         // Teradata Database
        Ingres,           // Ingres Database
        Cassandra,        // Apache Cassandra (genellikle CQL ile bağlanılır)
        CosmosDb,         // Azure Cosmos DB (SQL API üzerinden)
        Redshift,         // Amazon Redshift
        Hive,             // Apache Hive
        ElasticSearch,    // ElasticSearch (genelde HTTP tabanlı bağlantı kullanılır)
        H2                // H2 Database (Java tabanlı, ancak ADO.NET sürücüleri mevcut)
    }
}