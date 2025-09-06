﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.FileDTOs
{
    public class FileUploadRequest
    {
        public IFormFile File { get; set; }
        public string FolderName { get; set; }
    }

}
