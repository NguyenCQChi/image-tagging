using authentication.Models;
using authentication.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace authentication.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<RefreshToken>? RefreshTokens { get; set; }
    public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
    private readonly List<string> _roles;
    private readonly List<RegistrationRequestDto> _admins;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
    {
        _roles = configuration.GetSection("ApiSettings:Roles").Get<List<string>>()!;
        _admins = configuration.GetSection("ApiSettings:Admins").Get<List<RegistrationRequestDto>>()!;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var roleIds = new Dictionary<string, string>();
        foreach (var role in _roles)
        {
            var id = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = id,
                    ConcurrencyStamp = id,
                    Name = role.ToLower(),
                    NormalizedName = role.ToUpper()
                });
            roleIds.Add(role, id);
        }

        foreach (var admin in _admins)
        {
            var id = Guid.NewGuid().ToString();
            var appUser = new ApplicationUser 
            { 
                Id = id,
                UserName = admin.UserName, 
                Email = admin.Email, 
                NormalizedEmail = admin.Email!.ToUpper(), 
                NormalizedUserName = admin.UserName!.ToUpper(), 
                Name = admin.Name
            };
            var ph = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, admin.Password!);
            modelBuilder.Entity<ApplicationUser>().HasData(appUser);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = roleIds[admin.Role!],
                    UserId = id
                });
        }
    }
    
    
}