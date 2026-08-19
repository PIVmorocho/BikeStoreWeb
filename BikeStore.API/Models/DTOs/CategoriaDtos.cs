using System.ComponentModel.DataAnnotations;

namespace BikeStore.API.Models.DTOs
{
    public class CategoriaDto
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }

    public class CategoriaCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        public bool Activo { get; set; } = true;
    }

    public class CategoriaUpdateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        public bool Activo { get; set; }
    }
}
