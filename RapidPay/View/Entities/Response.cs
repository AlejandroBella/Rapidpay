using RapidPay.Business.Helpers;

namespace RapidPay.View.Entities
{
    public class Response
    {
        public ErrorCodes Code { get; set; }
        public string Message { get; set; }
    }
}
