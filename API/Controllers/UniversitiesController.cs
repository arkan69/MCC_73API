using API.Base;
using API.Contexts;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Manager")]//untuk meng autorisasi user
    public class UniversitiesController : BaseController<University,int,UniversityRepositories>
    {
        private UniversityRepositories _urepositories;

        public UniversitiesController(UniversityRepositories urepositories) : base(urepositories)
        {
            _urepositories = urepositories;
        }

        //[AllowAnonymous]//untuk membuat method dapat diakses tanpa autorsasi
        
    }
}
