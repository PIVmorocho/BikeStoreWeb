using System.ComponentModel.DataAnnotations;

namespace BikeStore.Web.Models.Api
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Correo { get; set; }

        public string NombreCompleto => $"{Nombres} {Apellidos}";
    }

    public class ClienteCreateDto
    {
        [Required(ErrorMessage = "La cedula es obligatoria.")]
        [StringLength(20)]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [StringLength(100)]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(100)]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Telefono")]
        public string? Telefono { get; set; }

        [StringLength(150)]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato valido.")]
        [Display(Name = "Correo")]
        public string? Correo { get; set; }
    }

    public class ClienteUpdateDto
    {
        [Required(ErrorMessage = "La cedula es obligatoria.")]
        [StringLength(20)]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [StringLength(100)]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(100)]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Telefono")]
        public string? Telefono { get; set; }

        [StringLength(150)]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato valido.")]
        [Display(Name = "Correo")]
        public string? Correo { get; set; }
    }
}
