using System.Collections.Generic;
using System.Reflection;
using Todo.MainProject.Web.Host.Services.Dto;

namespace Todo.MainProject.Web.Host.Services
{
    public interface IPluginService
    {
        List<PluginObject> GetPluginObjects();
        bool IsNullService();
    }
}
