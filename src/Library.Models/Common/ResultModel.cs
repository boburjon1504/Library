namespace Library.Models.Common;

public class ResultModel<T>
{
    public T Data { get; set; } = default!;

    public string ErrorMessage { get; set; } = default!;
    public bool IsSuccess => Data is not null;
    public ResultModel(T data) => Data = data;

    public ResultModel(string error) => ErrorMessage = error;
}
