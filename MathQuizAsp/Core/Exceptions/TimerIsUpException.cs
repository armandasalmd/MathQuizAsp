using System;

namespace MathQuizAsp.Core.Exceptions
{
    public class TimerIsUpException : Exception
    {
        public TimerIsUpException() {}
        public TimerIsUpException(string message) : base(message) {}
        public TimerIsUpException(string message, Exception inner) : base(message, inner) {}
    }
}