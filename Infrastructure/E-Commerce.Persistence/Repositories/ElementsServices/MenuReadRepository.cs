using E_Commerce.Application.Repositories.ElementsRepositories;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories.ElementsServices
{
    public class MenuReadRepository : ReadRepository<Menu>,IMenuReadRepository 
    {
        public MenuReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
