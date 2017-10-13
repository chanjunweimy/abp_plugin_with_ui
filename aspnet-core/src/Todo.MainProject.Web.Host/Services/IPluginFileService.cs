using Microsoft.Extensions.FileProviders;

namespace Todo.MainProject.Web.Host.Services
{
    public interface IPluginFileService
    {
        /// <summary>
        /// Important to initialize the service.
        /// </summary>
        /// <param name="fileProvider"></param>
        void InjectFileProvider(IFileProvider fileProvider);

        IDirectoryContents GetFilesFromProvider(string path);
    }
}
