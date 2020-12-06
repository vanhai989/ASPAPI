using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models
{
    [Table("winformProduct")]
    public class Product
    {
        [ForeignKey("productId")]
        public int productId { get; set; }
        public string productName { get; set; }
        public string productImage { get; set; }
        public int productPrice { get; set; }
        public int topSale { get; set; }
    }
}
