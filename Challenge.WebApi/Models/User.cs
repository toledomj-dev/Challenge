using System.ComponentModel.DataAnnotations;

namespace Challenge.WebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Apellido { get; set; } = string.Empty;
        public DateOnly FechaDeNacimiento { get; set; }
        [Required, MaxLength(20)]
        public string DNI { get; set; } = string.Empty;
    }
}
