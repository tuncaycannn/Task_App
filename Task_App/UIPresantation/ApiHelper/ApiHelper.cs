using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UIPresantation.Models;

namespace UIPresantation.ApiHelper
{
    public class ApiHelper
    {
        public static string apiUrl { get; set; }
        IHttpClientFactory httpClientFactory;
        public ApiHelper(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
            var configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            apiUrl = configurationBuilder.Build().GetSection("ApiUrl:URL").Value;
        }

        public async Task<AccessTokenVM> UserAdd(UserForRegisterVM register)
        {
            RestClientHelper client = new RestClientHelper(httpClientFactory);
            var result = await client.PostAsync(apiUrl + "/Auth/register", register, null);
            result = result.TrimStart('\"');
            result = result.TrimEnd('\"');
            result = result.Replace("\\", "");
            var accessToken = JsonConvert.DeserializeObject<AccessTokenVM>(result);

            return accessToken;
        }
        public async Task<AccessTokenVM> UserLogin(UserForLoginVM login)
        {
            RestClientHelper client = new RestClientHelper(httpClientFactory);
            var result = await client.PostAsync(apiUrl + "/Auth/login", login, null);
            result = result.TrimStart('\"');
            result = result.TrimEnd('\"');
            result = result.Replace("\\", "");
            var accessToken = JsonConvert.DeserializeObject<AccessTokenVM>(result);

            return accessToken;
        }
        public async Task<UserForUpdate> UserUpdate(UserVM user)
        {
            UserForUpdate userForUpdate = new UserForUpdate();
            RestClientHelper client = new RestClientHelper(httpClientFactory);
            var result = await client.PutAsync(apiUrl + "/Users/update", user, null);
            result = result.TrimStart('\"');
            result = result.TrimEnd('\"');
            result = result.Replace("\\", "");
            userForUpdate = JsonConvert.DeserializeObject<UserForUpdate>(result);

            return userForUpdate;
        }
        public async Task<UserDeleteVM> UserDelete(int id, string jwt)
        {
            UserDeleteVM userDeleteVM = new UserDeleteVM();
            RestClientHelper client = new RestClientHelper(httpClientFactory);
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("id", id.ToString());
            var result = await client.DeleteAsync(apiUrl + "/Users/delete" + QueryStringHelper.ToQueryString(parameters), jwt, null);
            result = result.TrimStart('\"');
            result = result.TrimEnd('\"');
            result = result.Replace("\\", "");
            userDeleteVM = JsonConvert.DeserializeObject<UserDeleteVM>(result);

            return userDeleteVM;
        }
        public async Task<GetByIdUserResponseVM> GetUserById(long id)
        {
            RestClientHelper client = new RestClientHelper(httpClientFactory);
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("id", id.ToString());

            var result = await client.GetAsync(apiUrl + "/Users/getbyid" + QueryStringHelper.ToQueryString(parameters), null);
            result = result.TrimStart('\"');
            result = result.TrimEnd('\"');
            result = result.Replace("\\", "");

            var template = JsonConvert.DeserializeObject<GetByIdUserResponseVM>(result);

            return template;
        }
        public async Task<UserResponseVM> GetAllUser()
        {
            RestClientHelper client = new RestClientHelper(httpClientFactory);
            var result = await client.GetAsync(apiUrl + "/Users/getall", null);
            result = result.TrimStart('\"');
            result = result.TrimEnd('\"');
            result = result.Replace("\\", "");
            var parameters = JsonConvert.DeserializeObject<UserResponseVM>(result);

            return parameters;
        }
    }
}
