namespace QMan.Application.Services.Cache;

public static class CacheKeys
{
    public static string AccessCacheKey(int adminId) => "AccessCacheKey_" + adminId;
    public static string AllAccessCacheKey => "AllAccessCacheKey";
}