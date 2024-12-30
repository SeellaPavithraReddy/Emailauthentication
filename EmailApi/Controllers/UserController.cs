// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using EmailApi.Models.BAO;
// using EmailApi.Models.Entities;
// using Microsoft.AspNetCore.Mvc;
//  //MT-DCADC.in.mouritech.net


// namespace EmailApi.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class UserController : ControllerBase
//     {
//         private readonly UserBao userBao;
//     private readonly EmailService _emailService;

//         public UserController(UserBao userBao, EmailService emailService)
//         {
//             this.userBao = userBao;
//             _emailService = emailService;
//         }

//         [Route("GetUsers")]
//         [HttpGet]
//         public IActionResult GetUserAll()
//         {
//             var userData = userBao.GetUser();
//             return Ok(userData);
//         }
//         [Route("Add")]
//         [HttpPost]
//         public IActionResult Add([FromBody] User user)
//         {
//             var user1 = userBao.userAdd(user);
//             if (user1 != null)
//                 return Ok(user1);
//             else
//                 return BadRequest("Invalid data");
//         }
        
//         [HttpPost("verify")]
//         public IActionResult VerifyOtp([FromBody] OtpVerificationRequest request)
//         {
//             var user = userBao.GetUserByEmail(request.Email);
//             if (user != null && userBao.VerifyOtp(request.EnteredOtp, user.otp))
//             {
//                 return Ok("OTP verified successfully!");
//             }
//             else
//             {
//                 return BadRequest("Wrong OTP. Please try again.");
//             }
//         }



//     }

// }
//-----------------------------------------------------------------------
// using EmailApi.Models.BAO;
// using EmailApi.Models.Entities;
// using Microsoft.AspNetCore.Mvc;

// namespace EmailApi.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class UserController : ControllerBase
//     {
//         private readonly UserBao userBao;

//         public UserController(UserBao userBao)
//         {
//             this.userBao = userBao;
//         }

//         [Route("GetUsers")]
//         [HttpGet]
//         public IActionResult GetUserAll()
//         {
//             var userData = userBao.GetUser();
//             return Ok(userData);
//         }

//         [Route("Add")]
//         [HttpPost]
//         public IActionResult Add([FromBody] User user)
//         {
//             var user1 = userBao.UserAdd(user);
//             if (user1 != null)
//                 return Ok(user1);
//             else
//                 return BadRequest("Invalid data");
//         }

//         [HttpPost("verify")]
//         public IActionResult VerifyOtp([FromBody] OtpVerificationRequest request)
//         {
//             var user = userBao.GetUserByEmail(request.Email);
//             if (user != null && userBao.VerifyOtp(request.EnteredOtp, user.otp))
//             {
//                 return Ok("OTP verified successfully!");
//             }
//             else
//             {
//                 return BadRequest("Wrong OTP. Please try again.");
//             }
//         }
//         [HttpPost("send-otp")]
//         public IActionResult SendOtp([FromBody] EmailRequest request)
//         {
//             userBao.SendOtp(request.ToEmail);
//             return Ok(new { Message = "OTP sent successfully!" });
//         }
//     }

//     }

//--------------------------------------------------------------
using EmailApi.Models.BAO;
using EmailApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmailApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserBao userBao;

        public UserController(UserBao userBao)
        {
            this.userBao = userBao;
        }

        [Route("GetUsers")]
        [HttpGet]
        public IActionResult GetUserAll()
        {
            var userData = userBao.GetUser();
            return Ok(userData);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult Add([FromBody] User user)
        {
            var user1 = userBao.UserAdd(user);
            if (user1 != null)
                return Ok(user1);
            else
                return BadRequest("Invalid data");
        }

        [HttpPost("verify")]
        public IActionResult VerifyOtp([FromBody] OtpVerificationRequest request)
        {
            var user = userBao.GetUserByEmail(request.Email);
            if (user != null && userBao.VerifyOtp(request.EnteredOtp, user.otp))
            {
                return Ok("OTP verified successfully!");
            }
            else
            {
                return BadRequest("Wrong OTP. Please try again.");
            }
        }

        [HttpPost("send-otp")]
        public IActionResult SendOtp([FromBody] EmailRequest request)
        {
            try
            {
                userBao.SendOtp(request.ToEmail);
                return Ok(new { Message = "OTP sent successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }

    public class EmailRequest
    {
        public string ToEmail { get; set; }
    }
}
