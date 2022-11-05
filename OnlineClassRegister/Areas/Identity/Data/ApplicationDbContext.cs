﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineClassRegister.Areas.Identity.Data;

namespace OnlineClassRegister.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<OnlineClassRegisterUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<OnlineClassRegisterUser>
{
    public void Configure(EntityTypeBuilder<OnlineClassRegisterUser> builder)
    {
        builder.Property(u => u.FirstName).IsRequired();
        builder.Property(u => u.FirstName).IsRequired();
    }
}