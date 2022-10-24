using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DemoAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Data
{
    public class DemoAPIContext : IdentityDbContext<DemoUser>
    {
        public DemoAPIContext (DbContextOptions<DemoAPIContext> options)
            : base(options)
        {
        }

        public DbSet<DemoAPI.Models.Cat> Cat { get; set; }
    }
}
