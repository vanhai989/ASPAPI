using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models
{
    // generate class to recieve data from client request
    public class RequestTokenModel
    {
        [Required]
        public string Token { get; set; }
    }
}
