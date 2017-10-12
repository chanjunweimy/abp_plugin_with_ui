using Microsoft.Extensions.FileProviders;

namespace Todo.MainProject.Web.Host.Services
{
    public class PluginFileService : IPluginFileService
    {
        private IFileProvider _fileProvider;

        public PluginFileService()
        {
            _fileProvider = new NullFileProvider();
        }

        public IDirectoryContents GetFilesFromProvider(string path)
        {
            var contents = _fileProvider.GetDirectoryContents(path);
            return contents;
        }

        public void InjectFileProvider(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
    }
}
