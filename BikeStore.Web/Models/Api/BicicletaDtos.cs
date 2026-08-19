using System.ComponentModel.DataAnnotations;

namespace BikeStore.Web.Models.Api
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
        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        [StringLength(50)]
        [Display(Name = "Marca")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        [StringLength(100)]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0.")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0.")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Activo";
    }

    public class BicicletaUpdateDto
    {
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0.")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0.")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }
    }
}
