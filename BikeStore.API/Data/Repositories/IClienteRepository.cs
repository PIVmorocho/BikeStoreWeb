using BikeStore.API.Models.Entities;

namespace BikeStore.API.Data.Repositories
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Task<Cliente?> GetByCedulaAsync(string cedula);
    }
}
