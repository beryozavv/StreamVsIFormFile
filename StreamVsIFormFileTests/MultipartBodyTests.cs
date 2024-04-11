using NUnit.Framework;

namespace StreamVsIFormFileTests;

public class MultipartBodyTests
{
    [Test]
    public async Task MultipartTest()
    {
        using (var httpClient = new HttpClient())
        {
            await using (var stream = new FileStream("E:\\Test_files\\test2.pdf", FileMode.Open, FileAccess.Read,
                             FileShare.ReadWrite))
            {
                using (var streamContent = new StreamContent(stream))
                {
                    using (var multipartFormDataContent = new MultipartFormDataContent("Upload test"))
                    {
                        multipartFormDataContent.Add(streamContent, "MyTestFile", "testFileName");
                        using (var message =
                               await httpClient.PostAsync("https://localhost:7084/BodyMultipart/Multipart",
                                   multipartFormDataContent))
                        {
                            var readAsStringAsync = await message.Content.ReadAsStringAsync();
                            TestContext.WriteLine(readAsStringAsync);
                        }
                    }
                }
            }
        }
    }

    [Test]
    public async Task MultipartLargeTest()
    {
        using (var httpClient = new HttpClient())
        {
            await using (var stream = new FileStream("E:\\Test_files\\test.mp4", FileMode.Open, FileAccess.Read,
                             FileShare.ReadWrite))
            {
                var streamContent = new StreamContent(stream);
                using (var multipartFormDataContent = new MultipartFormDataContent("Upload test"))
                {
                    multipartFormDataContent.Add(streamContent, "MyTestFile", "testFileName");
                    using (var message =
                           await httpClient.PostAsync("https://localhost:7084/BodyMultipart/Multipart",
                               multipartFormDataContent))
                    {
                        var readAsStringAsync = await message.Content.ReadAsStringAsync();
                        TestContext.WriteLine(readAsStringAsync);
                    }
                }
            }
        }
    }

    [Test]
    public async Task MultipartMultipleFilesTest()
    {
        using (var httpClient = new HttpClient())
        {
            using (var multipartFormDataContent = new MultipartFormDataContent("Upload test"))
            {
                using (var stream1 = new FileStream("E:\\Test_files\\test.mp4", FileMode.Open, FileAccess.Read,
                           FileShare.ReadWrite))
                {
                    // todo just for test
                    using (var stream2 = new FileStream("E:\\Test_files\\test2.pdf", FileMode.Open, FileAccess.Read,
                               FileShare.ReadWrite))
                    {
                        var streamContent1 = new StreamContent(stream1);
                        multipartFormDataContent.Add(streamContent1, "MyFile1", "test.mp4");

                        var streamContent2 = new StreamContent(stream2);
                        multipartFormDataContent.Add(streamContent2, "MyFile2", "test2.pdf");

                        using (var message =
                               await httpClient.PostAsync("https://localhost:7084/BodyMultipart/MultipartMultiple",
                                   multipartFormDataContent))
                        {
                            var readAsStringAsync = await message.Content.ReadAsStringAsync();
                            TestContext.WriteLine(readAsStringAsync);
                        }
                    }
                }
            }
        }
    }
}