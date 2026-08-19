using BikeStore.API.Models.Entities;

namespace BikeStore.API.Data.Repositories
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        Task<bool> TieneBicicletasAsociadasAsync(int idCategoria);
    }
}
