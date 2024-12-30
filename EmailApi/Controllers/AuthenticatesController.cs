// using System;
// using System.Collections.Generic;
// using Microsoft.AspNetCore.Mvc;
// using EmailApi.Models.BAO;
// using EmailApi.Models.Entities;
// using Microsoft.AspNetCore.Authorization;

// namespace EmailApi.Controllers
// {
//     [ApiController]  // This automatically adds model validation and binding conventions
//     [Route("api/[controller]")]  // Base route for all actions in this controller
//     public class AuthenticatesController : ControllerBase
//     {
//         private readonly AuthenticatesBao authenticatesBao;

//         public AuthenticatesController(AuthenticatesBao authenticatesBao)
//         {
//             this.authenticatesBao = authenticatesBao;
//         }
//         [Authorize]
//         // GET: api/authenticate/GetAll
//         [HttpGet("GetAll")]  // Route for the GetAll action
//         public IActionResult GetAll()
//         {
//             List<Authenticates> data = authenticatesBao.Get();
//             if (data.Count != 0)
//             {
//                 return Ok(data);  // Return the data with a 200 OK response
//             }
//             else
//             {
//                 return NotFound("No records found");  // Return a 404 if no data
//             }
//         }

//         // POST: api/authenticate/Insert
//         [HttpPost("Insert")]  // Route for the Insert action
//         public IActionResult Insert([FromBody] Authenticates authenticates)
//         {
//             if (authenticates == null)
//             {
//                 return BadRequest("Invalid data");  // Check if the body is null
//             }

//             string result = authenticatesBao.Insert(authenticates);
//             if (result != null)
//             {
//                 return Ok(result);  // Return success response with the result
//             }
//             else
//             {
//                 return BadRequest("Error inserting record");  // Return a BadRequest if something went wrong
//             }
//         }
//     }
// }
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EmailApi.Models.BAO;
using EmailApi.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace EmailApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticatesController : ControllerBase
    {
        private readonly AuthenticatesBao _authenticatesBao;

        public AuthenticatesController(AuthenticatesBao authenticatesBao)
        {
            _authenticatesBao = authenticatesBao;
        }

        // This endpoint requires authorization
        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            List<Authenticates> data = _authenticatesBao.Get();
            if (data.Count != 0)
            {
                return Ok(data);  // Return data with a 200 OK response
            }
            else
            {
                return NotFound("No records found");  // Return a 404 if no data
            }
        }

        // This endpoint allows inserting a new authenticate record
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] Authenticates authenticates)
        {
            if (authenticates == null)
            {
                return BadRequest("Invalid data");
            }

            string result = _authenticatesBao.Insert(authenticates);
            if (result != null)
            {
                return Ok(result);  // Return success response with the result
            }
            else
            {
                return BadRequest("Error inserting record");  // Return BadRequest if something went wrong
            }
        }
    }
}

