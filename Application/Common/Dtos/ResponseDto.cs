namespace Application.Common.Dtos;

public class ResponseDto
{
    public int ErrorCode { get; set; }
    public string? ErrorDescription { get; set; }
    public Object? Data { get; set; }
}

public class ResponseDto<T> : ResponseDto
{
    public new T? Data { get; set; }
}
