using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Usuario
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Activity { get; set; }

        public int IdDieta { get; set; }
        [ForeignKey("IdDieta")]
        public Dieta? Dieta { get; set; }
        public int TotalCalories { get; set; }

    }
}
