using System.Net.Http.Json;
using BikeStore.Web.Models.Api;

namespace BikeStore.Web.Services
{
    public class VentaApiService : ApiServiceBase, IVentaApiService
    {
        public VentaApiService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<VentaResponseDto>> GetAllAsync()
        {
            var response = await HttpClient.GetAsync("api/ventas");
            await EnsureSuccessAsync(response);
            return await response.Content.ReadFromJsonAsync<List<VentaResponseDto>>(JsonOptions) ?? new();
        }

        public async Task<VentaResponseDto> GetByIdAsync(int id)
        {
            var response = await HttpClient.GetAsync($"api/ventas/{id}");
            await EnsureSuccessAsync(response);
            return (await response.Content.ReadFromJsonAsync<VentaResponseDto>(JsonOptions))!;
        }

        public async Task<List<VentaResponseDto>> GetByClienteAsync(int idCliente)
        {
            var response = await HttpClient.GetAsync($"api/ventas/cliente/{idCliente}");
            await EnsureSuccessAsync(response);
            return await response.Content.ReadFromJsonAsync<List<VentaResponseDto>>(JsonOptions) ?? new();
        }

        public async Task<VentaResponseDto> CrearAsync(VentaCreateDto dto)
        {
            var response = await HttpClient.PostAsJsonAsync("api/ventas", dto);
            await EnsureSuccessAsync(response);
            return (await response.Content.ReadFromJsonAsync<VentaResponseDto>(JsonOptions))!;
        }
    }
}
