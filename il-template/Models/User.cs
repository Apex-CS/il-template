using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace il_template.Models;

[Index(nameof(Email))]
public class User
{
    [Key] public int Id { get; set; }
    [StringLength(255)] public required string FirstName { get; set; }
    [StringLength(255)] public required string LastName { get; set; }
    [StringLength(255)] public required string Email { get; set; }
}
