using Microsoft.EntityFrameworkCore;
using LibraryManagement.DAL.Entities;

namespace LibraryManagement.DAL.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Fine> Fines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.BookId);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b => b.ISBN).HasMaxLength(20);
                entity.Property(b => b.Publisher).HasMaxLength(100);

                entity.HasOne(b => b.Category)
                      .WithMany(c => c.Books)
                      .HasForeignKey(b => b.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Author)
                      .WithMany(a => a.Books)
                      .HasForeignKey(b => b.AuthorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryId);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.AuthorId);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Email).HasMaxLength(100);
                entity.Property(a => a.Country).HasMaxLength(50);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.MemberId);
                entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Email).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Phone).HasMaxLength(20);
                entity.Property(m => m.MembershipType).HasMaxLength(20);
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(l => l.LoanId);
                entity.Property(l => l.Status).HasMaxLength(20);

                entity.HasOne(l => l.Book)
                      .WithMany(b => b.Loans)
                      .HasForeignKey(l => l.BookId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Member)
                      .WithMany(m => m.Loans)
                      .HasForeignKey(l => l.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Fine>(entity =>
            {
                entity.HasKey(f => f.FineId);
                entity.Property(f => f.Amount).HasColumnType("decimal(10,2)");
                entity.Property(f => f.Reason).HasMaxLength(200);

                entity.HasOne(f => f.Loan)
                      .WithMany(l => l.Fines)
                      .HasForeignKey(f => f.LoanId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Member)
                      .WithMany(m => m.Fines)
                      .HasForeignKey(f => f.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
