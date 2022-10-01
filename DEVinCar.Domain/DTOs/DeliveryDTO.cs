using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.DTOs;

public class DeliveryDTO
{
    public int? AddressId { get; set; }
    public DateTime? DeliveryForecast { get; set; }

    public DeliveryDTO (Delivery queryDTO)
    {
        AddressId = queryDTO.AddressId;
    }
}