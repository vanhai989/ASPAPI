using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models
{
    [Table("RefeshToken")]
    public class RefeshToken
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string token { get; set; }
        public string relaceById { get; set; }
        public DateTime? revokedDate { get; set; }
        public DateTime createDate { get; set; } = DateTime.UtcNow;
        public string userId { get; set; }
        public virtual User User { get; set; }
        public DateTime expireDate { get; set; }
        public string CreateByIp { get; set; } // this value know generate token from where ip, avoid hacking
        public bool isValid => !revokedDate.HasValue && expireDate < DateTime.UtcNow;
    }
}
