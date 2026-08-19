using BikeStore.API.Models.DTOs;
using BikeStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.API.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoriaDto>>> GetAll()
        {
            return Ok(await _categoriaService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaDto>> GetById(int id)
        {
            return Ok(await _categoriaService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDto>> Create(CategoriaCreateDto dto)
        {
            var creada = await _categoriaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = creada.IdCategoria }, creada);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDto>> Update(int id, CategoriaUpdateDto dto)
        {
            return Ok(await _categoriaService.UpdateAsync(id, dto));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoriaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
