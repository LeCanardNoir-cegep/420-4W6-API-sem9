using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserToken.Models
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Owner_Id { get; set; }
        [ForeignKey("Owner_Id")]
        [JsonIgnore]
        public virtual Owner Owner { get; set; }
    }
}
