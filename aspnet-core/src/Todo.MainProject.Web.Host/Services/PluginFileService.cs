using System.Collections.Generic;
using Abp.Resources.Embedded;
using Microsoft.Extensions.FileProviders;

namespace Todo.MainProject.Web.Host.Services
{
    public class PluginFileService : IPluginFileService
    {
        private IFileReader _fileReader;

        public PluginFileService()
        {
            _fileReader = null;
        }

        public List<EmbeddedResourceItem> ReadFilesFromReader(string path)
        {
            var contents = _fileReader.GetFileEmbeddedResourceItems(path);
            return contents;
        }

        public IDirectoryContents GetFilesFromProvider(string path)
        {
            var contents = _fileReader.GetDirectoryContents(path);
            return contents;
        }

        public void InjectFileProvider(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }
    }
}
