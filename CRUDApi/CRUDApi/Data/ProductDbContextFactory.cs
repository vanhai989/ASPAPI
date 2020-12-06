using CRUDApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Data
{
    public class ProductDbContextFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContext CreateDbContext(string[] args)
        {
            var builler = new DbContextOptionsBuilder<ProductContext>();
            builler.UseSqlServer("Server=PHAMVANHAI\\SQLEXPRESS;Database=DbAspApi;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new ProductContext(builler.Options);
        }
    }
}
