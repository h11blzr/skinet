namespace Core.Interfaces
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object resposne, TimeSpan timeToLeave);
        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}