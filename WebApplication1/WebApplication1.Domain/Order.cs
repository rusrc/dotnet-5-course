using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApplication1.Domain
{
    public class Order
    {
        public int Id { get; set; }

        private DateTime? created = null;
        public DateTime Created
        {
            get => created ?? DateTime.Now;
            set => created = value;
        }

        [JsonIgnore]
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
