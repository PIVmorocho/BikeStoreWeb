using BikeStore.Web.Models.Api;
using BikeStore.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaApiService _categoriaApiService;

        public CategoriasController(ICategoriaApiService categoriaApiService)
        {
            _categoriaApiService = categoriaApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoriaApiService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View(new CategoriaCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            try
            {
                await _categoriaApiService.CreateAsync(dto);
                TempData["Success"] = "Categoria creada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaApiService.GetByIdAsync(id);
            var dto = new CategoriaUpdateDto
            {
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Activo = categoria.Activo
            };
            ViewBag.IdCategoria = id;
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdCategoria = id;
                return View(dto);
            }

            try
            {
                await _categoriaApiService.UpdateAsync(id, dto);
                TempData["Success"] = "Categoria actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.IdCategoria = id;
                return View(dto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoriaApiService.DeleteAsync(id);
                TempData["Success"] = "Categoria eliminada correctamente.";
            }
            catch (ApiException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
