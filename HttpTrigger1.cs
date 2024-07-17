using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace FunctionAPP
{
    public static class HttpFunction
    {
        [Function("HttpFunction")]
        public static HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
            FunctionContext executionContext)
        {
            // Retrieve the logger for the current execution context
            var logger = executionContext.GetLogger("HttpFunction");
            logger.LogInformation("Processing HTTP request.");

            // Create an HTTP response with status code OK (200)
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            
            // Write a response message to the HTTP response body
            response.WriteString("Welcome to .NET isolated worker !!");

            // Log the response information
            logger.LogInformation("Response created and sent.");

            return response;
        }
    }
}