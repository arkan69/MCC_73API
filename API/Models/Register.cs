using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("tb_m_registers")]
    public class Register
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("nik", TypeName = "nchar(5)")]
        public string NIK { get; set; }

        [Required, Column("full_name"), MaxLength(25)]
        public string FullName { get; set; }

        [Required, Column("phone"), MaxLength(25)]
        public string Phone { get; set; }

        [Required, Column("gender")]
        public Gender Gender { get; set; }

        [Required, Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Required, Column("salary")]
        public int Salary { get; set; }

        [Required, Column("email"), MaxLength(50)]
        public string Email { get; set; }
        
        [Required, Column("password")]
        public string Password { get; set; }

        //realtions
        // Relation
        [JsonIgnore]
        [ForeignKey("NIK")]
        public Account? Account { get; set; }

    }
}
