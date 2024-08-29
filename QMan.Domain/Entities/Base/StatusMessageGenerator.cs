namespace QMan.Infrastructure.Helpers;

public static class StatusMessageGenerator
{
    public static string GetMessage(int statusCode) => statusCode switch
    {
        200 => "با موفقیت انجام شد",
        404 => "آیتم یافت نشد",
        _ => ""
    };
}