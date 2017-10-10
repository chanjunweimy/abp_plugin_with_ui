using System;
using System.Collections.Generic;
using Todo.MainProject.Web.Host.Services.Dto;
using System.IO;
using System.Linq;
using Abp.Collections.Extensions;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace Todo.MainProject.Web.Host.Services
{
    public class PluginService : IPluginService
    {
        private readonly List<PluginObject> _pluginObjects;
        private readonly Dictionary<string, IFileProvider> _pluginEmbeddedFileProviders;

        public PluginService(string pluginPath)
        {
            _pluginObjects = new List<PluginObject>();
            _pluginEmbeddedFileProviders = new Dictionary<string, IFileProvider>();
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

        public IFileProvider GetFileProvider(string pluginTitle)
        {
            if (_pluginEmbeddedFileProviders.IsNullOrEmpty())
            {
                foreach (var pluginObject in _pluginObjects)
                {
                    var webCoreDll = Directory.EnumerateFiles(pluginObject.Path, "*.Web.Core.dll").FirstOrDefault();
                    if (webCoreDll == null)
                    {
                        _pluginEmbeddedFileProviders.Add(pluginObject.Title, new NullFileProvider());
                    }
                    else
                    {
                        webCoreDll = webCoreDll.Replace(".dll", "").Trim();
                        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                        var assembly = assemblies.FirstOrDefault(a => a.GetName().Name.Contains(webCoreDll));
                        if (assembly == null)
                        {
                            _pluginEmbeddedFileProviders.Add(pluginObject.Title, new NullFileProvider());
                        }
                        else
                        {
                            _pluginEmbeddedFileProviders.Add(pluginObject.Title, new EmbeddedFileProvider(assembly));
                        }
                        
                    }
                }
            }

            if (!_pluginEmbeddedFileProviders.ContainsKey(pluginTitle))
            {
                return new NullFileProvider();
            }
            return _pluginEmbeddedFileProviders[pluginTitle];
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
