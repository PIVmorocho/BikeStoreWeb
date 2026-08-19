using System.ComponentModel.DataAnnotations;

namespace BikeStore.Web.Models.Api
{
    public class DetalleVentaCreateDto
    {
        [Required]
        public int IdBicicleta { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }
    }

    public class VentaCreateDto
    {
        [Required(ErrorMessage = "Debe seleccionar un cliente.")]
        public int IdCliente { get; set; }

        [MinLength(1, ErrorMessage = "Agregue al menos una bicicleta al carrito.")]
        public List<DetalleVentaCreateDto> Detalles { get; set; } = new();
    }

    public class DetalleVentaResponseDto
    {
        public int IdBicicleta { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class VentaResponseDto
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal PorcentajeIva { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVentaResponseDto> Detalles { get; set; } = new();
    }
}
