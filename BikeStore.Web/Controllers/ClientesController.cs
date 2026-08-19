using BikeStore.Web.Models.Api;
using BikeStore.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteApiService _clienteApiService;

        public ClientesController(IClienteApiService clienteApiService)
        {
            _clienteApiService = clienteApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _clienteApiService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View(new ClienteCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            try
            {
                await _clienteApiService.CreateAsync(dto);
                TempData["Success"] = "Cliente creado correctamente.";
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
            var cliente = await _clienteApiService.GetByIdAsync(id);
            var dto = new ClienteUpdateDto
            {
                Cedula = cliente.Cedula,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                Telefono = cliente.Telefono,
                Correo = cliente.Correo
            };
            ViewBag.IdCliente = id;
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdCliente = id;
                return View(dto);
            }

            try
            {
                await _clienteApiService.UpdateAsync(id, dto);
                TempData["Success"] = "Cliente actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.IdCliente = id;
                return View(dto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _clienteApiService.DeleteAsync(id);
                TempData["Success"] = "Cliente eliminado correctamente.";
            }
            catch (ApiException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
