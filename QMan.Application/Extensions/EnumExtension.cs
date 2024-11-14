using QMan.Application.Dtos.Base;

namespace QMan.Application.Extensions;

public static class EnumExtension
{
    public static string? GetName(this UserRole value) 
    {
        return Enum.GetName(typeof(UserRole), value);
    }
}