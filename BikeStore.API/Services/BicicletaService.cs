using BikeStore.API.Configuration;
using BikeStore.API.Data;
using BikeStore.API.Data.Repositories;
using BikeStore.API.Exceptions;
using BikeStore.API.Models.DTOs;
using BikeStore.API.Models.Entities;
using Microsoft.Extensions.Options;

namespace BikeStore.API.Services
{
    public class BicicletaService : IBicicletaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly InventarioSettings _inventarioSettings;

        public BicicletaService(IUnitOfWork unitOfWork, IOptions<InventarioSettings> inventarioSettings)
        {
            _unitOfWork = unitOfWork;
            _inventarioSettings = inventarioSettings.Value;
        }

        public async Task<IReadOnlyList<BicicletaDto>> BuscarAsync(BicicletaFiltro filtro)
        {
            var bicicletas = await _unitOfWork.Bicicletas.BuscarAsync(filtro, _inventarioSettings.StockBajoUmbral);
            return bicicletas.Select(MapToDto).ToList();
        }

        public async Task<BicicletaDto> GetByIdAsync(int id)
        {
            var bicicleta = await _unitOfWork.Bicicletas.GetByIdConCategoriaAsync(id)
                ?? throw new NotFoundException($"No se encontro la bicicleta con id {id}.");
            return MapToDto(bicicleta);
        }

        public async Task<BicicletaDto> CreateAsync(BicicletaCreateDto dto)
        {
            if (await _unitOfWork.Categorias.GetByIdAsync(dto.IdCategoria) is null)
                throw new NotFoundException($"No se encontro la categoria con id {dto.IdCategoria}.");

            var bicicleta = new Bicicleta
            {
                IdCategoria = dto.IdCategoria,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Precio = dto.Precio,
                Stock = dto.Stock,
                Estado = dto.Estado
            };

            await _unitOfWork.Bicicletas.AddAsync(bicicleta);
            await _unitOfWork.SaveChangesAsync();

            var creada = await _unitOfWork.Bicicletas.GetByIdConCategoriaAsync(bicicleta.IdBicicleta);
            return MapToDto(creada!);
        }

        public async Task<BicicletaDto> UpdateAsync(int id, BicicletaUpdateDto dto)
        {
            var bicicleta = await _unitOfWork.Bicicletas.GetByIdAsync(id)
                ?? throw new NotFoundException($"No se encontro la bicicleta con id {id}.");

            bicicleta.Precio = dto.Precio;
            bicicleta.Stock = dto.Stock;

            _unitOfWork.Bicicletas.Update(bicicleta);
            await _unitOfWork.SaveChangesAsync();

            var actualizada = await _unitOfWork.Bicicletas.GetByIdConCategoriaAsync(id);
            return MapToDto(actualizada!);
        }

        public async Task DeleteAsync(int id)
        {
            var bicicleta = await _unitOfWork.Bicicletas.GetByIdAsync(id)
                ?? throw new NotFoundException($"No se encontro la bicicleta con id {id}.");

            _unitOfWork.Bicicletas.Remove(bicicleta);
            await _unitOfWork.SaveChangesAsync();
        }

        private BicicletaDto MapToDto(Bicicleta bicicleta) => new()
        {
            IdBicicleta = bicicleta.IdBicicleta,
            IdCategoria = bicicleta.IdCategoria,
            NombreCategoria = bicicleta.Categoria?.Nombre,
            Marca = bicicleta.Marca,
            Modelo = bicicleta.Modelo,
            Precio = bicicleta.Precio,
            Stock = bicicleta.Stock,
            Estado = bicicleta.Estado,
            Agotado = bicicleta.Stock == 0,
            StockBajo = bicicleta.Stock > 0 && bicicleta.Stock <= _inventarioSettings.StockBajoUmbral
        };
    }
}
