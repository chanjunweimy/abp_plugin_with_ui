using System;
using System.Collections.Generic;
using System.Linq;
using Abp.AspNetCore.EmbeddedResources;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Resources.Embedded;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Todo.MainProject.Web.Host.Services
{
    public class PluginEmbeddedResourceFileProvider : IFileProvider
    {
        private readonly IIocResolver _iocResolver;
        private readonly Lazy<IEmbeddedResourceManager> _embeddedResourceManager;
        private readonly Lazy<IEmbeddedResourcesConfiguration> _configuration;
        private Dictionary<string, IDirectoryContents> _directoryContents;
        private Dictionary<string, List<EmbeddedResourceItem>> _embeddedResourceItems;
        private bool _isInitialized;

        public PluginEmbeddedResourceFileProvider(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
            _embeddedResourceManager = new Lazy<IEmbeddedResourceManager>(
                () => iocResolver.Resolve<IEmbeddedResourceManager>(),
                true
            );

            _configuration = new Lazy<IEmbeddedResourcesConfiguration>(
                () => iocResolver.Resolve<IEmbeddedResourcesConfiguration>(),
                true
            );
            _directoryContents = null;
            _embeddedResourceItems = null;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (!IsInitialized())
            {
                return new NotFoundFileInfo(subpath);
            }

            var resource = GetFileEmbeddedResourceItem(subpath);

            if (resource == null || IsIgnoredFile(resource))
            {
                return new NotFoundFileInfo(subpath);
            }

            return new EmbeddedResourceItemFileInfo(resource);
        }

        private EmbeddedResourceItem GetFileEmbeddedResourceItem(string subpath)
        {
            if (!IsInitialized())
            {
                return null;
            }
            var resource = _embeddedResourceManager.Value.GetResource(subpath);
            if (IsIgnoredFile(resource))
            {
                return null;
            }
            return resource;
        }

        /// <summary>
        /// Hack: treat subpath as plugin name instead
        /// </summary>
        /// <param name="subpath"></param>
        /// <inheritdoc />
        /// <returns></returns>
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            if (!IsInitialized())
            {
                return new NotFoundDirectoryContents();
            }
            return GetPluginDirectoryContents(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return NullChangeToken.Singleton;
        }

        /// <summary>
        /// TODO: support add ignore file
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        protected virtual bool IsIgnoredFile(EmbeddedResourceItem resource)
        {
            return resource.FileExtension == null;
        }

        private bool IsInitialized()
        {
            if (_isInitialized)
            {
                return true;
            }

            _isInitialized = _iocResolver.IsRegistered<IEmbeddedResourceManager>() && _iocResolver.IsRegistered<IEmbeddedResourcesConfiguration>();
            return _isInitialized;
        }

        private IDirectoryContents GetPluginDirectoryContents(string pluginName)
        {
            if (_directoryContents == null)
            {
                _directoryContents = CreateDirectoryContents();
            }

            if (!_directoryContents.ContainsKey(pluginName))
            {
                return new NotFoundDirectoryContents();
            }
            return _directoryContents[pluginName];
        }

        private Dictionary<string, IDirectoryContents> CreateDirectoryContents()
        {
            var directoryContents = new Dictionary<string, IDirectoryContents>(StringComparer.OrdinalIgnoreCase);

            foreach (var source in _configuration.Value.Sources)
            {
                var fileInfos = new List<IFileInfo>();
                foreach (var resourceName in source.Assembly.GetManifestResourceNames())
                {
                    if (!resourceName.StartsWith(source.ResourceNamespace))
                    {
                        continue;
                    }
                    var filePath = source.RootPath + ConvertToRelativePath(source, resourceName);
                    var fileInfo = GetFileInfo(filePath);
                    fileInfos.Add(fileInfo);
                }
                IDirectoryContents directoryContent = new PluginEmbeddedDirectoryContents(fileInfos.GetEnumerator());
                directoryContents.Add(source.ResourceNamespace, directoryContent);
            }

            return directoryContents;
        }


        private string ConvertToRelativePath(EmbeddedResourceSet source, string resourceName)
        {
            resourceName = resourceName.Substring(source.ResourceNamespace.Length + 1);

            var pathParts = resourceName.Split('.');
            if (pathParts.Length <= 2)
            {
                return resourceName;
            }

            var folder = pathParts.Take(pathParts.Length - 2).Select(NormalizeFolderName).JoinAsString("/");
            var fileName = pathParts[pathParts.Length - 2] + "." + pathParts[pathParts.Length - 1];

            return folder + "/" + fileName;
        }

        private static string NormalizeFolderName(string pathPart)
        {
            //TODO: Implement all rules of .NET
            return pathPart.Replace('-', '_');
        }
    }
}
