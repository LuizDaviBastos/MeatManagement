using MeatManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeatManager.Data.Data
{
    public class MeatManagerContext : DbContext
    {
        private string connectionString;
        public MeatManagerContext() { }

        public MeatManagerContext(string connectionString) 
        {
            this.connectionString = connectionString;
        }

        public DbSet<Meat> Meats { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Buyer>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Name).IsRequired().HasMaxLength(150);
                entity.HasIndex(b => b.Document).IsUnique();
                entity.Property(b => b.Document).IsRequired().HasMaxLength(20);
                entity.Property(b => b.DocumentType).HasConversion<string>().IsRequired();
                entity.Property(b => b.CreatedAt).IsRequired();
                entity.HasOne(b => b.Address)
                      .WithOne(a => a.Buyer)
                      .HasForeignKey<Address>(a => a.BuyerId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.UF).IsRequired().HasMaxLength(2);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(c => c.State)
                      .WithMany(s => s.Cities)
                      .HasForeignKey(c => c.StateId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasOne(a => a.City)
                      .WithMany()
                      .HasForeignKey(a => a.CityId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.State)
                      .WithMany()
                      .HasForeignKey(a => a.StateId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Meat>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
                modelBuilder.Entity<Meat>()
                .Property(m => m.Origin)
                .HasConversion<string>()
                .IsRequired();
                entity.Property(m => m.PricePerKg).IsRequired().HasPrecision(10, 2);
                entity.Property(m => m.WeightKg).IsRequired();
                entity.Property(m => m.CreatedAt).IsRequired();

                entity.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Meat_Price_Positive", "PricePerKg >= 0");
                    t.HasCheckConstraint("CK_Meat_Weight_Positive", "WeightKg >= 0");
                });
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.CreatedAt).IsRequired();
                entity.HasOne(o => o.Buyer)
                      .WithMany(b => b.Orders)
                      .HasForeignKey(o => o.BuyerId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.Id);
                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.Items)
                      .HasForeignKey(oi => oi.OrderId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Meat)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(oi => oi.MeatId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(oi => oi.QuantityKg).IsRequired();
                entity.Property(oi => oi.PricePerKg).IsRequired();
                entity.Property(oi => oi.Total).IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString, op => op.MigrationsAssembly("MeatManager.Data"));
            }
        }
    }
}
