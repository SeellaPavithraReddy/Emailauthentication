using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EmailApi.Models.Context;
using EmailApi.Models.Entities;
using Microsoft.Data.SqlClient;

namespace EmailApi.Models.DAO
{
    public class UserDao
    {
        private readonly EmailDbContext emailDbContext;

        public UserDao(EmailDbContext emailDbContext)
        {
            this.emailDbContext = emailDbContext;
        }
        public List<User> GetUsers()
        {
            return emailDbContext.users.ToList();
        }
        public string AddUser(User user)
        {
         
                emailDbContext.users.Add(user);
                emailDbContext.SaveChanges();
                return "added Successfully";
        }
        public User getEmail(string email){
            return emailDbContext.users.Where(x=>x.Email.Equals(email)).FirstOrDefault(); 
        }
    }
}

    
