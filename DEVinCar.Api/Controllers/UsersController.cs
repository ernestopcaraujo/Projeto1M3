using DEVinCar.Domain.Models;
using DEVinCar.Infra.Data;
using DEVinCar.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using DEVinCar.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

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
    //[Authorize]
    public IActionResult Get(
       [FromQuery] string name,
       [FromQuery] DateTime birthDateMax,
       [FromQuery] DateTime birthDateMin)
    {
        var user = _usersService.GetByNameService(name,birthDateMax,birthDateMin);
        return Ok(user.ToList());
    }

    [HttpGet("{id}")]
    //[Authorize]
    public IActionResult GetById(
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
    //[Authorize]
    public IActionResult GetByIdBuy(
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
    //[Authorize]
    public IActionResult GetSalesBySellerId(
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
    //[Authorize]
    public IActionResult Post(
        [FromBody] UserDTO userDTO
    )
    {
        var newUser = new User(userDTO);     
        _usersService.InsertUser(newUser);

        return Created("api/users", newUser.Id);
    }

    [HttpPost("{userId}/sales")]
    //[Authorize]
    public ActionResult<Sale> PostSaleUserId(
           [FromRoute] int userId,
           [FromBody] SaleDTO saleDTO)
    {
        var sale = new Sale(saleDTO);
        sale.SellerId = userId;
        _salesService.InsertSale(sale);

        return Created("api/sale", sale.Id);
    }

    [HttpPost("{userId}/buy")]
    //[Authorize]
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
    //[Authorize]
    public ActionResult Delete(
       [FromRoute] int userId
   )
    {
        _usersService.RemoveUser(userId);
        return Ok();
    }
}





