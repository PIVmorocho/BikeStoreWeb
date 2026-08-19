using BikeStore.API.Data;
using BikeStore.API.Exceptions;
using BikeStore.API.Models.DTOs;
using BikeStore.API.Models.Entities;

namespace BikeStore.API.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<CategoriaDto>> GetAllAsync()
        {
            var categorias = await _unitOfWork.Categorias.GetAllAsync();
            return categorias.Select(MapToDto).ToList();
        }

        public async Task<CategoriaDto> GetByIdAsync(int id)
        {
            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id)
                ?? throw new NotFoundException($"No se encontro la categoria con id {id}.");
            return MapToDto(categoria);
        }

        public async Task<CategoriaDto> CreateAsync(CategoriaCreateDto dto)
        {
            var categoria = new Categoria
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Activo = dto.Activo
            };

            await _unitOfWork.Categorias.AddAsync(categoria);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(categoria);
        }

        public async Task<CategoriaDto> UpdateAsync(int id, CategoriaUpdateDto dto)
        {
            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id)
                ?? throw new NotFoundException($"No se encontro la categoria con id {id}.");

            categoria.Nombre = dto.Nombre;
            categoria.Descripcion = dto.Descripcion;
            categoria.Activo = dto.Activo;

            _unitOfWork.Categorias.Update(categoria);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(categoria);
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id)
                ?? throw new NotFoundException($"No se encontro la categoria con id {id}.");

            if (await _unitOfWork.Categorias.TieneBicicletasAsociadasAsync(id))
                throw new BusinessRuleException("No se puede eliminar la categoria porque tiene bicicletas asociadas.");

            _unitOfWork.Categorias.Remove(categoria);
            await _unitOfWork.SaveChangesAsync();
        }

        private static CategoriaDto MapToDto(Categoria categoria) => new()
        {
            IdCategoria = categoria.IdCategoria,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            Activo = categoria.Activo
        };
    }
}
