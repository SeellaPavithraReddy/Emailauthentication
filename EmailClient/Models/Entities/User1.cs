using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailClient.Models.Entities
{
    public class User1
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "please enter email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "please enter Password")]

        public string Password { get; set; }
        public bool? IsEmailVerified { get; set; }
        public string? OTP { get; set; }
        public DateTime? OTPExpiry { get; set; }
    }
}