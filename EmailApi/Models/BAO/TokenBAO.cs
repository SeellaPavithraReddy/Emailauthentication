using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using EmailApi.Models.DAO;
using EmailApi.Models.Entities;

namespace PracticeAPIS.Models.BAO
{
    public class TokenBAO
    {
        private readonly ITokenDAO _tokenDAO;
        private readonly Jwtset _jwtSettings;

        public TokenBAO(ITokenDAO tokenDAO, Jwtset jwtSettings)
        {
            _tokenDAO = tokenDAO;
            _jwtSettings = jwtSettings;
        }

        // Validate user by checking credentials against the data store
        public Userlogin ValidateUser(string username, string password)
        {
            return _tokenDAO.ValidateUser(username, password);
        }

        // Generate JWT token with user information
        public string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);  // The security key used for signing the token

            // Ensure the key is long enough for security purposes
            if (tokenKey.Length < 16)
            {
                throw new Exception("Key length is insufficient.");
            }

            // Create the claims for the token (i.e., the information that will be embedded into the token)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, username)  // Username as a claim
                }),
                Expires = DateTime.Now.AddMinutes(30),  // Set token expiration (e.g., 30 minutes)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256) // Sign the token
            };

            // Create the token based on the descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the generated JWT token as a string
            return tokenHandler.WriteToken(token);
        }

        // Fetch user details from the database or other data store (this can be customized)
        public IEnumerable<Userlogin> GetDetails()
        {
            return _tokenDAO.GetDetails();
        }
        public async Task<int?> GetUserIdAsync(string username, string password)
        {
            return await _tokenDAO.GetUserIdAsync(username, password);
        }
        //--------------------------------------
        public async Task SaveTokenAsync(int userId, string token)
        {
            await _tokenDAO.SaveTokenAsync(userId, token);
        }
        //----------------------------
    }
}
