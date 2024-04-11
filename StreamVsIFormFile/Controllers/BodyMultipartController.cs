using HttpMultipartParser;
using Microsoft.AspNetCore.Mvc;
using StreamVsIFormFile.Attributes;

namespace StreamVsIFormFile.Controllers;

[ApiController]
[Route("[controller]")]
public class BodyMultipartController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public BodyMultipartController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }


    [HttpPost("Multipart")]
    [DisableFormValueModelBinding]
    public async Task PostMultipart(CancellationToken cancellationToken)
    {
        using (var readStream = Request.Body)
        {
            await using (var fileStream =
                         new FileStream($"E:\\Test_files\\results\\Multipart{Path.GetRandomFileName()}",
                             FileMode.Create))
            {
                await CopyFromMultipart(readStream, fileStream, cancellationToken);
            }
        }
    }

    private async Task CopyFromMultipart(Stream stream, Stream resultStream, CancellationToken cancellationToken)
    {
        var parser = new StreamingMultipartFormDataParser(stream, binaryBufferSize: 1024 * 64);
        //parser.ParameterHandler += part => { };
        parser.FileHandler += (name, fileName, type, disposition, buffer, bytes, number, properties) =>
        {
            if (fileName == "testFileName")
                resultStream.Write(buffer, 0, bytes);
        };
        await parser.RunAsync(cancellationToken);
    }
    
    [HttpPost("MultipartMultiple")]
    [DisableFormValueModelBinding]
    public async Task PostMultipartMultiple(CancellationToken cancellationToken)
    {
        using (var readStream = Request.Body)
        {
            var dictionary = new Dictionary<string, Stream>(2);
            await using (var fileStream1 =
                         new FileStream($"E:\\Test_files\\results\\MultipartMultiple{Path.GetRandomFileName()}",
                             FileMode.Create))
            {
                dictionary.Add("test.mp4", fileStream1);

                await using (var fileStream2 =
                             new FileStream($"E:\\Test_files\\results\\MultipartMultiple{Path.GetRandomFileName()}",
                                 FileMode.Create))
                {
                    dictionary.Add("test2.pdf", fileStream2);

                    await CopyFromMultipartMultiple(readStream, dictionary, cancellationToken);
                }
            }
        }
    }

    private async Task CopyFromMultipartMultiple(Stream stream, Dictionary<string, Stream> streamsDict, CancellationToken cancellationToken)
    {
        var parser = new StreamingMultipartFormDataParser(stream, binaryBufferSize: 1024 * 64);
        parser.FileHandler += (name, fileName, type, disposition, buffer, bytes, number, properties) =>
        {
            streamsDict[fileName].Write(buffer, 0, bytes);
        };
        await parser.RunAsync(cancellationToken);
    }
}