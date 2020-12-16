using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models.CustomerModels
{
    [Table("customer")]
    public class Customer
    {
        [ForeignKey("productId")]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerImage { get; set; }
        public string Gender { get; set; }
        public string OrderDate { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public int Status { get; set; }
    }
}
