using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.MainProject.Communication.Dto
{
    public class FileObject
    {
        public byte[] FileContents { get; set; }
        public string ContentType { get; set; }
        public string FileDownloadName { get; set; }
        public string LastModified { get; set; }
        public string EntityTag { get; set; }
    }
}
