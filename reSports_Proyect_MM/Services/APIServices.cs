using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Text;

namespace reSports_Proyect_MM.Services
{
    public class APIServices
    {
        private readonly int Timeout = 30;
        private string Url = default!;
        private readonly HttpStatusCode[] ErrorCodes = new[] { HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError };

        public APIServices SetModule(string controllerName)
        {
            Url = $"https://localhost:7062/api/{controllerName}/";
            return this;
        }

        public async Task<T?> Get<T>(string path = "")
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(Timeout)
            };

            var response = await httpClient.GetAsync(Url + path);
            if (ErrorCodes.Contains(response.StatusCode))
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T?> Post<T>(T content, string path = "")
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(Timeout)
            };

            var json = JsonConvert.SerializeObject(content);
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(Url + path, jsonContent);
            if (ErrorCodes.Contains(response.StatusCode))
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T?> Put<T>(T content, string path = "")
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(Timeout)
            };

            var json = JsonConvert.SerializeObject(content);
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(Url + path, jsonContent);
            if (ErrorCodes.Contains(response.StatusCode))
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task Delete(string path = "")
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(Timeout)
            };

            var response = await httpClient.DeleteAsync(Url + path);
            if (ErrorCodes.Contains(response.StatusCode))
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}
