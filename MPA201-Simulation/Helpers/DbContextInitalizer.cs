using Microsoft.AspNetCore.Identity;
using MPA201_Simulation.Models;
using MPA201_Simulation.ViewModels.UserViewModels;

namespace MPA201_Simulation.Helpers;

public class DbContextInitalizer
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly AdminVM _admin;

    public DbContextInitalizer(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _configuration = configuration;

        _admin = _configuration.GetSection("AdminSettings").Get<AdminVM>() ?? new();
    }

    public async Task InitDatabaseAsync()
    {
        await CreateRolesAsync();
        await CreateAdminAsync();
    }

    private async Task CreateAdminAsync()
    {
        var admin = new AppUser()
        {
            Email = _admin.Email,
            UserName = _admin.UserName,
            Fullname = _admin.Fullname
        };

        var result = await _userManager.CreateAsync(admin, _admin.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(admin, "Admin");
        }
    }

    private async Task CreateRolesAsync()
    {
        await _roleManager.CreateAsync(new() { Name = "Admin" });
        await _roleManager.CreateAsync(new() { Name = "Member" });
        await _roleManager.CreateAsync(new() { Name = "Moderator" });
        await _roleManager.CreateAsync(new() { Name = "NazarataNazarat" });
    }


}
