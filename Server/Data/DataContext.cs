using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThemeProgramme>()
                .HasKey(tp => new {tp.ThemeId, tp.ProgrammeId ,tp.SemesterId});
            modelBuilder.Entity<ThemeProgramme>()
                .HasOne(tp => tp.Semester)
                .WithMany(s => s.ThemeProgrammes)
                .HasForeignKey(tp => tp.SemesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Programme)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.ProgrammeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Theme>()
                .HasOne(t => t.Instructor)
                .WithMany(i => i.Themes)
                .HasForeignKey(u => u.InstructorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Theme>()
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId);

            modelBuilder.Entity<Theme>()
                .HasOne(t => t.StudentProgramme)
                .WithMany()
                .HasForeignKey(t => t.StudentProgrammeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Programme>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasIndex(s => new { s.SemesterId, s.ProgrammeId, s.NeptunCode }).IsUnique();
            });

            modelBuilder.Entity<Status>().HasData(
                    new Status
                    {
                        Id = 1,
                        Name = "Aktív"
                    },
                    new Status
                    {
                        Id = 2,
                        Name = "Inaktív"
                    }
                );
            modelBuilder.Entity<Approval>().HasData(
                    new Approval
                    {
                        Id = 1,
                        Name = "Jóváhagyásra vár"
                    },
                    new Approval
                    {
                        Id = 2,
                        Name = "Jóváhagyva"
                    },
                    new Approval
                    {
                        Id = 3,
                        Name = "Elutasítva"
                    }
                );

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Instructor",
                    DisplayName = "Oktató"
                },
                new Role
                {
                    Id = 2,
                    Name = "Programme Leader",
                    DisplayName = "Szakfelelős"
                },
                new Role
                {
                    Id = 3,
                    Name = "Head of department",
                    DisplayName = "Tanszéki admin"
                },
                new Role
                {
                    Id = 4,
                    Name = "Admin",
                    DisplayName = "Admin"
                }
            );

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Code)
                .IsUnique();

            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users);
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<Approval>? Approvals { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Programme>? Programmes { get; set; }
        public DbSet<Semester>? Semesters { get; set; }
        public DbSet<Status>? Statuses { get; set; }
        public DbSet<Theme>? Themes { get; set; }
        public DbSet<ThemeProgramme>? ThemeProgrammes { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Student>? Students { get; set; }
    }
}
