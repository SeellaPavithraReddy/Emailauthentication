using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi.Models.Entities
{
    [Table("UserLogin111")]
    public class Userlogin
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public string? Token { get; set; } 
    }
}

