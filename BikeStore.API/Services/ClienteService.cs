using BikeStore.API.Data;
using BikeStore.API.Exceptions;
using BikeStore.API.Models.DTOs;
using BikeStore.API.Models.Entities;

namespace BikeStore.API.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<ClienteDto>> GetAllAsync()
        {
            var clientes = await _unitOfWork.Clientes.GetAllAsync();
            return clientes.Select(MapToDto).ToList();
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id)
                ?? throw new NotFoundException($"No se encontro el cliente con id {id}.");
            return MapToDto(cliente);
        }

        public async Task<ClienteDto> CreateAsync(ClienteCreateDto dto)
        {
            if (await _unitOfWork.Clientes.GetByCedulaAsync(dto.Cedula) is not null)
                throw new BusinessRuleException($"Ya existe un cliente registrado con la cedula {dto.Cedula}.");

            var cliente = new Cliente
            {
                Cedula = dto.Cedula,
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Telefono = dto.Telefono,
                Correo = dto.Correo
            };

            await _unitOfWork.Clientes.AddAsync(cliente);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(cliente);
        }

        public async Task<ClienteDto> UpdateAsync(int id, ClienteUpdateDto dto)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id)
                ?? throw new NotFoundException($"No se encontro el cliente con id {id}.");

            var clienteConMismaCedula = await _unitOfWork.Clientes.GetByCedulaAsync(dto.Cedula);
            if (clienteConMismaCedula is not null && clienteConMismaCedula.IdCliente != id)
                throw new BusinessRuleException($"Ya existe un cliente registrado con la cedula {dto.Cedula}.");

            cliente.Cedula = dto.Cedula;
            cliente.Nombres = dto.Nombres;
            cliente.Apellidos = dto.Apellidos;
            cliente.Telefono = dto.Telefono;
            cliente.Correo = dto.Correo;

            _unitOfWork.Clientes.Update(cliente);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(cliente);
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id)
                ?? throw new NotFoundException($"No se encontro el cliente con id {id}.");

            _unitOfWork.Clientes.Remove(cliente);
            await _unitOfWork.SaveChangesAsync();
        }

        private static ClienteDto MapToDto(Cliente cliente) => new()
        {
            IdCliente = cliente.IdCliente,
            Cedula = cliente.Cedula,
            Nombres = cliente.Nombres,
            Apellidos = cliente.Apellidos,
            Telefono = cliente.Telefono,
            Correo = cliente.Correo
        };
    }
}
