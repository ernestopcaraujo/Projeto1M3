using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVinCar.Domain.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message) : base(message)
        {
            
        }
    }
}