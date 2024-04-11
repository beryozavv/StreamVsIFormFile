using Microsoft.AspNetCore.Mvc;

namespace StreamVsIFormFile.Controllers;

[ApiController]
[Route("[controller]")]
public class FormFileController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public FormFileController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }


    [HttpPost]
    public async Task Post(IFormFile formFile, CancellationToken cancellationToken)
    {
        await using (var readStream = formFile.OpenReadStream())
        {
            await using (var fileStream =
                         new FileStream($"E:\\Test_files\\results\\FormFile{Path.GetRandomFileName()}", FileMode.Create))
            {
                await readStream.CopyToAsync(fileStream, cancellationToken);
            }
        }
    }
}