using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class RoleRepositories : GeneralRepository<Role,int>
    {
        public RoleRepositories(MyContext context) : base(context)
        {
        }
    }
}
