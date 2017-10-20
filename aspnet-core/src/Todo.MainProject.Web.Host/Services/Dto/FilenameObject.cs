
using System;

namespace Todo.MainProject.Web.Host.Services.Dto
{
    public class FilenameObject
    {
        public FilenameObject(string filePath)
        {
            if (!filePath.Contains("/"))
            {
                FileName = filePath;
                PhysicalPath = "";
            }
            else
            {
                var splitIndex = filePath.LastIndexOf("/", StringComparison.Ordinal);
                FileName = filePath.Substring(splitIndex + 1);
                PhysicalPath = filePath.Substring(0, splitIndex);
            }
        }

        public string FileName { get; set; }
        public string PhysicalPath { get; set; }
    }
}
