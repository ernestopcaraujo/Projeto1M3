using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVinCar.Domain.Exceptions
{
    public class IncompatibleValuesException : Exception
    {
        public IncompatibleValuesException(string message) : base(message)
        {
            
        }
    }
}