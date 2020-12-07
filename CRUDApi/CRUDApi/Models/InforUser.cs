using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models
{
    // form data request from client to generate token
    public class InforUser
    {
        [Required]
        public string usename { get; set; }
        [Required]
        public string password { get; set; }
    }
}
