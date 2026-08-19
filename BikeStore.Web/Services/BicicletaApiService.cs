using System.Net.Http.Json;
using System.Web;
using BikeStore.Web.Models.Api;

namespace BikeStore.Web.Services
{
    public class BicicletaApiService : ApiServiceBase, IBicicletaApiService
    {
        public BicicletaApiService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<BicicletaDto>> BuscarAsync(BicicletaFiltro filtro)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (!string.IsNullOrWhiteSpace(filtro.Nombre)) query["nombre"] = filtro.Nombre;
            if (filtro.Categoria.HasValue) query["categoria"] = filtro.Categoria.Value.ToString();
            if (!string.IsNullOrWhiteSpace(filtro.Marca)) query["marca"] = filtro.Marca;
            if (filtro.StockBajo.HasValue) query["stockBajo"] = filtro.StockBajo.Value.ToString();
            if (filtro.Agotado.HasValue) query["agotado"] = filtro.Agotado.Value.ToString();

            var response = await HttpClient.GetAsync($"api/bicicletas?{query}");
            await EnsureSuccessAsync(response);
            return await response.Content.ReadFromJsonAsync<List<BicicletaDto>>(JsonOptions) ?? new();
        }

        public async Task<BicicletaDto> GetByIdAsync(int id)
        {
            var response = await HttpClient.GetAsync($"api/bicicletas/{id}");
            await EnsureSuccessAsync(response);
            return (await response.Content.ReadFromJsonAsync<BicicletaDto>(JsonOptions))!;
        }

        public async Task CreateAsync(BicicletaCreateDto dto)
        {
            var response = await HttpClient.PostAsJsonAsync("api/bicicletas", dto);
            await EnsureSuccessAsync(response);
        }

        public async Task UpdateAsync(int id, BicicletaUpdateDto dto)
        {
            var response = await HttpClient.PutAsJsonAsync($"api/bicicletas/{id}", dto);
            await EnsureSuccessAsync(response);
        }

        public async Task DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync($"api/bicicletas/{id}");
            await EnsureSuccessAsync(response);
        }
    }
}
