﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_m_university")]
public class University
{
    [Key,Column("id")]
    public int Id { get; set; }

    [Required, Column("name"),MaxLength(50)]
    public string Name { get; set; }

    // Relation
    [JsonIgnore]
    public ICollection<Education>? Educations { get; set; }
}
