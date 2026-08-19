using System.ComponentModel.DataAnnotations;

namespace BikeStore.API.Models.DTOs
{
    public class BicicletaDto
    {
        public int IdBicicleta { get; set; }
        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Estado { get; set; } = string.Empty;
        public bool StockBajo { get; set; }
        public bool Agotado { get; set; }
    }

    public class BicicletaCreateDto
    {
        [Required(ErrorMessage = "La categoria es obligatoria.")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        [MaxLength(50)]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        [MaxLength(100)]
        public string Modelo { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0.")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0.")]
        public int Stock { get; set; }

        [MaxLength(20)]
        public string Estado { get; set; } = "Activo";
    }

    public class BicicletaUpdateDto
    {
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0.")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0.")]
        public int Stock { get; set; }
    }
}
