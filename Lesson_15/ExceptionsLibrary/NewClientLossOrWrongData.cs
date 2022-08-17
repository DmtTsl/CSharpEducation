using System;

namespace ExceptionsLibrary
{
    public class NewClientLossOrWrongData:Exception
    {
        public NewClientLossOrWrongData (string message):base(message)
        {

        }
    }
}
