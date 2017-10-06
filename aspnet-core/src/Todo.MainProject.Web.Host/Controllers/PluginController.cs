using System.Collections.Generic;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.MainProject.Controllers;
using Todo.MainProject.Web.Host.Services;
using Todo.MainProject.Web.Host.Services.Dto;

namespace Todo.MainProject.Web.Host.Controllers
{
    [Route("api/[controller]")]
    public class PluginController : MainProjectControllerBase
    {
        private readonly IPluginService _pluginService;

        public PluginController(IPluginService pluginService)
        {
            _pluginService = pluginService;
        }

        [HttpGet]
        public List<PluginObject> GetPluginObjectsResult()
        {
            return _pluginService.GetPluginObjects();
        }
    }
}