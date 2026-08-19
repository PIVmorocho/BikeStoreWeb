using BikeStore.API.Data.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace BikeStore.API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BikeStoreDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(BikeStoreDbContext context)
        {
            _context = context;
            Categorias = new CategoriaRepository(_context);
            Bicicletas = new BicicletaRepository(_context);
            Clientes = new ClienteRepository(_context);
            Ventas = new VentaRepository(_context);
        }

        public ICategoriaRepository Categorias { get; }
        public IBicicletaRepository Bicicletas { get; }
        public IClienteRepository Clientes { get; }
        public IVentaRepository Ventas { get; }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction is null) return;
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is null) return;
            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction is not null)
                await _transaction.DisposeAsync();
            await _context.DisposeAsync();
        }
    }
}
