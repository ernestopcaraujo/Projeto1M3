using DEVinCar.Domain.Models;
using DEVinCar.Infra.Data;
using DEVinCar.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Runtime.ConstrainedExecution;
using DEVinCar.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace DEVinCar.Api.Controllers
{
    [ApiController]
    [Route("api/deliver")]
    public class DeliverController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        public DeliverController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet]
        //[Authorize]
        public ActionResult <List<Delivery>> Get(
        [FromQuery] int addressId,
        [FromQuery] int saleId)
        {
            var query = _deliveryService.GetDelivery(addressId, saleId);
            var queryDTO = query.Select(x=>new DeliveryDTO(x));
            
            return Ok(queryDTO.ToList());
       
        }
    }
}

