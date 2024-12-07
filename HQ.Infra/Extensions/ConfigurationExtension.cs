namespace HQ.Infra.Extensions;
using Microsoft.Extensions.Configuration;

public static class ConfigurationExtension
{
    public static string ConnectionString(this IConfigurationManager configuration)
    {
        return configuration.GetConnectionString("Connection")!;
    }

    public static bool IsUnitTestEnvironment(this IConfigurationManager configuration)
    {
        var inMemoryTestText = configuration.GetSection("InMemoryTest").Value;
        return inMemoryTestText != null && Convert.ToBoolean(inMemoryTestText);
    }
}