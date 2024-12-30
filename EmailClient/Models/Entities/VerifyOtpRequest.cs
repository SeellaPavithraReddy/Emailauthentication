using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailClient.Models.Entities
{
    public class VerifyOtpRequest
    {
        [Required(ErrorMessage = "Please Enter Email")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Otp")]

        public string OTP { get; set; }
    }
}