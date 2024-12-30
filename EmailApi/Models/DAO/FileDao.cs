using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Models.Context;
using EmailApi.Models.Entities;

namespace EmailApi.Models.DAO
{
    public class FileDao
    {
        private readonly EmailDbContext emailDbContext;

        public FileDao(EmailDbContext emailDbContext)
        {
            this.emailDbContext = emailDbContext;
        }

        public void SaveFile(FileModel fileModel)
        {
            emailDbContext.fileModels.Add(fileModel);
            emailDbContext.SaveChanges();
        }

        public FileModel GetFile(int id)
        {
            return emailDbContext.fileModels.FirstOrDefault(f => f.Id == id);
        }
    }

}
