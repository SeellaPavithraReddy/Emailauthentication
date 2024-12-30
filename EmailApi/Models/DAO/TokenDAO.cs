using System.Linq;
using EmailApi;
using EmailApi.Models.Context;
using EmailApi.Models.DAO;
using EmailApi.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace TokensGenarate.Models.DAO
{
    public class TokenDAO : ITokenDAO
    {
        private readonly EmailDbContext _context;

        public TokenDAO(EmailDbContext context)
        {
            _context = context;
        }

        public Userlogin ValidateUser(string username, string password)
        {
            var run = _context.userlogins.FirstOrDefault(s => s.Username == username && s.Password == password);
            return run;
            //return _context.userlogins.FirstOrDefault(s => s.Username == username && s.Password == password);
        }
        public IEnumerable<Userlogin> GetDetails()
        {
            return _context.userlogins.ToList();
        }
        public async Task<int?> GetUserIdAsync(string username, string password)
        {
            var user = await _context.Set<Userlogin>()
                                     .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            return user?.Id;
        }

        //----------------------------------
        public async Task SaveTokenAsync(int userId, string token)
        {
            var user = await _context.userlogins.FindAsync(userId);
            if (user != null)
            {
                user.Token = token;
                await _context.SaveChangesAsync();
            }
        }
        //---------------------------------------


    }
}
