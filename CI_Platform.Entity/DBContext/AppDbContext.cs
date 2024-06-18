using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<LoginCarousel> LoginCarousels { get; set; }

        public DbSet<Mission> Missions { get; set; }

        public DbSet<Theme> Themes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<MissionMedia> MissionMedias { get; set; }

        public DbSet<MissionApplication> MissionApplications { get; set; }

        public DbSet<RecentVolunteer> RecentVolunteers { get; set; }

        public DbSet<Story> Stories { get; set; }

        public DbSet<VolunteeringTimesheet> VolunteeringTimesheets { get; set; }

        public DbSet<StoryMedia> StoryMedias { get; set; }

        public DbSet<CMSPrivacyPolicy> CMSPrivacyPolicies { get; set; }

        public DbSet<UserMission> UserMissions { get; set; }

        public DbSet<MissionSkill> MissionSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Status)
                .HasDefaultValue(1);
            });

            modelBuilder.Entity<Admin>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<LoginCarousel>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Mission>(entity =>
                {
                    entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    entity.Property(e => e.MissionRating).HasDefaultValue(0);
                    entity.Property(e => e.MissionRatingCount).HasDefaultValue(0);
                    entity.Property(e => e.OccupiedSeats).HasDefaultValue(0);
                    entity.Property(e => e.OccupiedSeats).HasDefaultValue(0);
                });

            modelBuilder.Entity<UserMission>(entity =>
            {
                
                entity.Property(e => e.UserStatus).HasDefaultValue(1);
                entity.Property(e => e.Favourite).HasDefaultValue(0);
            });

            modelBuilder.Entity<Comment>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<MissionApplication>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        }
    }
}
