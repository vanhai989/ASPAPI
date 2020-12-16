using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models.AuthModels
{
    // form data request from client to generate token
    public class InforUser
    {
        [Required]
        public string Usename { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
