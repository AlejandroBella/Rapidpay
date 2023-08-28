namespace RapidPay.Business.Helpers
{
    public enum DataAction
    {
        Create = 1,
        Update = 2,
        Delete = 3,
    }

    public enum ErrorCodes
    {
        Ok = 0,
        InvalidObject = 1,
        DuplicatedItem = 2,
        NotFound = 3,
        SystemError = 4


    }
}
