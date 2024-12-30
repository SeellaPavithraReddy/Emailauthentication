using EmailApi.Models.Entities;

namespace EmailApi.Models.DAO
{
    public interface ITokenDAO
    {
        Userlogin ValidateUser(string username, string password);
        IEnumerable<Userlogin> GetDetails();
        Task<int?> GetUserIdAsync(string username, string password);
        Task SaveTokenAsync(int userId, string token); // Add this method



    }
}
