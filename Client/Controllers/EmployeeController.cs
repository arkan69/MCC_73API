using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Repositories.Data;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeeController : /*Controller*/BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeeController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public JsonResult Register([FromBody] RegisterVM entity)
        {
            var result = repository.Register(entity);
            return Json(result);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
