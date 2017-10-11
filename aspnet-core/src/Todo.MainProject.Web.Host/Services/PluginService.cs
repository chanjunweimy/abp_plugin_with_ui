using System;
using System.Collections.Generic;
using Todo.MainProject.Web.Host.Services.Dto;
using System.IO;
using System.Linq;
using System.Reflection;
using Abp.Collections.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Todo.MainProject.Web.Host.Services
{
    public class PluginService : IPluginService
    {
        private readonly List<PluginObject> _pluginObjects;
        private readonly Dictionary<string, Assembly> _pluginAssemblies;
        private readonly Dictionary<string, string> _pluginBaseName;

        public PluginService(string pluginPath)
        {
            _pluginObjects = new List<PluginObject>();
            _pluginAssemblies = new Dictionary<string, Assembly>();
            _pluginBaseName = new Dictionary<string, string>();
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

        public List<byte[]> GetFilesBytes(string pluginTitle)
        {
            var filesBytes = new List<byte[]>();
            var assembly = GetAssembly(pluginTitle);
            if (assembly == null)
            {
                return filesBytes;
            }
            var names = assembly.GetManifestResourceNames();
            foreach (var name in names)
            {
                var mem = new MemoryStream();
                var stream = assembly.GetManifestResourceStream(name);
                stream.CopyTo(mem);
                byte[] fileBytes = mem.ToArray();
            }
            
            return filesBytes;
        }

        protected string GetBaseNameSpace(string pluginTitle)
        {
            InitPluginArrays();

            if (!_pluginBaseName.ContainsKey(pluginTitle))
            {
                return null;
            }
            return _pluginBaseName[pluginTitle];
        }

        protected Assembly GetAssembly(string pluginTitle)
        {
            InitPluginArrays();

            if (!_pluginAssemblies.ContainsKey(pluginTitle))
            {
                return null;
            }
            return _pluginAssemblies[pluginTitle];
        }

        private void InitPluginArrays()
        {
            if (_pluginAssemblies.IsNullOrEmpty())
            {
                foreach (var pluginObject in _pluginObjects)
                {
                    var webCoreDll = Directory.EnumerateFiles(pluginObject.Path, "*.Web.Core.dll").FirstOrDefault();
                    if (webCoreDll == null)
                    {
                        _pluginAssemblies.Add(pluginObject.Title, null);
                    }
                    else
                    {
                        webCoreDll = Path.GetFileName(webCoreDll).Replace(".dll", "");
                        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                        var assembly = assemblies.FirstOrDefault(a => a.GetName().Name.Equals(webCoreDll));
                        if (assembly == null)
                        {
                            _pluginAssemblies.Add(pluginObject.Title, null);
                        }
                        else
                        {
                            _pluginAssemblies.Add(pluginObject.Title, assembly);
                        }
                        var baseNameSpace = webCoreDll.Replace("Web.Core", "");
                        _pluginBaseName.Add(pluginObject.Title, baseNameSpace);
                    }
                }
            }
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
