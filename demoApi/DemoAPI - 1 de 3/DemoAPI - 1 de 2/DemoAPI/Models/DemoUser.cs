using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Models
{
    public class DemoUser:IdentityUser
    {
        public virtual List<Cat> Cats { get; set; }
    }
}
