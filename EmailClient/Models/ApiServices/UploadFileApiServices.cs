using System.Net.Http;
using System.Net.Http.Headers;
using EmailClient.Models.Entities;
using Newtonsoft.Json;

namespace EmailClient.Models.ApiServices
{
    public class UploadFileApiServices
    {
        private readonly HttpClient httpClient;

        public UploadFileApiServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public HttpResponseMessage UploadFile(IFormFile file)
        {
            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.FileName);

                HttpResponseMessage response = httpClient.PostAsync(restUrl.URL + "Fileupload/upload", content).Result;
                return response;
            }
        }
    }
}
