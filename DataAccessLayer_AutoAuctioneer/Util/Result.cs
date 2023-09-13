namespace DataAccessLayer_AutoAuctioneer.Util;

public class Result<T>
{
    public bool IsSuccess { get; }
    public List<T>? Data = new List<T>();/*{ get; set; }*/
    public string? ErrorMessage { get; }

    private Result(bool isSuccess, List<T>? data, string? errorMessage)
    {
        IsSuccess = isSuccess;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(List<T> data) =>
        new Result<T>(true, data, null);

    public static Result<T> Failure(string errorMessage) =>
        new Result<T>(false, default, errorMessage);

    public static Result<T> SuccessNoData() =>
        new Result<T>(true, default, null);
}