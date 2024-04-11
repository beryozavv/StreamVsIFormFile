using NUnit.Framework;

namespace StreamVsIFormFileTests;

public class FormFileTests
{
    [Test]
    public async Task FormFileTest()
    {
        using (var httpClient = new HttpClient())
        {
            await using (var stream = new FileStream("E:\\Test_files\\test2.pdf", FileMode.Open, FileAccess.Read,
                             FileShare.ReadWrite))
            {
                var streamContent = new StreamContent(stream);
                using (var multipartFormDataContent = new MultipartFormDataContent("Upload test"))
                {
                    multipartFormDataContent.Add(streamContent, "formFile", "testFileName");
                    using (var message =
                           await httpClient.PostAsync("https://localhost:7084/FormFile", multipartFormDataContent))
                    {
                        var readAsStringAsync = await message.Content.ReadAsStringAsync();
                        TestContext.WriteLine(readAsStringAsync);
                    }
                }
            }
        }
    }

    [Test]
    public async Task FormFileLargeTest()
    {
        using (var httpClient = new HttpClient())
        {
            await using (var stream = new FileStream("E:\\Test_files\\test.mp4", FileMode.Open, FileAccess.Read,
                             FileShare.ReadWrite))
            {
                var streamContent = new StreamContent(stream);
                using (var multipartFormDataContent = new MultipartFormDataContent("Upload test"))
                {
                    multipartFormDataContent.Add(streamContent, "formFile", "testFileName");
                    using (var message =
                           await httpClient.PostAsync("https://localhost:7084/FormFile", multipartFormDataContent))
                    {
                        var readAsStringAsync = await message.Content.ReadAsStringAsync();
                        TestContext.WriteLine(readAsStringAsync);
                    }
                }
            }
        }
    }
}