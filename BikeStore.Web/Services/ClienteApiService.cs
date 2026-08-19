using System.Net.Http.Json;
using BikeStore.Web.Models.Api;

namespace BikeStore.Web.Services
{
    public class ClienteApiService : ApiServiceBase, IClienteApiService
    {
        public ClienteApiService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<ClienteDto>> GetAllAsync()
        {
            var response = await HttpClient.GetAsync("api/clientes");
            await EnsureSuccessAsync(response);
            return await response.Content.ReadFromJsonAsync<List<ClienteDto>>(JsonOptions) ?? new();
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            var response = await HttpClient.GetAsync($"api/clientes/{id}");
            await EnsureSuccessAsync(response);
            return (await response.Content.ReadFromJsonAsync<ClienteDto>(JsonOptions))!;
        }

        public async Task CreateAsync(ClienteCreateDto dto)
        {
            var response = await HttpClient.PostAsJsonAsync("api/clientes", dto);
            await EnsureSuccessAsync(response);
        }

        public async Task UpdateAsync(int id, ClienteUpdateDto dto)
        {
            var response = await HttpClient.PutAsJsonAsync($"api/clientes/{id}", dto);
            await EnsureSuccessAsync(response);
        }

        public async Task DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync($"api/clientes/{id}");
            await EnsureSuccessAsync(response);
        }
    }
}
