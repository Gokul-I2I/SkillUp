using Microsoft.EntityFrameworkCore;
using SkillUpBackend.Model;
using StreamModel = SkillUpBackend.Model.StreamModel;

namespace SkillUpBackend
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Subtopic> Subtopics { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<StreamModel> Streams { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Role>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.Role) 
            //    .WithMany(r => r.Users) 
            //    .HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<Batch>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StreamModel>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
            // User-Batch mapping
            modelBuilder.Entity<BatchUser>()
                .HasKey(ub => new { ub.UserId, ub.BatchId }); 

            modelBuilder.Entity<BatchUser>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.BatchUsers)
                .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<BatchUser>()
                .HasOne(ub => ub.Batch)
                .WithMany(b => b.BatchUsers)
                .HasForeignKey(ub => ub.BatchId);

            // Batch-StreamModel mapping
            modelBuilder.Entity<BatchStream>()
                .HasKey(bs => new { bs.BatchId, bs.StreamId }); // Composite Key

            modelBuilder.Entity<BatchStream>()
                .HasOne(bs => bs.Batch)
                .WithMany(b => b.BatchStreams)
                .HasForeignKey(bs => bs.BatchId);

            modelBuilder.Entity<BatchStream>()
                .HasOne(bs => bs.Stream)
                .WithMany(s => s.BatchStreams)
                .HasForeignKey(bs => bs.StreamId);
        }
        public DbSet<SkillUpBackend.Model.Batch> Batch { get; set; } = default!;
    }
}
