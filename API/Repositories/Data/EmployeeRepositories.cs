using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Repositories.Data
{
    public class EmployeeRepositories : GeneralRepository<Employee, string>
    {
        private MyContext _context;
        private DbSet<Employee> _employees;
        private DbSet<Account> _account;
        private DbSet<Profiling> _profile;
        private DbSet<Education> _educations;
        private DbSet<University> _universities;

        public EmployeeRepositories(MyContext context) : base(context)
        {
            _context = context;
            _employees = context.Set<Employee>();
            _account = context.Set<Account>();
            _profile = context.Set<Profiling>();
            _educations= context.Set<Education>();
            _universities= context.Set<University>();
        }

        public IEnumerable<MasterEmployeeVM> DetailEmployee()
        {
            var result = from u in _universities
                         join e in _educations on u.Id equals e.UniversityId
                         join p in _profile on e.Id equals p.EducationId
                         join a in _account on p.NIK equals a.NIK
                         join em in _employees on a.NIK equals em.NIK
                         select new MasterEmployeeVM {
                             NIK = em.NIK,
                             FullName = em.FirstName +" " +em.LastName,
                             Phone = em.Phone,
                             Gender = em.Gender.ToString(),
                             Email = em.Email,
                             BirthDate = em.BirthDate,
                             Salary = em.Salary,
                             GPA = e.GPA,
                             Degree = e.Degree,
                             UniversityName = u.Name
                         };
            return result;
        }        
    }
}
