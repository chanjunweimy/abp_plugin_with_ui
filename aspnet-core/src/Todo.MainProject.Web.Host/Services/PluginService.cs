using System.Collections.Generic;
using System.IO;
using Abp.Collections.Extensions;
using Newtonsoft.Json;
using Todo.MainProject.Communication.Dto;

namespace Todo.MainProject.Web.Host.Services
{
    public class PluginService : IPluginService
    {
        private readonly List<PluginObject> _pluginObjects;
        private readonly string _pluginPath;

        public PluginService(string pluginPath)
        {
            _pluginObjects = new List<PluginObject>();
            if (pluginPath == null)
            {
                return;
            }
            _pluginPath = pluginPath;

            if (!Directory.Exists(pluginPath))
            {
                return;
            }
            foreach (var file in Directory.EnumerateFiles(pluginPath, "*.json"))
            {
                var contents = File.ReadAllText(file);
                var pluginObject = JsonConvert.DeserializeObject<PluginObject>(contents);
                pluginObject.Path = pluginObject.Path;
                _pluginObjects.Add(pluginObject);
            }
        }

        public string GetPluginPath()
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
