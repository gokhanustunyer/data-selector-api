using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Configurations;
using YGKAPI.Application.CustomAttributes;
using YGKAPI.Application.Enums;
using YGKAPI.Domain.Entities.Auth;

namespace YGKAPI.Infrastructure.Services.Configurator
{
    public class ApplicationService : IApplicationService
    {
        public List<Application.DTOs.Configuration.Menu> GetAuthorizeDefinitionEndPoints(Type type)
        {
            Assembly assemly = Assembly.GetAssembly(type);
            var controllers = assemly.GetTypes().Where(t => t.IsAssignableTo(typeof(Microsoft.AspNetCore.Mvc.ControllerBase)));
            List<Application.DTOs.Configuration.Menu> menus = new();
            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if (actions != null)
                    {
                        foreach (var action in actions)
                        {
                            var attributes = action.GetCustomAttributes(true);
                            if (attributes != null)
                            {
                                Application.DTOs.Configuration.Menu menu = new();
                                var authAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

                                if (!menus.Any(m => m.Name == authAttribute.Menu))
                                {
                                    menu = new() { Name = authAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                    menu = menus.FirstOrDefault(m => m.Name == authAttribute.Menu);

                                Application.DTOs.Configuration.Action actionDTO = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType), authAttribute.ActionType),
                                    Definition = authAttribute.Definition,
                                };
                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAttribute != null)
                                    actionDTO.HttpType = httpAttribute.HttpMethods.First();
                                else
                                    actionDTO.HttpType = HttpMethods.Get;
                                actionDTO.Code = $"{actionDTO.HttpType}.{actionDTO.ActionType}.{actionDTO.Definition.Replace(" ", "")}";

                                menu.Actions.Add(actionDTO);
                            }
                        }
                    }
                }
            }
            return menus;
        }
    }
}