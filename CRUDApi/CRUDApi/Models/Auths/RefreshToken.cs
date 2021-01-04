using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models.AuthModels
{
    [Table("RefeshToken")]
    public class RefreshToken
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Token { get; set; }
        public string RelaceById { get; set; }
        public DateTime? RevokedDate { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime ExpireDate { get; set; }
        public string CreateByIp { get; set; } // this value know generate token from where ip, avoid hacking
        public bool IsValid => !RevokedDate.HasValue && ExpireDate < DateTime.UtcNow;
    }
}
