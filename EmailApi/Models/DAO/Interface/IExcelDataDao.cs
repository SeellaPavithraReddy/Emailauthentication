using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Models.Entities;

namespace EmailApi.Models.DAO.Interface
{
    public interface IExcelDataDao
    {
        List<Exceldata> GetExcelData(string path);
    }
}