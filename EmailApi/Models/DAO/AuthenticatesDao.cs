using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Models.Context;
using EmailApi.Models.DAO.Interface;
using EmailApi.Models.Entities;

namespace EmailApi.Models.DAO
{
    public class AuthenticatesDao:IAuthentucates
    {
        private readonly EmailDbContext emailDbContext;

        public AuthenticatesDao(EmailDbContext emailDbContext)
        {
            this.emailDbContext = emailDbContext;
        }
        public string Insert(Authenticates authenticates){
            emailDbContext.Add(emailDbContext);
            emailDbContext.SaveChanges();
            return "1";
        }
        public List<Authenticates> Get(){
            return emailDbContext.authenticates.ToList();
        }

        
    }
}