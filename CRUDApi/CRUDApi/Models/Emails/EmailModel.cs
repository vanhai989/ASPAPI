using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models.EmailModels
{
    [Table("EmailSender")]
    public class EmailModel
    {
        [Key]
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
