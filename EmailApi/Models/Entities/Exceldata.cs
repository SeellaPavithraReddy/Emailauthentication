using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi.Models.Entities
{
    public class Exceldata
    {
        public int Id { get; set; }
        public string Orderid { get; set; }
        public DateOnly? Orderdate { get; set; }
        public DateOnly? Shipdate { get; set; }
        public string Shipmode { get; set; }
        public string Customerid { get; set; }
        public string Customername { get; set; }
        public string Segment { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Postalcode { get; set; }
        public string Region { get; set; }
        public string Productid { get; set; }
        public IFormFile? file { get; set; }
    }
}

  