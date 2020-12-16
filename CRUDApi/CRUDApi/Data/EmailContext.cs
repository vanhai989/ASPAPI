using CRUDApi.Models.EmailModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Data
{
    public class EmailContext : DbContext
    {
        public EmailContext(DbContextOptions<EmailContext> options) : base(options)
        { }
        public DbSet<EmailModel> EmailModels { get; set; }
    }
}
