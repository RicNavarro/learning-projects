namespace OrderFlow.Api.Configuration;

public class CacheOptions
{
    public const string SectionName = "Cache";

    public int OrdersExpirationMinutes { get; set; }
}