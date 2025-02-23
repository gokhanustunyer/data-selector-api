using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Domain.Entities.DbCredentials
{
    public class DbCredentials : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DbType DbType { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }
    }
}