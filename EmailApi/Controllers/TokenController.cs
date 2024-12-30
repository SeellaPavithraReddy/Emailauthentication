// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;
// using EmailApi.Models.Entities;
// using EmailApi.Models.BAO;
// using PracticeAPIS.Models.BAO;

// namespace EmailApi.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     [Authorize]
//     public class TokenController : ControllerBase
//     {
//         private readonly TokenBAO _tokenBAO;

//         public TokenController(TokenBAO tokenBAO)
//         {
//             _tokenBAO = tokenBAO;
//         }

//         [HttpGet("details-get")]
//         [Authorize]
//         public IActionResult GetUserDetails()
//         {
//             var details = _tokenBAO.GetDetails();
//             return Ok(new { data = details });
//         }

//         [HttpPost("authenticate")]
//         public IActionResult Authenticate([FromBody] Jwtcredentials credentials)
//         {
//             var user = _tokenBAO.ValidateUser(credentials.Username, credentials.Password);

//             if (user == null)
//             {
//                 return Unauthorized("Invalid username or password.");
//             }

//             var token = _tokenBAO.GenerateToken(user.Username);
//             return Ok(new { token });
//         }
//     }
// }
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmailApi.Models.Entities;
using EmailApi.Models.BAO;
using PracticeAPIS.Models.BAO;
using Microsoft.Extensions.Logging;

namespace EmailApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]
    public class TokenController : ControllerBase
    {
        private readonly TokenBAO _tokenBAO;
        private readonly ILogger<TokenController> _logger;

        // Inject logger and TokenBAO service
        public TokenController(TokenBAO tokenBAO, ILogger<TokenController> logger)
        {
            _tokenBAO = tokenBAO;
            _logger = logger;
        }

        // Get user details - This is a protected endpoint
        [HttpGet("details-get")]
        //[Authorize]
        public IActionResult GetUserDetails()
        {
            try
            {
                var details = _tokenBAO.GetDetails();
                return Ok(new { data = details });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user details.");
                return StatusCode(500, "An unexpected error occurred while fetching user details.");
            }
        }

        // Authentication method - Accepts username and password to authenticate user
        // [HttpPost("Authenticate")]
        // public IActionResult Authenticate([FromBody] Jwtcredentials credentials)
        // {
        //     try
        //     {
        //         // Validate model state
        //         if (!ModelState.IsValid)
        //         {
        //             return BadRequest("Invalid credentials format.");
        //         }

        //         // Validate user credentials using TokenBAO
        //         var user = _tokenBAO.ValidateUser(credentials.Username, credentials.Password);

        //         if (user == null)
        //         {
        //             return Unauthorized("Invalid username or password.");
        //         }

        //         // Generate JWT Token for the valid user
        //         var token = _tokenBAO.GenerateToken(user.Username);
        //         return Ok(new { token });
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "An error occurred during authentication.");
        //         return StatusCode(500, "An unexpected error occurred during authentication.");
        //     }
        // }
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Jwtcredentials credentials)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid credentials format.");
                }

                var user = _tokenBAO.ValidateUser(credentials.Username, credentials.Password);

                if (user == null)
                {
                    return Unauthorized("Invalid username or password.");
                }

                var token = _tokenBAO.GenerateToken(user.Username);

                await _tokenBAO.SaveTokenAsync(user.Id, token);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during authentication.");
                return StatusCode(500, "An unexpected error occurred during authentication.");
            }
        }

        [HttpPost("GetUserId")]
        public async Task<IActionResult> GetUserId([FromBody] LoginRequest request)
        {
            var userId = await _tokenBAO.GetUserIdAsync(request.Username, request.Password);
            if (userId.HasValue)
            {
                return Ok(userId.Value);
            }
            return NotFound("User not found");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
