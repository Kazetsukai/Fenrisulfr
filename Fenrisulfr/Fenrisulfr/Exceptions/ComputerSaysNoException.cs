using System;

namespace Fenrisulfr.Exceptions
{
    public class ComputerSaysNoException : Exception
    {
        public ComputerSaysNoException(string message)
            : base(message)
        {
        }
    }
}