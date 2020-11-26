using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    [Table("ProductTable")]
    public class Product
    {
        [ForeignKey("Id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
