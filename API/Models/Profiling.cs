using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_r_profile")]
public class Profiling
{
    [Key,Column("nik", TypeName = "nchar(5)")]
    public string NIK { get; set; }

    [Required,Column("education_id")]
    public int EducationId { get; set; }

    // Relation
    [ForeignKey("EducationId")]
    [JsonIgnore]
    public Education? Education { get; set; }

    [JsonIgnore]
    public Account? Account { get; set; }
}
