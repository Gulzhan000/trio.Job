using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Common;
using Domain.Entities;

namespace Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        UserProfiles = Set<UserProfile>();
    }

    // Добавляем DbSet для UserProfile
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Можно добавить конфигурацию таблицы (например, имя)
        modelBuilder.Entity<UserProfile>().ToTable("UserProfiles");

        // Если есть связи, их можно настроить здесь
    }
}

