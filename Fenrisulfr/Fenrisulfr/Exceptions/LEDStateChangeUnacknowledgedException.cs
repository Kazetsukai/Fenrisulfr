using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Exceptions
{
    class LEDStateChangeUnacknowledgedException : Exception
    {
        public  LEDStateChangeUnacknowledgedException(string message) : base(message)
        {
        }
    }
}
