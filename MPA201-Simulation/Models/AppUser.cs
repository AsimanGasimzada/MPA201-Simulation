using Microsoft.AspNetCore.Identity;

namespace MPA201_Simulation.Models;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; } = null!;
}
