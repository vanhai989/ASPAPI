using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models.AuthModels
{
    public class AppSetting
    {
        public string ConnectString { get; set; }
        public string SecrectKey { get; set; }
        public string EmailConfiguration { get; set; }
    }
}
