using Microsoft.EntityFrameworkCore;

namespace TPAdmissionTask.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserModel>? Users { get; set; }
        public virtual DbSet<RecordModel>? Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Users");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Name).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Phone).HasMaxLength(14).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Password).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Type).HasMaxLength(20).IsUnicode(false);
            });

            modelBuilder.Entity<RecordModel>(entity =>
            {
                entity.ToTable("Records");
                entity.Property(e => e.RecordId).HasColumnName("RecordId");
                entity.HasKey(e => e.RecordId);
                entity.Property(e => e.UserId).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Name).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Phone).HasMaxLength(14).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.ImgUrl).HasMaxLength(300).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
