using System.Collections.Generic;
using Abp.Resources.Embedded;
using Microsoft.Extensions.FileProviders;

namespace Todo.MainProject.Web.Host.Services
{
    public class PluginFileService : IPluginFileService
    {
        private IFileProvider _fileReader;

        public PluginFileService()
        {
            _fileReader = null;
        }

        public IDirectoryContents GetFilesFromProvider(string path)
        {
            var contents = _fileReader.GetDirectoryContents(path);
            return contents;
        }

        public void InjectFileProvider(IFileProvider fileReader)
        {
            _fileReader = fileReader;
        }
    }
}
