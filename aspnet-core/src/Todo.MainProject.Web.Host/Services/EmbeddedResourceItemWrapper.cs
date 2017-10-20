using System;
using Abp.Resources.Embedded;

namespace Todo.MainProject.Web.Host.Services
{
    internal class EmbeddedResourceItemWrapper
    {
        public bool Exists { get; set; }
        public string PhysicalPath { get; set; }
        public string Name { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public bool IsDirectory { get; set; }

        public byte[] Content { get; set; }

        public EmbeddedResourceItemWrapper(EmbeddedResourceItem resourceItem)
        {
            InitializeObject(resourceItem, null);
        }

        public EmbeddedResourceItemWrapper(EmbeddedResourceItem resourceItem,
            string filepath)
        {
            InitializeObject(resourceItem, filepath);
        }

        private void InitializeObject(EmbeddedResourceItem resourceItem,
            string filepath)
        {
            Exists = true;
            Content = resourceItem.Content;
            LastModified = resourceItem.LastModifiedUtc;
            IsDirectory = false;

            if (filepath == null)
            {
                PhysicalPath = null;
                Name = resourceItem.FileName;
            }
            else
            {
                var obj = PathHelper.SplitPathAndName(filepath);
                PhysicalPath = obj.PhysicalPath;
                Name = obj.FileName;
            }
        }
    }
}
