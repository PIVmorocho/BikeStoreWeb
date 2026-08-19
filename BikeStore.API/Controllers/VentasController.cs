using BikeStore.API.Models.DTOs;
using BikeStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.API.Controllers
{
    [ApiController]
    [Route("api/ventas")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<VentaResponseDto>>> GetAll()
        {
            return Ok(await _ventaService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VentaResponseDto>> GetById(int id)
        {
            return Ok(await _ventaService.GetByIdAsync(id));
        }

        [HttpGet("cliente/{idCliente:int}")]
        public async Task<ActionResult<IReadOnlyList<VentaResponseDto>>> GetByCliente(int idCliente)
        {
            return Ok(await _ventaService.GetByClienteAsync(idCliente));
        }

        [HttpPost]
        public async Task<ActionResult<VentaResponseDto>> Create(VentaCreateDto dto)
        {
            var creada = await _ventaService.CrearVentaAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = creada.IdVenta }, creada);
        }
    }
}
