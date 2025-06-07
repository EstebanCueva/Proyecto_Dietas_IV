using System.Text.Json.Serialization;

namespace Proyecto.Models
{
    public class UsuarioDTO
    {
        [JsonPropertyName("nombre")]
        public string Name { get; set; }

        [JsonPropertyName("sexo")]
        public string Sex { get; set; }

        [JsonPropertyName("edad")]
        public int Age { get; set; }

        [JsonPropertyName("descripcion")]
        public string Description { get; set; }

        [JsonPropertyName("peso")]
        public int Weight { get; set; }

        [JsonPropertyName("altura")]
        public int Height { get; set; }

        [JsonPropertyName("actividad")]
        public string Activity { get; set; }

        [JsonPropertyName("dieta")]
        public string DietaNombre { get; set; }

        [JsonPropertyName("calorias_totales")]
        public int TotalCalories { get; set; }
    }
}
