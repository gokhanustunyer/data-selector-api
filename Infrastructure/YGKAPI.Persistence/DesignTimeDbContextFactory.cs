using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Persistence.Contexts.MySQL;

namespace YGKAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<YGKAPIMySQLDbContext>
    {
        public YGKAPIMySQLDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<YGKAPIMySQLDbContext> dbContextOptionsBuilder = new();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            dbContextOptionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=gokhan949;database=dataselector;", serverVersion);
            return new YGKAPIMySQLDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
