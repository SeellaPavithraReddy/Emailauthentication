using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi.Models.Entities
{
    [Table("user112")]
    public class User1
    {


        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsEmailVerified { get; set; }
        public string? OTP { get; set; }
        public DateTime? OTPExpiry { get; set; }
    }

}
