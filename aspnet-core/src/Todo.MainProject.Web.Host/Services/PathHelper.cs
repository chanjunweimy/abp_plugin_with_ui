using System;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Resources.Embedded;
using Todo.MainProject.Communication.Dto;

namespace Todo.MainProject.Web.Host.Services
{
    public class PathHelper
    {
        public static string ConvertToRelativePath(EmbeddedResourceSet source, string resourceName)
        {
            resourceName = resourceName.Substring(source.ResourceNamespace.Length + 1);

            var pathParts = resourceName.Split('.');
            if (pathParts.Length <= 2)
            {
                return resourceName;
            }

            string folder;
            string fileName;
            if (resourceName.Contains(".module") ||
                resourceName.Contains(".component") ||
                resourceName.Contains(".routing") ||
                resourceName.Contains(".directive") ||
                resourceName.Contains(".service"))
            {
                folder = pathParts.Take(pathParts.Length - 3).Select(NormalizeFolderName).JoinAsString("/");
                fileName = pathParts[pathParts.Length - 3] + "." +
                           pathParts[pathParts.Length - 2] + "." +
                           pathParts[pathParts.Length - 1];
            }
            else
            {
                folder = pathParts.Take(pathParts.Length - 2).Select(NormalizeFolderName).JoinAsString("/");
                fileName = pathParts[pathParts.Length - 2] + "." + pathParts[pathParts.Length - 1];
            }
            return folder + "/" + fileName;
        }

        public static string NormalizeFolderName(string pathPart)
        {
            //TODO: Implement all rules of .NET
            return pathPart.Replace('-', '_');
        }

        public static string CalculateFileName(string filePath)
        {
            if (!filePath.Contains("/"))
            {
                return filePath;
            }

            return filePath.Substring(filePath.LastIndexOf("/", StringComparison.Ordinal) + 1);
        }

        public static FilenameObject SplitPathAndName(string filePath)
        {
            return new FilenameObject(filePath);
        }
    }
}
