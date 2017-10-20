using System;
using System.IO;
using Abp.Resources.Embedded;
using Microsoft.Extensions.FileProviders;

namespace Todo.MainProject.Web.Host.Services
{
    public class PluginEmbeddedResourceItemFileInfo : IFileInfo
    {
        public bool Exists => _embeddedResourceItemWrapper.Exists;
        public long Length => _embeddedResourceItemWrapper.Content.Length;
        public string PhysicalPath => _embeddedResourceItemWrapper.PhysicalPath;
        public string Name => _embeddedResourceItemWrapper.Name;
        public DateTimeOffset LastModified => _embeddedResourceItemWrapper.LastModified;
        public bool IsDirectory => _embeddedResourceItemWrapper.IsDirectory;

        private readonly EmbeddedResourceItemWrapper _embeddedResourceItemWrapper;

        public PluginEmbeddedResourceItemFileInfo(EmbeddedResourceItem resourceItem)
        {
            _embeddedResourceItemWrapper = new EmbeddedResourceItemWrapper(resourceItem);
        }

        public PluginEmbeddedResourceItemFileInfo(EmbeddedResourceItem resourceItem,
                                                  string filepath)
        {
            _embeddedResourceItemWrapper = new EmbeddedResourceItemWrapper(resourceItem,
                                                                           filepath);
        }

        public Stream CreateReadStream()
        {
            return new MemoryStream(_embeddedResourceItemWrapper.Content);
        }
    }
}
