using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.Exceptions;
using DEVinCar.Domain.DTOs;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace DEVinCar.Api.Config
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync (HttpContext context)
        {
            try {
                await _next(context);
            }
            catch (Exception ex){
                await TratamentoExcecao(context, ex);
            }
        }

        private Task TratamentoExcecao(HttpContext context, Exception ex)
        {
            HttpStatusCode status; 
            string message; 

            switch(ex)
            {
                case AlreadyExistsException: 
                    status = HttpStatusCode.NotAcceptable;
                    message = ex.Message;
                    break;
                case NotExistsException: 
                    status = HttpStatusCode.NoContent;
                    message = ex.Message;
                    break;
                case NotFoundException: 
                    status = HttpStatusCode.NotFound;
                    message = ex.Message;
                    break;
                case InvalidLoginException: 
                    status = HttpStatusCode.Unauthorized;
                    message = ex.Message;
                    break;
                case IncompatibleValuesException: 
                    status = HttpStatusCode.NotAcceptable;
                    message = ex.Message;
                    break;    
                default:
                    status = HttpStatusCode.InternalServerError;
                    message = "An error has occurred. Please, contact IT support";
                    break;
            }

            var response = new ErrorDTO(message);
            context.Response.StatusCode = (int) status;

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}