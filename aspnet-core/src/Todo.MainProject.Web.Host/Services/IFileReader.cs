using System.Collections.Generic;
using Abp.Resources.Embedded;
using Microsoft.Extensions.FileProviders;

namespace Todo.MainProject.Web.Host.Services
{
    public interface IFileReader : IFileProvider
    {
        EmbeddedResourceItem GetFileEmbeddedResourceItem(string subpath);
        List<EmbeddedResourceItem> GetFileEmbeddedResourceItems(string subpath);
    }
}
