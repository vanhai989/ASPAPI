using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models
{
    // form data request from client to generate token
    public class AbtracToken
    {
        [Required]
        public string Usname { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
