using CRUDApi.Models.EmailModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Data
{
    public class ConfirmActionContext : DbContext
    {
        public ConfirmActionContext(DbContextOptions<ConfirmActionContext> options) : base(options)
        { }
        public DbSet<ConfirmActionsModel> ConfirmActionsModels { get; set; }
    }
}
