using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi.Models.Entities
{
    public class OtpVerificationRequest
    {
        public string Email { get; set; }
        public int EnteredOtp { get; set; }
    }

}