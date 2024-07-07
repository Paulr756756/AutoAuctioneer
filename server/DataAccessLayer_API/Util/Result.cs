namespace DataAccessLayer_AutoAuctioneer.Util;

public class Result<T>
{
    public List<T>? Data = new(); /*{ get; set; }*/

    private Result(bool isSuccess, List<T>? data, string? errorMessage)
    {
        IsSuccess = isSuccess;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }

    public static Result<T> Success(List<T> data)
    {
        return new Result<T>(true, data, null);
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>(false, default, errorMessage);
    }

    public static Result<T> SuccessNoData()
    {
        return new Result<T>(true, default, null);
    }
}