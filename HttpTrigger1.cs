using System;
using System.Diagnostics;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Dynatrace.OpenTelemetry;
using OpenTelemetry.Trace;

namespace FunctionApp
{
    public class HttpExample
    {
        private readonly TracerProvider _tracerProvider;

        public HttpExample(TracerProvider tracerProvider)
        {
            _tracerProvider = tracerProvider;
        }

        [Function("HttpExample")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
                                    FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("HttpExample");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            using (var activity = new Activity("HttpExampleFunction"))
            {
                activity.Start();
                Activity.Current = activity;

                try
                {
                    // Your function logic here
                    response.WriteString("Hello from Azure Functions Demo Uma 1 !");
                }
                finally
                {
                    activity.Stop();

                    // Record any exceptions if needed
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        activity.SetStatus(Status.Error);
                    }
                }
            }

            return response;
        }
    }
}
