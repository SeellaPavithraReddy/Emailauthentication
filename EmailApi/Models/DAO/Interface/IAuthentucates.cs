using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Models.Entities;

namespace EmailApi.Models.DAO.Interface
{
    public interface IAuthentucates
    {
        public string Insert(Authenticates authenticates);
        public List<Authenticates> Get();
    }
}