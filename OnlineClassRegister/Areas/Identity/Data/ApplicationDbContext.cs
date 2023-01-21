using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineClassRegister.Areas.Identity.Data;
using OnlineClassRegister.Models;

namespace OnlineClassRegister.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<OnlineClassRegisterUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Subject> Subject { get; set; }

    public DbSet<Student> Student { get; set; }

    public DbSet<StudentClass> StudentClass { get; set; }

    public DbSet<Teacher> Teacher { get; set; }

    public DbSet<Grade> Grade { get; set; }
    public DbSet<Message> Message { get; set; }

    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<AnnouncementReceiver> AnnouncementReceivers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Student>().ToTable("Student");
        builder.Entity<Grade>().ToTable("Grade");
        builder.Entity<StudentClass>().ToTable("StudentClass");
        builder.Entity<Subject>().ToTable("Subject");
        builder.Entity<Teacher>().ToTable("Teacher");
        builder.Entity<Message>().ToTable("Message");

        builder.Entity<Subject>()
            .HasMany<Teacher>(s => s.teachers)
            .WithMany(t => t.subjects)
            .UsingEntity(j => j.ToTable("SubjectTeacher"));

        builder.Entity<Teacher>()
            .HasMany<Subject>(t => t.subjects)
            .WithMany(s => s.teachers)
            .UsingEntity(j => j.ToTable("SubjectTeacher"));

        builder.Entity<Student>()
            .HasOne(s => s.studentClass)
            .WithMany(sc => sc.students);

        builder.Entity<StudentClass>()
            .HasMany<Subject>(t => t.subjects)
            .WithMany(s => s.classes)
            .UsingEntity(j => j.ToTable("ClassSubject"));

        builder.Entity<StudentClass>()
            .HasOne(sc => sc.classTutor)
            .WithOne(t => t.classTutoring)
            .HasForeignKey<Teacher>(t => t.classTutoringId);

        builder.Entity<AnnouncementReceiver>()
            .HasKey(ar => new { ar.AnnouncementId, ar.ReceiverId });

        builder.Entity<AnnouncementReceiver>()
            .HasOne(ar => ar.Announcement)
            .WithMany(a => a.Receivers)
            .HasForeignKey(ar => ar.AnnouncementId);

        builder.Entity<AnnouncementReceiver>()
            .HasOne(ar => ar.Receiver)
            .WithMany()
            .HasForeignKey(ar => ar.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<OnlineClassRegisterUser>
{
    public void Configure(EntityTypeBuilder<OnlineClassRegisterUser> builder)
    {
        builder.Property(u => u.FirstName).IsRequired();
        builder.Property(u => u.LastName).IsRequired();
    }
}