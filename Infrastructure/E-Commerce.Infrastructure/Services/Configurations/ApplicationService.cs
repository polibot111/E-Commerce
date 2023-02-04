using E_Commerce.Application.Abstractions.Services.Configuration;
using E_Commerce.Application.CustomAttributes;
using E_Commerce.Application.DTOs.Configuration;
using E_Commerce.Application.Enıums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Services.Configurations
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinationsEndpoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);
            var contollers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));
            List<Menu> menus = new();

            if (contollers != null)
            {
                foreach (var controller in contollers)
                {
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinationAttribute)));

                    if (actions != null)
                    {
                        foreach (var action in actions)
                        {
                            var attributes = action.GetCustomAttributes(true);
                            if (attributes != null)
                            {
                                Menu menu = null;
                                var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == 
                                typeof(AuthorizeDefinationAttribute)) as AuthorizeDefinationAttribute;

                                if (!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                                {
                                    menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                    menus.Add(menu);
                                }
                                    
                                else
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);

                                Application.DTOs.Configuration.Action _action = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitionAttribute.ActionType),
                                    Defination = authorizeDefinitionAttribute.Definition,
                                };

                                var hhtpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;

                                if (hhtpAttribute != null)
                                    _action.HttpType = hhtpAttribute.HttpMethods.First();
                                else
                                    _action.HttpType = HttpMethods.Get;

                                _action.Code = $"{ _action.HttpType}.{ _action.ActionType}.{_action.Defination.Replace(" ","")}";

                                menu.Actions.Add(_action);
                            }
                        }
                    }
                }
            }

            return menus;
        }
    }
}
