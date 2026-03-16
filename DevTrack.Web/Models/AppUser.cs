using Microsoft.AspNetCore.Identity;

namespace DevTrack.Web.Models;

// app user for identity + student/admin roles
public class AppUser : IdentityUser
{
    public string? DisplayName { get; set; }

    public StudentProfile? Profile { get; set; }
}