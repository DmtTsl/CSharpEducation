using System;

namespace ExceptionsLibrary
{
    public class NotIntException:Exception
    {
        public NotIntException(string message) : base(message) { }
    }
}
