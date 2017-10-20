using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Todo.MainProject.Communication.Dto;

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
            InitializePluginHandler(args);
        }

        public void ExecuteZip(string[] args)
        {
            _rootSavePath = "wwwroot/";
            InitializePluginHandler(args);
            DownloadPlugins().GetAwaiter().GetResult();
        }

        public void ExecuteSource(string[] args)
        {
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
                    var filenameObject = new FilenameObject(fileContentResult.FileDownloadName);
                    var saveFileRoot = RecursiveCreateDirectory(filenameObject.PhysicalPath);

                    var saveFilePath = Path.Combine(saveFileRoot, filenameObject.FileName);
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

        private void InitializePluginHandler(string[] args)
        {
            CreateIfNotExist(_rootSavePath);
        }

        private string RecursiveCreateDirectory(string path)
        {
            var folders = path.Split("/");
            var createdPath = "";
            var finalDirectory = "";
            foreach (var folder in folders)
            {
                var folderName = folder.Trim();
                if (string.IsNullOrEmpty(folderName))
                {
                    continue;
                }
                if (folderName == "app")
                {
                    folderName = "plugins";
                }
                createdPath += folderName + "/";
                finalDirectory = CreateIfNotExist(createdPath);
            }
            return finalDirectory;
        }

        private string CreateIfNotExist(string folder)
        {
            folder = Path.GetFullPath(folder);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
        }
    }
}
