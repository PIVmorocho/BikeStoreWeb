using System.Net.Http.Json;
using System.Text.Json;
using BikeStore.Web.Models.Api;

namespace BikeStore.Web.Services
{
    public abstract class ApiServiceBase
    {
        protected static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        protected readonly HttpClient HttpClient;

        protected ApiServiceBase(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        protected static async Task EnsureSuccessAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;

            var mensaje = $"Error al comunicarse con la API ({(int)response.StatusCode}).";
            try
            {
                var problema = await response.Content.ReadFromJsonAsync<ApiProblemDetails>(JsonOptions);
                if (problema is not null && !string.IsNullOrWhiteSpace(problema.Title))
                    mensaje = problema.Title;
            }
            catch (JsonException)
            {
                // El cuerpo de la respuesta no contenia un ApiProblemDetails valido.
            }

            throw new ApiException(mensaje, response.StatusCode);
        }
    }
}
