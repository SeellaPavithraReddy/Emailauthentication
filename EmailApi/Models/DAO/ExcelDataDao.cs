using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EmailApi.Models.DAO.Interface;
using EmailApi.Models.Entities;

using ExcelDataReader;

namespace EmailApi.Models.DAO
{
    public class ExcelDataDao : IExcelDataDao
    {
        public List<Exceldata> GetExcelData(string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var exdata = new List<Exceldata>();
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateBinaryReader(stream))
                {
                    while (reader.Read())
                    {
                        var exd = new Exceldata
                        {
                            Orderid = reader.GetValue(0)?.ToString(),
                            Orderdate = DateTime.TryParse(reader.GetValue(1)?.ToString(), out DateTime orderDate) ? DateOnly.FromDateTime(orderDate) : (DateOnly?)null,
                            Shipdate = DateTime.TryParse(reader.GetValue(2)?.ToString(), out DateTime shipdate) ? DateOnly.FromDateTime(shipdate) : (DateOnly?)null,
                            Shipmode = reader.GetValue(3)?.ToString(),
                            Customerid = reader.GetValue(4)?.ToString(),
                            Customername = reader.GetValue(5)?.ToString(),
                            Segment = reader.GetValue(6)?.ToString(),
                            Country = reader.GetValue(7)?.ToString(),
                            City = reader.GetValue(8)?.ToString(),
                            Postalcode = reader.GetValue(9)?.ToString(),
                            Region = reader.GetValue(10)?.ToString(),
                            Productid = reader.GetValue(11)?.ToString(),
                        };
                        exdata.Add(exd);
                    }
                }
            }
            return exdata;
        }
    }
}
