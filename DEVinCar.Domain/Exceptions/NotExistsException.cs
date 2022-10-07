using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVinCar.Domain.Exceptions
{
    public class NotExistsException : Exception
    {
        public NotExistsException(string message) : base(message)
        {
            
        }
    }
}