using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Models
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual DemoUser DemoUser { get; set; }
    }
}
