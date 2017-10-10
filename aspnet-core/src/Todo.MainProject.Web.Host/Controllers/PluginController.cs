using System.Collections.Generic;
using System.IO;
using System.Linq;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Todo.MainProject.Controllers;
using Todo.MainProject.Web.Host.Services;
using Todo.MainProject.Web.Host.Services.Dto;

namespace Todo.MainProject.Web.Host.Controllers
{
    [AbpMvcAuthorize]
    public class PluginController : MainProjectControllerBase
    {
        private readonly IPluginService _pluginService;
        private readonly IFileProvider _fileProvider;

        public PluginController(IPluginService pluginService, IFileProvider fileProvider)
        {
            _pluginService = pluginService;
            _fileProvider = fileProvider;
        }

        [HttpGet("api/[controller]/GetPluginObjectsResult")]
        public List<PluginObject> GetPluginObjectsResult()
        {
            return _pluginService.GetPluginObjects();
        }

        [HttpGet("api/[controller]/Download")]
        public List<FileContentResult> Download(string pluginName)
        {
            var files = new List<FileContentResult>();
            if (_pluginService.IsNullService() || pluginName == null)
            {
                return null;
            }

            var pluginObjects = _pluginService.GetPluginObjects();
            var plugin = pluginObjects.FirstOrDefault(p => p.Title == pluginName);
            if (plugin == null)
            {
                return null;
            }

            var folder = plugin.Url.Replace("/", "");
            var fileEntries = _fileProvider.GetDirectoryContents(folder);

            foreach (var fileEntry in fileEntries)
            {
                var file = LoadFileFromPath(fileEntry);
                if (file == null)
                {
                    continue;
                }
                files.Add(file);
            }
           
            return files;
        }

        private FileContentResult LoadFileFromPath(IFileInfo fileEntry)
        {
            var filename = fileEntry.PhysicalPath;
            if (filename == null)
            {
                return null;
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(filename);
            var file = File(fileBytes, GetContentType(filename), Path.GetFileName(filename));
            return file;
        }

        private static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".ico", "image/x-icon"},
                {".js", "application/javascript"},
                {".html", "text/html" },
                {".css", "text/css" }
            };
            }
    }
}