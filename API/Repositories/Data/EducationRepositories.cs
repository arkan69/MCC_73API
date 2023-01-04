using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class EducationRepositories : GeneralRepository<Education, int>
    {
        public EducationRepositories(MyContext context) : base(context)
        {
        }
    }
}
