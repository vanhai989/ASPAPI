using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models
{
    [Table("EmailHistory")]
    public class EmailModel
    {
        [Key]
        public string Subject { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime createDate { get; set; } = DateTime.UtcNow;
        public string body { get; set; }
    }
}
