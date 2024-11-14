namespace QMan.Domain.Entities.Base;

public class BaseResponse
{
    public int StatusCode { get; set; } = 200;

    public object? Data { get; set; }

    public string? Token { get; set; }

    public string? Message=> MessageSetter ?? StatusMessageGenerator.GetMessage(StatusCode);


    public string? MessageSetter { set; private get; }
}