// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using EmailApi.Models.Entities;

// namespace EmailApi.Models.BAO
// {
//     public class ExcelDataDao
//     {
//         private readonly ExcelDataDao excelDataDao;

//         public ExcelDataDao(ExcelDataDao excelDataDao)
//         {
//             this.excelDataDao = excelDataDao;
//         }

//         public List<Exceldata> GetExcelData(string path)
//         {
//             Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
//             var exdata = new List<Exceldata>();
//             using (var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
//             {
//                 using (var reader = ExcelReaderFactory.CreateBinaryReader(stream))
//                 {
//                     while (reader.Read())
//                     {
//                         var exd = new Exceldata
//                         {
//                             Orderid = reader.GetValue(0).ToString(),
//                             Orderdate = DateTime.TryParse(reader.GetValue(1).ToString(), out DateTime orderDate) ? DateOnly.FromDateTime(orderDate) : null,
//                             Shipdate = DateTime.TryParse(reader.GetValue(2).ToString(), out DateTime shipdate) ? DateOnly.FromDateTime(shipdate) : null,
//                             Shipmode = reader.GetValue(3).ToString(),
//                             Customerid = reader.GetValue(4).ToString(),
//                             Customername = reader.GetValue(5).ToString(),
//                             Segment = reader.GetValue(6).ToString(),
//                             Country = reader.GetValue(7).ToString(),
//                             City = reader.GetValue(8).ToString(),
//                             Postalcode = reader.GetValue(9).ToString(),
//                             Region = reader.GetValue(10).ToString(),
//                             Productid = reader.GetValue(11).ToString(),
//                             Category = reader.GetValue(12).ToString(),
//                             SubCategory = reader.GetValue(13).ToString(),
//                             Productname = reader.GetValue(14).ToString(),
//                             Sales = reader.GetValue(15).ToString(),
//                             Quantity = reader.GetValue(16).ToString(),
//                             Discount = reader.GetValue(17).ToString(),
//                             Profit = reader.GetValue(18).ToString(),
//                         };
//                         exdata.Add(exd);
//                     }
//                 }
//             }
//             return exdata;
//         }
//     }
// }
