namespace DataAccessLibrary_AutoAuctioneer.Util;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; set; }
    public List<T>? DataList { get;}
    public string? ErrorMessage { get; }

    private Result(bool isSuccess,T? data,  List<T>? dataList, string? errorMessage)
    {
        IsSuccess = isSuccess;
        Data = data;
        DataList = dataList;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(T data) =>
        new Result<T>(true, data, default, null);

    public static Result<T> SuccessList(List<T> dataList) =>
        new Result<T>(true, default, dataList, null);

    public static Result<T> Failure(string errorMessage) =>
        new Result<T>(false, default, default, errorMessage);

    public static Result<T> SuccessNoData() =>
        new Result<T>(true, default, default, null);
}