using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models.AuthModels
{
    [Table("User")]
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Usename { get; set; }
        public string Password { get; set; }
        public virtual IList<RefreshToken> RefeshTokens { get; set; }

    }
}
