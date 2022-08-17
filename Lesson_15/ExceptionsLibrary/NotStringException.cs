using System;

namespace ExceptionsLibrary
{
    public class NotStringException:Exception
    {
        public NotStringException(string message) : base(message) { }
    }
}
