using System.ComponentModel.DataAnnotations;

namespace DevTrack.Web.Models;

// basic student profile linked 1:1 with app user
public class StudentProfile
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public AppUser? User { get; set; }

    [StringLength(120)]
    public string? ProgramName { get; set; }

    [StringLength(40)]
    public string? GraduationTerm { get; set; }

    [StringLength(500)]
    public string? Bio { get; set; }
}