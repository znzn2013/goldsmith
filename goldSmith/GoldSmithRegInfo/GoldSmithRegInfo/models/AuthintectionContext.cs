using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldSmithRegInfo.models
{
    public class AuthintectionContext : IdentityDbContext
    {
        public AuthintectionContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<goldSmithUsers> GoldSmithUsers { get; set; }

    }
}
