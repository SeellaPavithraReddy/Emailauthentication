using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailClient.Models.Entities
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string? FilePath { get; set; }

    }
}