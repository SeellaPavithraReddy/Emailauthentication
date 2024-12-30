using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi.Models.Entities
{
    [Table("User118")]
    public class User
    {
        [Key]
        public string userId{set;get;}
        public string firstname{set;get;}
        public string lastName{set;get;}
        public int age{set;get;}
        public long MobileNo{set;get;}
        public string Email{set;get;}
        public int otp{set;get;}
    }
}