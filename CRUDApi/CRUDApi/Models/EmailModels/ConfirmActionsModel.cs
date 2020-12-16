using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models.EmailModels
{
    [Table("FormHtmlConfirmAction")]
    public class ConfirmActionsModel
    {
        [Key]
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

