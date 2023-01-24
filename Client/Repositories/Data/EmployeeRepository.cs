using API.Models;
using API.ViewModels;
using Client.Base;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            /*httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
             * _contextAccessor.HttpContext.Session.GetString("JWToken"));*/
        }

        public HttpStatusCode Register(RegisterVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + "Accounts/Register", content).Result;
            return result.StatusCode;
        }        
    }
}
