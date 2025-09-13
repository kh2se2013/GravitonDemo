using Amazon.Util;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IEchoService, EchoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.MapControllers();

app.Run();

public interface IEchoService
{
    Task<EchoInfo> GetEchoInfoAsync();
}

public class EchoService : IEchoService
{
    private readonly HttpClient _httpClient;

    public EchoService()
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(5);
    }

    public async Task<EchoInfo> GetEchoInfoAsync()
    {
        var echoInfo = new EchoInfo
        {
            Runtime = GetRuntimeInfo(),
            OSVersion = GetOSVersion(),
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        try
        {
            // Try to get EC2 metadata
            echoInfo.InstanceId = EC2InstanceMetadata.InstanceId;
            echoInfo.InstanceType = EC2InstanceMetadata.InstanceType;
            echoInfo.InstanceAZ = EC2InstanceMetadata.AvailabilityZone;
        }
        catch
        {
            // If not running on EC2, use default values
            echoInfo.InstanceId = "local-development";
            echoInfo.InstanceType = "local";
            echoInfo.InstanceAZ = "local-az";
        }

        return echoInfo;
    }

    private string GetRuntimeInfo()
    {
        return $"{RuntimeInformation.FrameworkDescription}, {Environment.Version}";
    }

    private string GetOSVersion()
    {
        return RuntimeInformation.OSDescription;
    }
}

public class EchoInfo
{
    public string Runtime { get; set; } = string.Empty;
    public string InstanceId { get; set; } = string.Empty;
    public string InstanceType { get; set; } = string.Empty;
    public string InstanceAZ { get; set; } = string.Empty;
    public string OSVersion { get; set; } = string.Empty;
    public string Timestamp { get; set; } = string.Empty;
}