using BikeStore.Web.Models.Api;
using BikeStore.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Web.Controllers
{
    public class VentasController : Controller
    {
        private readonly IVentaApiService _ventaApiService;
        private readonly IClienteApiService _clienteApiService;
        private readonly IBicicletaApiService _bicicletaApiService;

        public VentasController(
            IVentaApiService ventaApiService,
            IClienteApiService clienteApiService,
            IBicicletaApiService bicicletaApiService)
        {
            _ventaApiService = ventaApiService;
            _clienteApiService = clienteApiService;
            _bicicletaApiService = bicicletaApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _ventaApiService.GetAllAsync());
        }

        public async Task<IActionResult> Detalle(int id)
        {
            return View(await _ventaApiService.GetByIdAsync(id));
        }

        public async Task<IActionResult> PorCliente(int idCliente)
        {
            ViewBag.Cliente = await _clienteApiService.GetByIdAsync(idCliente);
            return View(await _ventaApiService.GetByClienteAsync(idCliente));
        }

        public async Task<IActionResult> PuntoDeVenta()
        {
            ViewBag.Clientes = await _clienteApiService.GetAllAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BuscarBicicletas(string? nombre, string? marca)
        {
            var filtro = new BicicletaFiltro { Nombre = nombre, Marca = marca };
            var bicicletas = await _bicicletaApiService.BuscarAsync(filtro);
            return Json(bicicletas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar([FromBody] VentaCreateDto dto)
        {
            try
            {
                var venta = await _ventaApiService.CrearAsync(dto);
                return Json(new { success = true, idVenta = venta.IdVenta });
            }
            catch (ApiException ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
