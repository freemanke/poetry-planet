using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace PoetryPlanet.Services;

public class HttpService
{
    private readonly ILogger<HttpService> logger;
    private HttpClient httpClient;

    public HttpService(ILogger<HttpService> logger)
    {
        this.logger = logger;
        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
        httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://home.freemanke.com:60011") };
    }

    public void Download(string url, string path)
    {
        var result = httpClient.GetByteArrayAsync(new Uri(url)).Result;
        File.WriteAllBytes(path, result);
        
    }
}