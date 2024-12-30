using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Models.DAO;
using EmailApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace EmailApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class User1Controller : ControllerBase
    {
        private readonly User1Dao user1Dao;

        public User1Controller(User1Dao user1Dao)
        {
            this.user1Dao = user1Dao;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User1 user)
        {
            if (user1Dao.GetUserByEmail(user.Email) != null)
            {
                return BadRequest("Email already exists. Please use a different email or recover your account.");
            }

            user.IsEmailVerified = false;
            user.OTP = GenerateOTP();
            user.OTPExpiry = DateTime.Now.AddMinutes(10);

            user1Dao.AddUser(user);
            SendOtpEmail(user.Email, user.OTP);

            return Ok("Registration successful. Please check your email for the OTP.");
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            var user = user1Dao.GetUserByEmail(request.Email);
            if (user == null || user.OTP != request.OTP || user.OTPExpiry < DateTime.Now)
            {
                return BadRequest("Invalid OTP or OTP has expired.");
            }

            user.IsEmailVerified = true;
            user.OTP = null;
            user.OTPExpiry = DateTime.MinValue;
            user1Dao.UpdateUser(user);

            return Ok("Email verified successfully.");
        }

        private string GenerateOTP()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
        private void SendOtpEmail(string email, string otp)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("PavithraReddy", "pavithrareddy0712@gmail.com"));
            message.To.Add(new MailboxAddress("User", email));
            message.Subject = "Your OTP Code";
            message.Body = new TextPart("plain")
            {
                Text = $"Your OTP code is {otp}. It will expire in 10 minutes."
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Read the email password from the environment variable
                string emailPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
                if (string.IsNullOrEmpty(emailPassword))
                {
                    throw new ArgumentNullException(nameof(emailPassword), "Email password environment variable is not set.");
                }

                // For debugging purposes only
                Console.WriteLine($"Email password: {emailPassword}");

                client.Authenticate("pavithrareddy0712@gmail.com", emailPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }
        [HttpGet("check-env")]
        public IActionResult CheckEnv()
        {
            string emailPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
            if (string.IsNullOrEmpty(emailPassword))
            {
                return BadRequest("Email password environment variable is not set.");
            }
            return Ok($"Email password: {emailPassword}");
        }


    }

    public class VerifyOtpRequest
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}

