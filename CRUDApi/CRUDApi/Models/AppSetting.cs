using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models
{
    public class AppSetting
    {
        public string ConnectString { get; set; }
        public string secrectKey { get; set; }
        public string EmailConfiguration { get; set; }
    }
}
