
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
public class CarController : ControllerBase
{
    private readonly DevInCarDbContext _context;
    private readonly IMemoryCache _cache;
    private readonly ICarsService _carsService;

    public CarController(DevInCarDbContext context,ICarsService carsService,IMemoryCache cache)
    {
        _context = context;
        _carsService = carsService;
        _cache = cache;
    }


    [HttpGet("{carId}")]
    [Authorize(Roles = "Gerente")]
    public ActionResult<Car> GetById([FromRoute] int carId)
    {
        var car = _carsService.GetById(carId);
        return Ok(car);
    }

    [HttpGet]
    [Authorize(Roles = "Gerente")]
    public ActionResult<List<Car>> Get(
        [FromQuery] string name,
        [FromQuery] decimal priceMin,
        [FromQuery] decimal priceMax
    )
    {
        var car = _carsService.GetList(name,priceMin,priceMax);
        var carDTO = car.Select(c=>new CarDTO(c));
        return Ok(carDTO);
        
    }

    [HttpPost]
    [Authorize(Roles = "Gerente")]
    public ActionResult<Car> Post(
        [FromBody] CarDTO carDTO
    )
    {
        var newCar = new Car(carDTO);     
        _carsService.InsertCar(newCar);

        return Created("api/car", newCar.Id);

    }

    [HttpDelete("{carId}")]
    [Authorize(Roles = "Gerente")]
    public ActionResult Delete([FromRoute] int carId)
    {
        var car = _context.Cars.Find(carId);
        var soldCar = _context.SaleCars.Any(s => s.CarId == carId);
        if (car == null)
        {
            return NotFound();
        }
        if (soldCar)
        {
            return BadRequest();
        }
        _context.Remove(car);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPut("{carId}")]
    [Authorize(Roles = "Gerente")]
    public ActionResult<Car> Put([FromBody] CarDTO carDto, [FromRoute] int carId)
    {
        var car = _context.Cars.Find(carId);
        var name = _context.Cars.Any(c => c.Name == carDto.Name && c.Id != carId);


        if (car == null)
            return NotFound();
        if (carDto.Name.Equals(null) || carDto.SuggestedPrice.Equals(null))
            return BadRequest();
        if (carDto.SuggestedPrice <= 0)
            return BadRequest();
        if (name)
            return BadRequest();

        car.Name = carDto.Name;
        car.SuggestedPrice = carDto.SuggestedPrice;

        _context.SaveChanges();
        return NoContent();
    }
}
