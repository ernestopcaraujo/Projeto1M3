
using DEVinCar.Infra.Data;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using DEVinCar.Domain.Interfaces.Services;

namespace DEVinCar.Api.Controllers;

[ApiController]
[Route("api/car")]
public class CarsController : ControllerBase
{
    
    private readonly IMemoryCache _cache;
    private readonly ICarsService _carsService;
    private readonly ISalesService _salesService;

    public CarsController(ICarsService carsService,IMemoryCache cache, ISalesService salesService)
    {
        _carsService = carsService;
        _cache = cache;
        _salesService = salesService;
    }


    [HttpGet("{carId}")]
    //[Authorize(Roles = "Gerente")]
    public IActionResult GetById([FromRoute] int carId)
    {
        var car = _carsService.GetById(carId);
        return Ok(car);
    }


    [HttpGet]
    //[Authorize(Roles = "Gerente")]
    public ActionResult Get(
        [FromQuery] string name,
        [FromQuery] decimal priceMin,
        [FromQuery] decimal priceMax
    )
    {
        var car = _carsService.GetList(name,priceMin,priceMax);
        //var carDTO = car.Select(c=>new CarDTO(c));
        return Ok(car.ToList());

    }

    [HttpPost]
    //[Authorize(Roles = "Gerente")]
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
    //[Authorize(Roles = "Gerente")]
    public ActionResult Delete([FromRoute] int carId)
    {
        _carsService.RemoveCar(carId);
        
       return NoContent();
    }

    [HttpPut("{carId}")]
    //[Authorize(Roles = "Gerente")]
    public ActionResult<Car> Put([FromBody] CarDTO carDto, [FromRoute] int carId)
    {
        _carsService.PutCar(carDto,carId);
        return Ok();
    }
}
