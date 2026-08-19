using BikeStore.API.Models.DTOs;
using BikeStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.API.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ClienteDto>>> GetAll()
        {
            return Ok(await _clienteService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDto>> GetById(int id)
        {
            return Ok(await _clienteService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Create(ClienteCreateDto dto)
        {
            var creado = await _clienteService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = creado.IdCliente }, creado);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ClienteDto>> Update(int id, ClienteUpdateDto dto)
        {
            return Ok(await _clienteService.UpdateAsync(id, dto));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteService.DeleteAsync(id);
            return NoContent();
        }
    }
}
