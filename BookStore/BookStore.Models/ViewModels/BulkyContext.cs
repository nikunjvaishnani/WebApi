using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookStore.Models.ViewModels
{
    public partial class BulkyContext : DbContext
    {
        public BulkyContext()
        {
        }

        public BulkyContext(DbContextOptions<BulkyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Orderdtl> Orderdtls { get; set; }
        public virtual DbSet<Ordermst> Ordermsts { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;User ID =postgres;Port=5432;Password=GJ03PG2511;Database=Bulky;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_India.1252");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.BookId).UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Book_Category");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Book_Publisher");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.CartId).UseIdentityAlwaysColumn();

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Cart_Book");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Cart_User");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Orderdtl>(entity =>
            {
                entity.ToTable("Orderdtl");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Orderdtls)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orderdtl_BookId_fkey");

                entity.HasOne(d => d.Ordermst)
                    .WithMany(p => p.Orderdtls)
                    .HasForeignKey(d => d.OrdermstId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orderdtl_OrdermstId_fkey");
            });

            modelBuilder.Entity<Ordermst>(entity =>
            {
                entity.ToTable("Ordermst");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ordermsts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ordermst_UserId_fkey");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.Property(e => e.PublisherId).UseIdentityAlwaysColumn();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_RoleId_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
