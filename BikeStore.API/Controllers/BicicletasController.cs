using BikeStore.API.Data.Repositories;
using BikeStore.API.Models.DTOs;
using BikeStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.API.Controllers
{
    [ApiController]
    [Route("api/bicicletas")]
    public class BicicletasController : ControllerBase
    {
        private readonly IBicicletaService _bicicletaService;

        public BicicletasController(IBicicletaService bicicletaService)
        {
            _bicicletaService = bicicletaService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BicicletaDto>>> GetAll(
            [FromQuery] string? nombre,
            [FromQuery] int? categoria,
            [FromQuery] string? marca,
            [FromQuery] bool? stockBajo,
            [FromQuery] bool? agotado)
        {
            var filtro = new BicicletaFiltro
            {
                Nombre = nombre,
                IdCategoria = categoria,
                Marca = marca,
                StockBajo = stockBajo,
                Agotado = agotado
            };

            return Ok(await _bicicletaService.BuscarAsync(filtro));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BicicletaDto>> GetById(int id)
        {
            return Ok(await _bicicletaService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<BicicletaDto>> Create(BicicletaCreateDto dto)
        {
            var creada = await _bicicletaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = creada.IdBicicleta }, creada);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<BicicletaDto>> Update(int id, BicicletaUpdateDto dto)
        {
            return Ok(await _bicicletaService.UpdateAsync(id, dto));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bicicletaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
