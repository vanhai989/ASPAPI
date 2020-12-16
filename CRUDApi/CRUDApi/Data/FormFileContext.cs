using CRUDApi.Models.Images;
using Microsoft.EntityFrameworkCore;

namespace CRUDApi.Data
{
    public class FormFileContext : DbContext
    {
        public FormFileContext(DbContextOptions<FormFileContext> options) : base(options)
        { }
        public DbSet<ImageModel> ImageModels { get; set; }
    }
}
