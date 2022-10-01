using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVinCar.Domain.Exceptions
{
    public class NoDeliveryFoundException : Exception
    {
        public NoDeliveryFoundException(string message) : base(message)
        {
            
        }
    }
}