// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NewDemoEventGrid;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class EventGridBlobFunction
{
    private readonly ILogger _logger;
    private static readonly HttpClient _httpClient = new HttpClient();

    public EventGridBlobFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<EventGridBlobFunction>();
    }

    [Function("EventGridBlobFunction")]
    public async Task Run([EventGridTrigger] EventGridEvent eventGridEvent)
    {
        _logger.LogInformation($"Event received: {eventGridEvent.EventType}");

        try
        {
            var data = eventGridEvent.Data.ToObjectFromJson<JsonElement>();

            string blobUrl = data.GetProperty("url").GetString();
            string fileName = blobUrl.Split('/')[^1];

            _logger.LogInformation($"Blob uploaded: {fileName}");

            var payload = new
            {
                FileName = fileName,
                BlobUrl = blobUrl
            };

            var json = JsonSerializer.Serialize(payload);

            var response = await _httpClient.PostAsync(
            "https://prod-06.centralindia.logic.azure.com:443/workflows/85829883df864768a0dcacfdedf31ba0/triggers/When_an_HTTP_request_is_received/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2FWhen_an_HTTP_request_is_received%2Frun&sv=1.0&sig=SQ-zOiU4ZCS30cKcREO7_xAWRZujlQb2fvvW7JPApro", new StringContent(json, Encoding.UTF8, "application/json")
            );

            _logger.LogInformation($"Logic App response: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR: {ex.Message}");
            throw;
        }
    }
}