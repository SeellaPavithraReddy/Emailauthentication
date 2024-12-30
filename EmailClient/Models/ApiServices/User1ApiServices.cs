using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailClient.Models.Entities;
using Newtonsoft.Json;

namespace EmailClient.Models.ApiServices
{
    public class User1ApiServices
    {
        private readonly HttpClient httpClient;

        public User1ApiServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public HttpResponseMessage register(User1 user1)
        {
            string data = JsonConvert.SerializeObject(user1);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync(restUrl.URL + "user1/register", content).Result;
            return response;
        }
        public HttpResponseMessage verifyotp(VerifyOtpRequest verifyOtpRequest)
        {
            string data = JsonConvert.SerializeObject(verifyOtpRequest);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync(restUrl.URL + "user1/verify-otp", content).Result;
            return response;
        }
    }
}
