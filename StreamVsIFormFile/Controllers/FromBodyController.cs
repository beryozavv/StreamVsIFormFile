using HttpMultipartParser;
using Microsoft.AspNetCore.Mvc;
using StreamVsIFormFile.Attributes;

namespace StreamVsIFormFile.Controllers;

[ApiController]
[Route("[controller]")]
public class FromBodyController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public FromBodyController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }


    [HttpPost("Stream")]
    [DisableFormValueModelBinding]
    [RequestSizeLimit(300000000)]
    public async Task PostStream(CancellationToken cancellationToken)
    {
        await using (var readStream = Request.Body)
        {
            await using (var fileStream =
                         new FileStream($"E:\\Test_files\\results\\FromBody{Path.GetRandomFileName()}",
                             FileMode.Create))
            {
                await readStream.CopyToAsync(fileStream, cancellationToken);
            }
        }
    }
}