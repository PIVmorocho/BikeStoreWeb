using System.ComponentModel.DataAnnotations;

namespace BikeStore.API.Models.DTOs
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
    }

    public class ClienteCreateDto
    {
        [Required(ErrorMessage = "La cedula es obligatoria.")]
        [MaxLength(20)]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Telefono { get; set; }

        [MaxLength(150)]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato valido.")]
        public string? Correo { get; set; }
    }

    public class ClienteUpdateDto
    {
        [Required(ErrorMessage = "La cedula es obligatoria.")]
        [MaxLength(20)]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Telefono { get; set; }

        [MaxLength(150)]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato valido.")]
        public string? Correo { get; set; }
    }
}
