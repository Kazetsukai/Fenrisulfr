using System;

namespace Fenrisulfr.Exceptions
{
    class LEDStateChangeUnacknowledgedException : Exception
    {
        public  LEDStateChangeUnacknowledgedException(string message) : base(message)
        {
        }
    }
}
