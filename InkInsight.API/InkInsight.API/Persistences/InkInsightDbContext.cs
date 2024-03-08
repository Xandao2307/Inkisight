using InkInsight.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace InkInsight.API.Persistences
{
    public class InkInsightDbContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public InkInsightDbContext(DbContextOptions<InkInsightDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>(e =>
            {
                e.HasKey(r => r.Id);
                e.Property(r => r.Rating).IsRequired(false);
                e.Property(r => r.Text)
                    .HasMaxLength(300)
                    .HasColumnType("varchar(300)")
                    .IsRequired(false);
                e.Property(r => r.Date).HasColumnName("date_review");

                e.HasOne(r => r.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.UserId)
                    .IsRequired();


                e.HasOne(r => r.Book)
                    .WithMany(b => b.Reviews)
                    .HasForeignKey(r => r.BookId)
                    .IsRequired();
            });

            modelBuilder.Entity<Book>(e =>
            {
                e.HasKey(b => b.Id);
                e.HasMany(b => b.Reviews)
                    .WithOne(r => r.Book)
                    .HasForeignKey(r => r.BookId);

            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);
                e.Property(u => u.Password).IsRequired(false);
                e.Property(u => u.Email).IsRequired(false);
                e.HasMany(u => u.Reviews)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId);


            });
        }

    }
}
