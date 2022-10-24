using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace UserToken.Models
{
    public class Owner : IdentityUser
    {
        public virtual List<Cat> Cats { get; set; }
    }
}
