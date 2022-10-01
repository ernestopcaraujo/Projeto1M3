using DEVinCar.Domain.Models;
using DEVinCar.Infra.Data;
using DEVinCar.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using DEVinCar.Domain.Interfaces.Services;

namespace DEVinCar.Api.Controllers;

[ApiController]
[Route("api/user")]

public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly ISalesService _salesService;
    public UsersController(IUsersService usersService, ISalesService salesService)
    {
        _usersService = usersService;
        _salesService = salesService;
    }

    [HttpGet]
    public ActionResult<List<User>> Get(
       [FromQuery] string name,
       [FromQuery] DateTime birthDateMax,
       [FromQuery] DateTime birthDateMin)
    {
        var user = _usersService.GetByNameService(name,birthDateMax,birthDateMin);
        var userDTO = user.Select(x=>new UserDTO(x));

        if (!userDTO.Any())
        {
            return NoContent();
        }
        
        return Ok(userDTO);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetById(
        [FromRoute] int id
    )
    {
        var userById = _usersService.GetByIdService(id);
        
        if (userById == null) 
        {
            return NotFound();
        }

        return Ok(userById);
    }

    [HttpGet("{userId}/buy")]
    public ActionResult <List<Sale>> GetByIdBuy(
       [FromRoute] int userId)

    {
        var sales = _usersService.GetByIdBuyService(userId);

        if (sales == null || sales.Count() == 0)
        {
            return NoContent();
        }

        return Ok(sales.ToList());
    }

    [HttpGet("{userId}/sales")]
    public ActionResult<Sale> GetSalesBySellerId(
       [FromRoute] int userId)
    {
        var sales = _usersService.GetSalesBySellerIdService(userId);

        if (sales == null || sales.Count() == 0)
        {
            return NoContent();
        }
        
        return Ok(sales.ToList());
    }

    [HttpPost]
    public ActionResult Post(
        [FromBody] UserDTO userDTO
    )
    {
        var newUser = new User(userDTO);     
        _usersService.InsertUser(newUser);

        return Created("api/users", newUser.Id);
    }

    [HttpPost("{userId}/sales")]
    public ActionResult<Sale> PostSaleUserId(
           [FromRoute] int userId,
           [FromBody] SaleDTO saleDTO)
    {
        // if(_context.Sales.Any(s=>s.BuyerId == 0 || body.BuyerId == 0))
        // {
        //      return BadRequest();
        //  }  
        //  A validação acima foi eliminada em favor da validação já existente na
        //  annotation da classe SaleDTO.cs

        var sale = new Sale(saleDTO);
        sale.SellerId = userId;
        _salesService.InsertSale(sale);

        return Created("api/sale", sale.Id);
    }

    [HttpPost("{userId}/buy")]

   public ActionResult<Sale> PostBuyUserId(
          [FromRoute] int userId,
          [FromBody] BuyDTO buyDTO)
    {
        var buy = new Sale(buyDTO);
        buy.BuyerId = userId;
        _salesService.InsertBuy(buy);

        return Created("api/user/{userId}/buy", buy.Id);
    }
      

    [HttpDelete("{userId}")]
    public ActionResult Delete(
       [FromRoute] int userId
   )
    {
        _usersService.RemoveUser(userId);
        return NoContent();
    }
}





