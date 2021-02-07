using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApplication1.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        [JsonIgnore]
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
