// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net;
// using System.Net.Mail;
// using System.Security.Cryptography;
// using System.Threading.Tasks;
// using EmailApi.Models.DAO;
// using EmailApi.Models.Entities;

// namespace EmailApi.Models.BAO
// {
//     public class UserBao
//     {
//         private readonly UserDao userDao;

//         public UserBao(UserDao userDao)
//         {
//             this.userDao = userDao;
//         }
//         public List<User> GetUser()
//         {
//             return userDao.GetUsers();
//         }
//         public string userAdd(User user)
//         {
//             int otp = generateOtp();

//             SendOtpEmail(user.Email, otp);

//             UpdateUserOtp(user.Email, otp);

//             return userDao.AddUser(user);
//         }
//         private int generateOtp()
//         {
//             using (var rng = new RNGCryptoServiceProvider())
//             {
//                 byte[] tokenData = new byte[4];
//                 rng.GetBytes(tokenData);

//                 int otp = BitConverter.ToInt32(tokenData, 0) % 1000000;
//                 return Math.Abs(otp); // Ensure the OTP is positive
//             }
//           //  return new Random().Next(100000, 999999).ToString();
//         }
//         public void SendOtpEmail(string toEmail, int otp)
//         {
//             var fromAddress = new MailAddress("your-email@example.com", "Your Name");
//             var toAddress = new MailAddress(toEmail);
//             const string fromPassword = "your-email-password";
//             const string subject = "Your OTP Code";
//             string body = $"Your OTP code is: {otp}";

//             var smtp = new SmtpClient
//             {
//                 Host = "smtp.example.com",
//                 Port = 587,
//                 EnableSsl = true,
//                 DeliveryMethod = SmtpDeliveryMethod.Network,
//                 UseDefaultCredentials = false,
//                 Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
//             };

//             using (var message = new MailMessage(fromAddress, toAddress)
//             {
//                 Subject = subject,
//                 Body = body
//             })
//             {
//                 smtp.Send(message);
//             }
//         }

//         public bool VerifyOtp(int enteredOtp, int receivedOtp)
//         {
//             return enteredOtp == receivedOtp;
//         }

//         public void UpdateUserOtp(string email, int otp)
//         {
//             var user = userDao.getEmail(email);
//             if (user != null)
//             {
//                 user.otp = otp;
//                 userDao.AddUser(user);
//             }
//         }
//         public User GetUserByEmail(string email)
//         {
//             return userDao.getEmail(email);
//         }

//     }
// }
//----------------------------------------------------------------------------------------
// using System;
// using System.Collections.Generic;
// using System.Security.Cryptography;
// using EmailApi.Models.DAO;
// using EmailApi.Models.Entities;

// namespace EmailApi.Models.BAO
// {
//     public class UserBao
//     {
//         private readonly UserDao userDao;
//         private readonly EmailService emailService;

//         public UserBao(UserDao userDao, EmailService emailService)
//         {
//             this.userDao = userDao;
//             this.emailService = emailService;
//         }

//         public List<User> GetUser()
//         {
//             return userDao.GetUsers();
//         }

//         public string UserAdd(User user)
//         {
//             int otp = GenerateOtp();

//             emailService.SendEmail(user.Email, "Your OTP Code", $"Your OTP code is: {otp}");

//             UpdateUserOtp(user.Email, otp);

//             return userDao.AddUser(user);
//         }

//         private int GenerateOtp()
//         {
//             using (var rng = new RNGCryptoServiceProvider())
//             {
//                 byte[] tokenData = new byte[4];
//                 rng.GetBytes(tokenData);

//                 int otp = BitConverter.ToInt32(tokenData, 0) % 1000000;
//                 return Math.Abs(otp); // Ensure the OTP is positive
//             }
//         }
//         public void SendOtp(string email)
//         {
//             int otp = GenerateOtp();
//             emailService.SendEmail(email, "Your OTP Code", $"Your OTP code is: {otp}");
//             UpdateUserOtp(email, otp);
//         }


//         public bool VerifyOtp(int enteredOtp, int receivedOtp)
//         {
//             return enteredOtp == receivedOtp;
//         }

//         public void UpdateUserOtp(string email, int Otp)
//         {
//             var user = userDao.getEmail(email);
//             if (user != null)
//             {
//                 user.otp = Otp;
//                 userDao.AddUser(user);
//             }
//         }

//         public User GetUserByEmail(string email)
//         {
//             return userDao.getEmail(email);
//         }
//     }
// }
//--------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using EmailApi.Models.DAO;
using EmailApi.Models.Entities;
using Microsoft.Extensions.Logging;

namespace EmailApi.Models.BAO
{
    public class UserBao
    {
        private readonly UserDao userDao;
        private readonly EmailService emailService;
        private readonly ILogger<UserBao> _logger;

        public UserBao(UserDao userDao, EmailService emailService, ILogger<UserBao> logger)
        {
            this.userDao = userDao;
            this.emailService = emailService;
            _logger = logger;
        }

        public List<User> GetUser()
        {
            return userDao.GetUsers();
        }

        public string UserAdd(User user)
        {
            int otp = GenerateOtp();

            emailService.SendEmail(user.Email, "Your OTP Code", $"Your OTP code is: {otp}");

            UpdateUserOtp(user.Email, otp);

            return userDao.AddUser(user);
        }

        public void SendOtp(string email)
        {
            try
            {
                int otp = GenerateOtp();
                emailService.SendEmail(email, "Your OTP Code", $"Your OTP code is: {otp}");
                UpdateUserOtp(email, otp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending OTP to {Email}", email);
                throw;
            }
        }

        private int GenerateOtp()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[4];
                rng.GetBytes(tokenData);

                int otp = BitConverter.ToInt32(tokenData, 0) % 1000000;
                return Math.Abs(otp); // Ensure the OTP is positive
            }
        }

        public bool VerifyOtp(int enteredOtp, int receivedOtp)
        {
            return enteredOtp == receivedOtp;
        }

        public void UpdateUserOtp(string email, int otp)
        {
            var user = userDao.getEmail(email);
            if (user != null)
            {
                user.otp = otp;
                userDao.AddUser(user);
            }
        }

        public User GetUserByEmail(string email)
        {
            return userDao.getEmail(email);
        }
    }
}
