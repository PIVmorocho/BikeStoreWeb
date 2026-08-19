using BikeStore.Web.Models.Api;
using BikeStore.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Web.Controllers
{
    public class BicicletasController : Controller
    {
        private readonly IBicicletaApiService _bicicletaApiService;
        private readonly ICategoriaApiService _categoriaApiService;

        public BicicletasController(IBicicletaApiService bicicletaApiService, ICategoriaApiService categoriaApiService)
        {
            _bicicletaApiService = bicicletaApiService;
            _categoriaApiService = categoriaApiService;
        }

        public async Task<IActionResult> Index(string? nombre, int? categoria, string? marca)
        {
            var filtro = new BicicletaFiltro { Nombre = nombre, Categoria = categoria, Marca = marca };
            ViewBag.Categorias = await _categoriaApiService.GetAllAsync();
            ViewBag.Filtro = filtro;
            return View(await _bicicletaApiService.BuscarAsync(filtro));
        }

        public async Task<IActionResult> Inventario(bool? stockBajo, bool? agotado)
        {
            var filtro = new BicicletaFiltro { StockBajo = stockBajo, Agotado = agotado };
            ViewBag.Filtro = filtro;
            return View(await _bicicletaApiService.BuscarAsync(filtro));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = await _categoriaApiService.GetAllAsync();
            return View(new BicicletaCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BicicletaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = await _categoriaApiService.GetAllAsync();
                return View(dto);
            }

            try
            {
                await _bicicletaApiService.CreateAsync(dto);
                TempData["Success"] = "Bicicleta creada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.Categorias = await _categoriaApiService.GetAllAsync();
                return View(dto);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var bicicleta = await _bicicletaApiService.GetByIdAsync(id);
            ViewBag.Bicicleta = bicicleta;
            return View(new BicicletaUpdateDto { Precio = bicicleta.Precio, Stock = bicicleta.Stock });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BicicletaUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Bicicleta = await _bicicletaApiService.GetByIdAsync(id);
                return View(dto);
            }

            try
            {
                await _bicicletaApiService.UpdateAsync(id, dto);
                TempData["Success"] = "Bicicleta actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.Bicicleta = await _bicicletaApiService.GetByIdAsync(id);
                return View(dto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bicicletaApiService.DeleteAsync(id);
                TempData["Success"] = "Bicicleta eliminada correctamente.";
            }
            catch (ApiException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
