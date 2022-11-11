using E_Commerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    public class DesignTimeDbContectFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext>
    {
        public ECommerceAPIDbContext CreateDbContext(string[] args)
        {

            DbContextOptionsBuilder<ECommerceAPIDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<ECommerceAPIDbContext>();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new ECommerceAPIDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
