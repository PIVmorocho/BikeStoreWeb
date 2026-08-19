using BikeStore.API.Data.Repositories;

namespace BikeStore.API.Data
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICategoriaRepository Categorias { get; }
        IBicicletaRepository Bicicletas { get; }
        IClienteRepository Clientes { get; }
        IVentaRepository Ventas { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
