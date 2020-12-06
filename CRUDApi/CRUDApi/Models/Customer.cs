using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models
{
    [Table("customer")]
    public class Customer
    {
        [ForeignKey("productId")]
        public int customerId { get; set; }
       
        public string customerName { get; set; }
        public string customerImage { get; set; }
        public string gender { get; set; }
        public string orderDate { get; set; }
        public string customerAddress { get; set; }
        public string customerPhone { get; set; }
        public string customerEmail { get; set; }
        public int status { get; set; }
    }
}
