using System.ComponentModel.DataAnnotations;

namespace Practica_API.Modelos.DTOs
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }

        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
