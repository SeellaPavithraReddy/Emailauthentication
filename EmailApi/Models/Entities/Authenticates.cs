using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi.Models.Entities
{
    [Table("sathii331")]
    public class Authenticates
    {
        [Key]
        public string userid{get;set;}
        public string UserName{get;set;}
        public string Password{get;set;}
    }
}