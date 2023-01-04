using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("tb_m_accounts")]
[Index(nameof(NIK), IsUnique = true)]
public class Account
{
    [Key, Column("nik", TypeName = "nchar(5)")]
    public string NIK { get; set; }

    [Required,Column("password")]
    public string Password { get; set; }

    public int OTP { get; set; }
    public DateTime ExpiredToken { get; set; }
    public bool IsUsed { get; set; }

    // Relation
    [JsonIgnore]
    public Employee? Employee { get; set; }

    [JsonIgnore]
    public Profiling? Profiling { get; set; }
    
    [JsonIgnore]
    public Register? Register { get; set; }

    [JsonIgnore]
    public ICollection<AccountRole>? AccountRoles { get; set; }
}
