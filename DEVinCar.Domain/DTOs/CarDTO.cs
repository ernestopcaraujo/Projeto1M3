using System.ComponentModel.DataAnnotations;
using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.DTOs
{
    public class CarDTO
    {
        [Required(ErrorMessage = "The name is required")]
        [MaxLength(255)]
        public string Name { get; set; }
        public decimal SuggestedPrice { get; set; }

        public CarDTO ()
        {
            
        }

        public CarDTO (Car car)
        {
            Name = car.Name;
            SuggestedPrice = car.SuggestedPrice;
        }

    }
}
