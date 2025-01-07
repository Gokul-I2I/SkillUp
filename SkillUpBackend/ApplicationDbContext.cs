using Microsoft.EntityFrameworkCore;
using SkillUpBackend.Model;
using StreamModel = SkillUpBackend.Model.StreamModel;

namespace SkillUpBackend
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Subtopic> Subtopics { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }

        public DbSet<UserSubtopic> UserSubtopics { get; set; }
        public DbSet<StreamModel> Streams { get; set; }
        public DbSet<Batch> Batches { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserSubtopic>()
       .HasKey(us => new { us.UserId, us.SubtopicId }); // Ensure composite key is defined.

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
            modelBuilder.Entity<UserSubtopic>()
                .Property(us => us.State)
                .HasConversion<string>();

            // Parent-Child: SubTopic -> SubTask
            modelBuilder.Entity<Subtopic>()
                .HasMany(st => st.SubTasks)
                .WithOne(sts => sts.SubTopic)
                .HasForeignKey(sts => sts.SubTopicId)
                .OnDelete(DeleteBehavior.Cascade);

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

            // Batch-user 
            modelBuilder.Entity<BatchUser>()
           .HasKey(bu => new { bu.UserId, bu.BatchId });

            modelBuilder.Entity<BatchUser>()
                .HasOne(bs => bs.Batch)
                .WithMany(b => b.BatchUsers)
                .HasForeignKey(bs => bs.BatchId);
            // Batch-StreamModel mapping
            modelBuilder.Entity<BatchStream>()
                .HasKey(bs => new { bs.BatchId, bs.StreamId }); 

            modelBuilder.Entity<BatchStream>()
                .HasOne(bs => bs.Batch)
                .WithMany(b => b.BatchStreams)
                .HasForeignKey(bs => bs.BatchId); 
        }
    }
}
