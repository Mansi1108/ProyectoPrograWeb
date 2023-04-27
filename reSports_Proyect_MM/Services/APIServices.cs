using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using reSportsModel.Auth;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;

namespace reSports_Proyect_MM.Services
{
    public class APIServices
    {
        private readonly int Timeout = 30;
        private string Url = default!;
        private readonly HttpStatusCode[] ErrorCodes = new[] { HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError };
        private static HttpClientHandler _clientHandler = new();
        private static HttpClient _client = new();
        public static string token = "";
        public static string urlLogin = "https://localhost:7062/api/";

        public APIServices SetModule(string controllerName)
        {
            Url = $"https://localhost:7062/api/{controllerName}/";
            return this;
        }

        public async Task<T?> Get<T>(string path = "")
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (!token.IsNullOrEmpty())
            {
                _client.DefaultRequestHeaders.Authorization = null;
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
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
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (!token.IsNullOrEmpty())
            {
                _client.DefaultRequestHeaders.Authorization = null;
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
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
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (!token.IsNullOrEmpty())
            {
                _client.DefaultRequestHeaders.Authorization = null;
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
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
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (!token.IsNullOrEmpty())
            {
                _client.DefaultRequestHeaders.Authorization = null;
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
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

        private static async Task<T> Post2<T>(string path, object? data)
        {
            var json_ = JsonConvert.SerializeObject(data);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (!token.IsNullOrEmpty())
            {
                _client.DefaultRequestHeaders.Authorization = null;
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            var response = await _client.PostAsync(path, content);
            int statusCode = (int)response.StatusCode;
            if (statusCode >= 200 && statusCode < 300)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        //login
        public static async Task<UserToken?> Login(UserAuth credentials)
        {
            return await Post2<UserToken?>(urlLogin + "auth/login", credentials);
        }

        public static async Task<UserToken?> Register(reSportsModel.Register personalInformation)
        {
            return await Post2<UserToken?>(urlLogin + "auth/register", personalInformation);
        }
    }
}
