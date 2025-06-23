
using System.Net;

namespace TaskManagement.Core.Dto;

public record ResultDto
{
    public long? Id { get; set; }
    public bool IsSuccess { get; set; }
    public int HttpStatusCode { get; set; }
    public string CodeError { get; set; } = string.Empty;
    public string MessageError { get; set; } = string.Empty;

    public ResultDto()
    {
    }

    public ResultDto(bool isSucceed, int statusCode)
    {
        IsSuccess = isSucceed;
        HttpStatusCode = statusCode;
    }

    public ResultDto(int statusCode, string codeError, string messageError)
    {
        IsSuccess = false;
        HttpStatusCode = statusCode;
        CodeError = codeError;
        MessageError = messageError;
    }
}
