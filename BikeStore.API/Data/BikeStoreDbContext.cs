using BikeStore.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.API.Data
{
    public class BikeStoreDbContext : DbContext
    {
        public BikeStoreDbContext(DbContextOptions<BikeStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Bicicleta> Bicicletas => Set<Bicicleta>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Venta> Ventas => Set<Venta>();
        public DbSet<DetalleVenta> DetallesVenta => Set<DetalleVenta>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("Categoria");
                entity.HasKey(e => e.IdCategoria);
                entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Descripcion).HasMaxLength(255);
                entity.Property(e => e.Activo).HasDefaultValue(true);
            });

            modelBuilder.Entity<Bicicleta>(entity =>
            {
                entity.ToTable("Bicicleta");
                entity.HasKey(e => e.IdBicicleta);
                entity.Property(e => e.Marca).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Modelo).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Precio).HasPrecision(10, 2);
                entity.Property(e => e.Estado).HasMaxLength(20).HasDefaultValue("Activo");

                entity.HasOne(e => e.Categoria)
                    .WithMany(c => c.Bicicletas)
                    .HasForeignKey(e => e.IdCategoria)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");
                entity.HasKey(e => e.IdCliente);
                entity.Property(e => e.Cedula).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Nombres).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Apellidos).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Telefono).HasMaxLength(20);
                entity.Property(e => e.Correo).HasMaxLength(150);
                entity.HasIndex(e => e.Cedula).IsUnique();
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("Venta");
                entity.HasKey(e => e.IdVenta);
                entity.Property(e => e.Total).HasPrecision(10, 2);

                entity.HasOne(e => e.Cliente)
                    .WithMany(c => c.Ventas)
                    .HasForeignKey(e => e.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.ToTable("DetalleVenta");
                entity.HasKey(e => e.IdDetalle);
                entity.Property(e => e.Precio).HasPrecision(10, 2);
                entity.Property(e => e.Subtotal).HasPrecision(10, 2);

                entity.HasOne(e => e.Venta)
                    .WithMany(v => v.Detalles)
                    .HasForeignKey(e => e.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Bicicleta)
                    .WithMany(b => b.DetallesVenta)
                    .HasForeignKey(e => e.IdBicicleta)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
