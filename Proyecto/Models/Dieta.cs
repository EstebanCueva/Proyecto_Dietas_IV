using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Dieta
    {
        [Key]
        public int Id { get; set; }
        public string NameDiet { get; set; }
        public string DescriptionDiet { get; set; }
        public int Calories { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int DailyUse { get; set; }
        public string DietType {  get; set; }

    }
}
