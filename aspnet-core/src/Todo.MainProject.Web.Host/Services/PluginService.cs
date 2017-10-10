using System;
using System.Collections.Generic;
using Todo.MainProject.Web.Host.Services.Dto;
using System.IO;
using Abp.Collections.Extensions;
using Newtonsoft.Json;

namespace Todo.MainProject.Web.Host.Services
{
    public class PluginService : IPluginService
    {
        private readonly List<PluginObject> _pluginObjects;
        private readonly string _pluginPath;

        public PluginService(string pluginPath)
        {
            _pluginObjects = new List<PluginObject>();
            _pluginPath = pluginPath;
            if (pluginPath == null)
            {
                return;
            }

            if (!Directory.Exists(pluginPath))
            {
                return;
            }
            foreach (var file in Directory.EnumerateFiles(pluginPath, "*.json"))
            {
                var contents = File.ReadAllText(file);
                var pluginObject = JsonConvert.DeserializeObject<PluginObject>(contents);
                pluginObject.Path = pluginPath + pluginObject.Path;
                _pluginObjects.Add(pluginObject);
            }
        }

        public string GetPluginDirectory()
        {
            return _pluginPath;
        }

        public List<PluginObject> GetPluginObjects()
        {
            return _pluginObjects;
        }

        public bool IsNullService()
        {
            return this._pluginObjects.IsNullOrEmpty();
        }
    }
}
