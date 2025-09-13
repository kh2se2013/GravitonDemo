using Microsoft.AspNetCore.Mvc;

namespace GravitonDemo.Controllers;

[ApiController]
[Route("/")]
public class EchoController : ControllerBase
{
    private readonly IEchoService _echoService;

    public EchoController(IEchoService echoService)
    {
        _echoService = echoService;
    }

    [HttpGet]
    public async Task<IActionResult> Echo()
    {
        var echoInfo = await _echoService.GetEchoInfoAsync();
        
        var html = $@"
<html>
<body>
    <h1>Graviton University .NET Demo</h1>
    <p>Instance Type: {echoInfo.InstanceType}</p>
    <p>Instance ID: {echoInfo.InstanceId}</p>
    <p>Runtime is: {echoInfo.Runtime}</p>
    <p>OS Version is: {echoInfo.OSVersion}</p>
    <p>Instance AZ is: {echoInfo.InstanceAZ}</p>
    <p>TimeStamp is: {echoInfo.Timestamp}</p>
</body>
</html>";

        return Content(html, "text/html");
    }
}