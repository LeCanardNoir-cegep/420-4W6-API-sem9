using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserToken.Models;

namespace UserToken.Data
{
    public class UserTokenContext : IdentityDbContext<Owner>
    {
        public UserTokenContext (DbContextOptions<UserTokenContext> options)
            : base(options)
        {
        }

        public DbSet<UserToken.Models.Cat> Cat { get; set; }

        //public DbSet<UserToken.Models.Owner> Owner { get; set; }
    }
}
