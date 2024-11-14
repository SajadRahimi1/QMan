namespace QMan.Domain.Entities.Base;

public static class StatusMessageGenerator
{
    public static string GetMessage(int statusCode) => statusCode switch
    {
        200 => "با موفقیت انجام شد",
        404 => "آیتم یافت نشد",
        403 => "شما به این بخش دسترسی ندارید",
        _ => ""
    };
}