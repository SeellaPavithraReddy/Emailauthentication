using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Models.DAO;
using EmailApi.Models.Entities;

namespace EmailApi.Models.BAO
{
    public class AuthenticatesBao
    {
        private readonly AuthenticatesDao authenticatesDao;

        public AuthenticatesBao(AuthenticatesDao authenticatesDao)
        {
            this.authenticatesDao = authenticatesDao;
        }

        public string Insert(Authenticates authenticates){
            return authenticatesDao.Insert(authenticates);
        }
        public List<Authenticates> Get(){
            return authenticatesDao.Get();
        }
    }
}