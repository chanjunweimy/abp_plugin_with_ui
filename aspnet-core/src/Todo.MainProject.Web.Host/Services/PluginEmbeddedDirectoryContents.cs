using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace Todo.MainProject.Web.Host.Services
{
    public class PluginEmbeddedDirectoryContents : IDirectoryContents
    {
        public bool Exists { get; }
        private readonly IEnumerator<IFileInfo> _fileInfos;

        public PluginEmbeddedDirectoryContents()
        {
            Exists = false;
            _fileInfos = null;
        }

        public PluginEmbeddedDirectoryContents(IEnumerator<IFileInfo> fileInfos)
        {
            Exists = true;
            _fileInfos = fileInfos;
        }

        public IEnumerator<IFileInfo> GetEnumerator()
        {
           return  _fileInfos;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
