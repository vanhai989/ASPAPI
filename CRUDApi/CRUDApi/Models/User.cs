using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string usname { get; set; }
        public string password { get; set; }
        public virtual IList<RefeshToken> RefeshTokens { get; set; }

    }
}
