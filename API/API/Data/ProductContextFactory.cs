using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class ProductContextFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContext CreateDbContext(string[] args)
        {
            var builler = new DbContextOptionsBuilder<ProductContext>();
            builler.UseSqlServer("Server =DESKTOP-15IPD1K; Database =ASP; Trusted_Connection = True; MultipleActiveResultSets = true");
            return new ProductContext(builler.Options);
        }
    }
}
