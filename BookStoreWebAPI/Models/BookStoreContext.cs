using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebAPI.Models;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Author { get; set; }

    public virtual DbSet<Book> Book { get; set; }

    public virtual DbSet<BookAuthor> BookAuthor { get; set; }

    public virtual DbSet<Job> Job { get; set; }

    public virtual DbSet<Publisher> Publisher { get; set; }

    public virtual DbSet<RefreshToken> RefreshToken { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<Sale> Sale { get; set; }

    public virtual DbSet<Store> Store { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=CO3182\\SQLEXPRESS;Database=BookStore;Trusted_Connection=True; Integrated Security= True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Author__86516BCF2E5EF4CB");

            entity.ToTable("Author");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Address)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasDefaultValue("UNKNOWN")
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("state");
            entity.Property(e => e.Zip)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("zip");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__490D1AE1C20200DE");

            entity.ToTable("Book");

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Advance)
                .HasColumnType("money")
                .HasColumnName("advance");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("notes");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.PubId).HasColumnName("pub_id");
            entity.Property(e => e.PublishedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("published_date");
            entity.Property(e => e.Royalty).HasColumnName("royalty");
            entity.Property(e => e.Title)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasDefaultValue("UNDECIDED")
                .IsFixedLength()
                .HasColumnName("type");
            entity.Property(e => e.YtdSales).HasColumnName("ytd_sales");

            entity.HasOne(d => d.Pub).WithMany(p => p.Books)
                .HasForeignKey(d => d.PubId)
                .HasConstraintName("FK__Book__pub_id__6383C8BA");
        });

        modelBuilder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(e => new { e.AuthorId, e.BookId });

            entity.ToTable("BookAuthor");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.AuthorOrder).HasColumnName("author_order");
            entity.Property(e => e.RoyalityPercentage).HasColumnName("royality_percentage");

            entity.HasOne(d => d.Author).WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__BookAutho__autho__6477ECF3");

            entity.HasOne(d => d.Book).WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__BookAutho__book___656C112C");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Job__6E32B6A55855AA6F");

            entity.ToTable("Job");

            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.JobDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("New Position - title not formalized yet")
                .HasColumnName("job_desc");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PubId).HasName("PK__Publishe__2515F222F2BD9A6D");

            entity.ToTable("Publisher");

            entity.Property(e => e.PubId).HasColumnName("pub_id");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("USA")
                .HasColumnName("country");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("publisher_name");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("state");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.TokenId);

            entity.ToTable("RefreshToken");

            entity.Property(e => e.TokenId).HasColumnName("token_id");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("datetime")
                .HasColumnName("expiry_date");
            entity.Property(e => e.Token)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RefreshTo__user___66603565");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CC13B58FAA");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("New Position - title not formalized yet")
                .HasColumnName("role_desc");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK_Sale2");

            entity.ToTable("Sale");

            entity.Property(e => e.SaleId).HasColumnName("sale_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderNum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("order_num");
            entity.Property(e => e.PayTerms)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("pay_terms");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StoreId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("store_id");

            entity.HasOne(d => d.Book).WithMany(p => p.Sales)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Sale__book_id__6754599E");

            entity.HasOne(d => d.Store).WithMany(p => p.Sales)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Sale__store_id__68487DD7");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("UPK_storeid");

            entity.ToTable("Store");

            entity.Property(e => e.StoreId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("store_id");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("state");
            entity.Property(e => e.StoreAddress)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("store_address");
            entity.Property(e => e.StoreName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("store_name");
            entity.Property(e => e.Zip)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("zip");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId)
                .HasName("PK_user_id_2")
                .IsClustered(false);

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("middle_name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PubId)
                .HasDefaultValue(1)
                .HasColumnName("pub_id");
            entity.Property(e => e.RoleId)
                .HasDefaultValue((short)1)
                .HasColumnName("role_id");
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("source");

            entity.HasOne(d => d.Pub).WithMany(p => p.Users)
                .HasForeignKey(d => d.PubId)
                .HasConstraintName("FK__User__pub_id__6A30C649");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__role_id__693CA210");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
