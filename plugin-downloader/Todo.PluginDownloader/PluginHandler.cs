using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Todo.PluginDownloader
{
    class PluginHandler
    {
        private string _hostUrl = "http://localhost:21021";

        private string _pluginObjectPath =
            "/api/Plugin/GetPluginObjectsResult";

        private string _pluginDownloadPath = "/api/Plugin/Download";

        private string _rootSavePath = "wwwroot/";

        public PluginHandler()
        {
        }

        public PluginHandler(string[] args)
        {
            initializePluginHandler(args);
        }

        public void Execute(string[] args)
        {
            initializePluginHandler(args);
            DownloadPlugins().GetAwaiter().GetResult();
        }

        public async Task DownloadPlugins()
        {
            var pluginDownloader = new WebDownloader(_hostUrl);
            var pluginObjects = await pluginDownloader.GetPluginObjects(_pluginObjectPath);
            foreach (var pluginObject in pluginObjects)
            {
                var pluginName = pluginObject.Title;
                var fileContentResults = await pluginDownloader.GetFileContentResults(_pluginDownloadPath, pluginName);
                foreach (var fileContentResult in fileContentResults)
                {
                    var saveFilePath = Path.Combine(_rootSavePath, fileContentResult.FileDownloadName);
                    File.WriteAllBytes(saveFilePath, fileContentResult.FileContents);
                    if (File.Exists(saveFilePath) && saveFilePath.EndsWith(".zip"))
                    {
                        var newDir = Path.Combine(_rootSavePath, pluginObject.Url.Replace("/", ""));
                        if (Directory.Exists(newDir))
                        {
                            Directory.Delete(newDir, true);
                        }
                        ZipFile.ExtractToDirectory(saveFilePath, _rootSavePath);
                        File.Delete(saveFilePath);
                    }
                }
            }
        }

        private void initializePluginHandler(string[] args)
        {
            _rootSavePath = Path.GetFullPath(_rootSavePath);
            if (!Directory.Exists(_rootSavePath))
            {
                Directory.CreateDirectory(_rootSavePath);
            }
        }
    }
}
