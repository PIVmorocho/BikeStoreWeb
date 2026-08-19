using BikeStore.API.Configuration;
using BikeStore.API.Data;
using BikeStore.API.Exceptions;
using BikeStore.API.Models.DTOs;
using BikeStore.API.Models.Entities;
using Microsoft.Extensions.Options;

namespace BikeStore.API.Services
{
    public class VentaService : IVentaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly VentaSettings _ventaSettings;

        public VentaService(IUnitOfWork unitOfWork, IOptions<VentaSettings> ventaSettings)
        {
            _unitOfWork = unitOfWork;
            _ventaSettings = ventaSettings.Value;
        }

        public async Task<VentaResponseDto> CrearVentaAsync(VentaCreateDto dto)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(dto.IdCliente)
                ?? throw new NotFoundException($"No se encontro el cliente con id {dto.IdCliente}.");

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var venta = new Venta
                {
                    IdCliente = dto.IdCliente,
                    Fecha = DateTime.Now
                };

                decimal subtotalVenta = 0m;

                foreach (var detalleDto in dto.Detalles)
                {
                    var bicicleta = await _unitOfWork.Bicicletas.GetByIdAsync(detalleDto.IdBicicleta)
                        ?? throw new NotFoundException($"No se encontro la bicicleta con id {detalleDto.IdBicicleta}.");

                    if (bicicleta.Stock < detalleDto.Cantidad)
                        throw new BusinessRuleException(
                            $"Stock insuficiente para {bicicleta.Marca} {bicicleta.Modelo}. Disponible: {bicicleta.Stock}, solicitado: {detalleDto.Cantidad}.");

                    var subtotalDetalle = bicicleta.Precio * detalleDto.Cantidad;
                    subtotalVenta += subtotalDetalle;

                    venta.Detalles.Add(new DetalleVenta
                    {
                        IdBicicleta = bicicleta.IdBicicleta,
                        Cantidad = detalleDto.Cantidad,
                        Precio = bicicleta.Precio,
                        Subtotal = subtotalDetalle
                    });

                    bicicleta.Stock -= detalleDto.Cantidad;
                    _unitOfWork.Bicicletas.Update(bicicleta);
                }

                var iva = Math.Round(subtotalVenta * _ventaSettings.PorcentajeIva, 2);
                venta.Total = subtotalVenta + iva;

                await _unitOfWork.Ventas.AddAsync(venta);
                await _unitOfWork.CommitTransactionAsync();

                var creada = await _unitOfWork.Ventas.GetByIdConDetallesAsync(venta.IdVenta);
                return MapToDto(creada!, subtotalVenta, iva);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IReadOnlyList<VentaResponseDto>> GetAllAsync()
        {
            var ventas = await _unitOfWork.Ventas.GetAllConDetallesAsync();
            return ventas.Select(MapExistingToDto).ToList();
        }

        public async Task<VentaResponseDto> GetByIdAsync(int id)
        {
            var venta = await _unitOfWork.Ventas.GetByIdConDetallesAsync(id)
                ?? throw new NotFoundException($"No se encontro la venta con id {id}.");
            return MapExistingToDto(venta);
        }

        public async Task<IReadOnlyList<VentaResponseDto>> GetByClienteAsync(int idCliente)
        {
            if (await _unitOfWork.Clientes.GetByIdAsync(idCliente) is null)
                throw new NotFoundException($"No se encontro el cliente con id {idCliente}.");

            var ventas = await _unitOfWork.Ventas.GetByClienteAsync(idCliente);
            return ventas.Select(MapExistingToDto).ToList();
        }

        private VentaResponseDto MapExistingToDto(Venta venta)
        {
            var subtotal = venta.Detalles.Sum(d => d.Subtotal);
            var iva = venta.Total - subtotal;
            return MapToDto(venta, subtotal, iva);
        }

        private VentaResponseDto MapToDto(Venta venta, decimal subtotal, decimal iva) => new()
        {
            IdVenta = venta.IdVenta,
            Fecha = venta.Fecha,
            IdCliente = venta.IdCliente,
            NombreCliente = venta.Cliente is not null ? $"{venta.Cliente.Nombres} {venta.Cliente.Apellidos}" : string.Empty,
            Subtotal = subtotal,
            PorcentajeIva = _ventaSettings.PorcentajeIva,
            Iva = iva,
            Total = venta.Total,
            Detalles = venta.Detalles.Select(d => new DetalleVentaResponseDto
            {
                IdBicicleta = d.IdBicicleta,
                Marca = d.Bicicleta?.Marca ?? string.Empty,
                Modelo = d.Bicicleta?.Modelo ?? string.Empty,
                Cantidad = d.Cantidad,
                Precio = d.Precio,
                Subtotal = d.Subtotal
            }).ToList()
        };
    }
}
