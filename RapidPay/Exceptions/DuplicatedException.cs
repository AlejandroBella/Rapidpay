namespace RapidPay.Exceptions
{
    public class DuplicatedItemException : Exception
    {
        override public string Message
        {
            get
            {
                return "The item already exists";
            }
        }
    }
}
