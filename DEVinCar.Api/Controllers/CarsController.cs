
using DEVinCar.Infra.Data;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Api.Config;

namespace DEVinCar.Api.Controllers;

[ApiController]
[Route("api/car")]
public class CarsController : ControllerBase
{
    
    private readonly IMemoryCache _cache;
    private readonly ICarsService _carsService;
    private readonly ISalesService _salesService;
    private readonly CacheService<CarDTO> _carsCache;

    public CarsController(ICarsService carsService,IMemoryCache cache, ISalesService salesService,CacheService<CarDTO> carsCache)
    {
        
        _carsService = carsService;
        _cache = cache;
        _salesService = salesService;
        carsCache.Config("car",new TimeSpan(0,5,0));
        _carsCache = carsCache;
    }


    [HttpGet("{carId}")]
    [Authorize(Roles = "Gerente")]
    public IActionResult GetById([FromRoute] int carId)
    {
        var car = _carsService.GetById(carId);

        if (!_cache.TryGetValue<Car>($"car:{carId}", out car))
        {
            car = _carsService.GetByIdCache(carId);
            _cache.Set($"car:{carId}", car, new TimeSpan(0, 5, 0));
        }

        return Ok(car);
        //cache deste Endpoint aplicado em CarsServices.cs
    }


    [HttpGet]
    [Authorize(Roles = "Gerente")]
    public ActionResult Get(
        [FromQuery] string name,
        [FromQuery] decimal priceMin,
        [FromQuery] decimal priceMax
    )
    {
        var car = _carsService.GetList(name,priceMin,priceMax);
        return Ok(car.ToList());

    }

    [HttpPost]
    [Authorize(Roles = "Gerente")]
    public IActionResult Post(
    [FromBody] CarDTO carDTO
    )
    {
        var newCar = new Car
        {
            Name =carDTO.Name,
            SuggestedPrice = carDTO.SuggestedPrice,
        };

        _carsService.InsertCar(newCar);

        return Created("api/car", newCar.Id);

    }

    [HttpDelete("{carId}")]
    [Authorize(Roles = "Gerente")]
    public ActionResult Delete([FromRoute] int carId)
    {
        _carsService.RemoveCar(carId);
        _carsCache.Remove($"{carId}");
        
       return NoContent();
    }

    [HttpPut("{carId}")]
    [Authorize(Roles = "Gerente")]
    public ActionResult<Car> Put([FromBody] CarDTO carDto, [FromRoute] int carId)
    {
        _carsService.PutCar(carDto,carId);
        _carsCache.Set($"{carId}", carDto);
        return Ok();
    }
}
