using System.Net.Http.Json;
using BikeStore.Web.Models.Api;

namespace BikeStore.Web.Services
{
    public class CategoriaApiService : ApiServiceBase, ICategoriaApiService
    {
        public CategoriaApiService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<CategoriaDto>> GetAllAsync()
        {
            var response = await HttpClient.GetAsync("api/categorias");
            await EnsureSuccessAsync(response);
            return await response.Content.ReadFromJsonAsync<List<CategoriaDto>>(JsonOptions) ?? new();
        }

        public async Task<CategoriaDto> GetByIdAsync(int id)
        {
            var response = await HttpClient.GetAsync($"api/categorias/{id}");
            await EnsureSuccessAsync(response);
            return (await response.Content.ReadFromJsonAsync<CategoriaDto>(JsonOptions))!;
        }

        public async Task CreateAsync(CategoriaCreateDto dto)
        {
            var response = await HttpClient.PostAsJsonAsync("api/categorias", dto);
            await EnsureSuccessAsync(response);
        }

        public async Task UpdateAsync(int id, CategoriaUpdateDto dto)
        {
            var response = await HttpClient.PutAsJsonAsync($"api/categorias/{id}", dto);
            await EnsureSuccessAsync(response);
        }

        public async Task DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync($"api/categorias/{id}");
            await EnsureSuccessAsync(response);
        }
    }
}
