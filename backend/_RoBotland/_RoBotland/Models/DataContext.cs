using Microsoft.EntityFrameworkCore;

namespace _RoBotland.Models
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Product>()
                .Property(x => x.IsAvailable)
                .HasConversion<string>();
            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Order>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<OrderDetails>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<OrderDetails>().Property(x => x.Quantity).IsRequired(true);
            modelBuilder.Entity<OrderDetails>().Property(x => x.Total).IsRequired(true);
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<UserDetails>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Order>()
                .Property(x => x.OrderStatus)
                .HasConversion<string>();
            modelBuilder.Entity<Product>()
                .HasMany(x => x.Categories)
                .WithMany(x => x.Products).UsingEntity("ProductCategory");
            modelBuilder.Entity<Product>().HasMany(x => x.OrderDetails)
                .WithOne(x => x.Product).HasForeignKey(x=>x.ProductId);
            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderDetails)
                .WithOne(x => x.Order)
                .HasForeignKey(x=>x.OrderId);
            modelBuilder.Entity<UserDetails>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.UserDetails)
                .HasForeignKey(x=>x.UserDetailsId);
            modelBuilder.Entity<UserDetails>()
                .HasOne(x => x.User)
                .WithOne(x => x.UserDetails)
                .HasForeignKey<UserDetails>(x=>x.Id)
                .IsRequired(false);

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
    }
}
