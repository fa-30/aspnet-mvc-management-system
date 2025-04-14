using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Services.AttachementService
{
    public interface IAttachmentService
    {
        public string? Upload(IFormFile  file, string FolderName);
        bool Delete(string filePath);
    }
}
