using System;

namespace Fenrisulfr
{
    public class ComputerSaysNoException : Exception
    {
        public ComputerSaysNoException(string message)
            : base(message)
        {
        }
    }
}