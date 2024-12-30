using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Models.Context;
using EmailApi.Models.Entities;

namespace EmailApi.Models.DAO
{
    public class User1Dao
    {
        private readonly EmailDbContext emailDbContext;

        public User1Dao(EmailDbContext emailDbContext)
        {
            this.emailDbContext = emailDbContext;
        }

    public User1 GetUserByEmail(string email)
    {
        return emailDbContext.user1s.FirstOrDefault(u => u.Email == email);
    }

    public void UpdateUser(User1 user)
    {
        emailDbContext.user1s.Update(user);
        emailDbContext.SaveChanges();
    }

    public void AddUser(User1 user)
    {
        emailDbContext.user1s.Add(user);
        emailDbContext.SaveChanges();
    }
}

    }
