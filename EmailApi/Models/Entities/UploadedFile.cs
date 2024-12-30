using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi.Models.Entities
{
    [Table("uploadedFile13")]
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string? FilePath { get; set; }

    }
}