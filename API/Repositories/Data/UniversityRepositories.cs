using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class UniversityRepositories : GeneralRepository<University,int>
    {
        public UniversityRepositories(MyContext context) : base(context)
        {
        }
    }
}
