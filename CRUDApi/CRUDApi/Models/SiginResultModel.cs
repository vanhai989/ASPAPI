using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models
{

    // create model to return data to client token from server
    public class SiginResultModel
    {
        public string Token { get; set; }
        public string RefeshToken { get; set; }
    }
}
