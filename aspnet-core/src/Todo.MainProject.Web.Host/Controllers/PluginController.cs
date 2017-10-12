using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Todo.MainProject.Controllers;
using Todo.MainProject.Web.Host.Services;
using Todo.MainProject.Web.Host.Services.Dto;

namespace Todo.MainProject.Web.Host.Controllers
{
    public class PluginController : MainProjectControllerBase
    {
        private readonly IPluginService _pluginService;
        private readonly IPluginFileService _pluginFileService;

        public PluginController(IPluginService pluginService, IPluginFileService pluginFileService)
        {
            _pluginService = pluginService;
            _pluginFileService = pluginFileService;
        }

        [HttpGet("api/[controller]/GetPluginObjectsResult")]
        public List<PluginObject> GetPluginObjectsResult()
        {
            return _pluginService.GetPluginObjects();
        }

        [HttpGet("api/[controller]/Download")]
        public List<FileContentResult> Download(string pluginName)
        {
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
            var folder = plugin.Path.Replace("/", "");
            var fileEntries = _pluginFileService.GetFilesFromProvider(folder);
            return fileEntries.Select(LoadFileFromPath).Where(file => file != null).ToList();
        }

        private FileContentResult LoadFileFromPath(IFileInfo fileEntry)
        {
            var filename = fileEntry.Name;
            var stream = fileEntry;
            var memstream = new MemoryStream();
            stream.CopyTo(memstream);
            var fileBytes = memstream.ToArray();
            var file = File(fileBytes, GetContentType(filename), filename);
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
                {".css", "text/css" },
                {".zip", "application/zip" }
            };
        }
    }
}