using E_Commerce.Application.DTOs.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Abstractions.Services.Configuration
{
    public interface IApplicationService
    {
        List<Menu> GetAuthorizeDefinationsEndpoints(Type type);
    }
}
