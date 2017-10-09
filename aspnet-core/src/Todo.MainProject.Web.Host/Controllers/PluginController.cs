using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.MainProject.Controllers;
using Todo.MainProject.Web.Host.Services;
using Todo.MainProject.Web.Host.Services.Dto;

namespace Todo.MainProject.Web.Host.Controllers
{
    [AbpMvcAuthorize]
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

        [HttpGet]
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
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