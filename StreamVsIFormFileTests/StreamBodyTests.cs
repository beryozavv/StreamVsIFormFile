using NUnit.Framework;

namespace StreamVsIFormFileTests;

public class StreamBodyTests
{
    [Test]
    public async Task FromBodyTest()
    {
        using (var httpClient = new HttpClient())
        {
            await using (var stream = new FileStream("E:\\Test_files\\test2.pdf", FileMode.Open, FileAccess.Read,
                             FileShare.ReadWrite))
            {
                using (var streamContent = new StreamContent(stream))
                {
                    using (var message =
                           await httpClient.PostAsync("https://localhost:7084/FromBody/Stream", streamContent))
                    {
                        var readAsStringAsync = await message.Content.ReadAsStringAsync();
                        TestContext.WriteLine(readAsStringAsync);
                    }
                }
            }
        }
    }

    [Test]
    public async Task FromBodyLargeTest()
    {
        using (var httpClient = new HttpClient())
        {
            await using (var stream = new FileStream("E:\\Test_files\\test.mp4", FileMode.Open, FileAccess.Read,
                             FileShare.ReadWrite))
            {
                using (var streamContent = new StreamContent(stream))
                {
                    using (var message =
                           await httpClient.PostAsync("https://localhost:7084/FromBody/Stream", streamContent))
                    {
                        var readAsStringAsync = await message.Content.ReadAsStringAsync();
                        TestContext.WriteLine(readAsStringAsync);
                    }
                }
            }
        }
    }
}