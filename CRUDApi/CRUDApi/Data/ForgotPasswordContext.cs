using CRUDApi.Models.EmailModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Data
{
    public class ForgotPasswordContext : DbContext
    {
        public ForgotPasswordContext(DbContextOptions<ForgotPasswordContext> options) : base(options)
        { }
        public DbSet<ForgotPasswordModel> ForgotPasswordModels { get; set; }
    }
}
