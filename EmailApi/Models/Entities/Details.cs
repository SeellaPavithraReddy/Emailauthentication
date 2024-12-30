using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi.Models.Entities
{
    [Table("Details3312")]
    public class Details
    {
        [Key]
        public string userId{set;get;}
                public string Pwd{set;get;}


    }
}