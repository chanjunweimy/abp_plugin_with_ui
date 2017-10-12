using System.Collections.Generic;
using Abp.Resources.Embedded;
using Microsoft.Extensions.FileProviders;

namespace Todo.MainProject.Web.Host.Services
{
    public interface IPluginFileService
    {
        /// <summary>
        /// Important to initialize the service.
        /// </summary>
        /// <param name="fileProvider"></param>
        void InjectFileProvider(IFileReader fileProvider);

        IDirectoryContents GetFilesFromProvider(string path);
        List<EmbeddedResourceItem> ReadFilesFromReader(string path);
    }
}
