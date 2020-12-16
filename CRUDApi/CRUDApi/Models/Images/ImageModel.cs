using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Models.Images
{
    [Table("FileDB")]
    public class ImageModel
    {
        [Key]
        public int FormFileID { get; set; }
        public string Usename { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
