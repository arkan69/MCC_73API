using API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class MasterEmployeeVM
    {
        public string NIK { get; set; }
        public string FullName { get; set; }
        //public string LastName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public int EducationId { get; set; }
        public string GPA { get; set; }
        public string Degree { get; set; }
        public string UniversityName { get; set; }       

    }
}
