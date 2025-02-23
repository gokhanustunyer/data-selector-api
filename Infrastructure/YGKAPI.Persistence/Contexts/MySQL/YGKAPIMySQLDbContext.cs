using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Domain.Entities;
using YGKAPI.Domain.Entities.Auth;
using YGKAPI.Domain.Entities.DbCredentials;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Persistence.Contexts.MySQL
{
    public class YGKAPIMySQLDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<ClientEndpoint> ClientEndpoints { get; set; }
        public DbSet<ClientEndpointMenu> ClientMenus { get; set; }
        public DbSet<APIClient> APIClients { get; set; }
        public DbSet<DbCredentials> DbCredentials { get; set; }
        
        public YGKAPIMySQLDbContext(DbContextOptions options) : base(options)
        {

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.DateCreated = DateTime.Now,
                    EntityState.Modified => data.Entity.DateUpdated = DateTime.Now,
                    _ => DateTime.Now
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
