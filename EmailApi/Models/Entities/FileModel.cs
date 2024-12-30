using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi.Models.Entities
{
    [Table("File11")]
    public class FileModel
    {

        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string FilePath { get; set; }
    }


}