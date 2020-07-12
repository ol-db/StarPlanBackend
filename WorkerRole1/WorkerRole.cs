using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using WebServer.HTTP;
using WebServer.HTTP.Routing;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("WorkerRole1 is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            Resource pupils = new Resource("pupils");

            HTTPResponse pupilsResponse = new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "pupils"
                        );

            pupils.AddMethodRoute(new MethodRoute(
                    HTTPRequest.RequestMethodType.GET,
                    pupilsResponse
                    )
                );

            Resource year9 = new Resource("year9");

            HTTPResponse year9Response = new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "year 9"
                        );

            year9.AddMethodRoute(new MethodRoute(
                    HTTPRequest.RequestMethodType.GET,
                    year9Response
                    )
                );

            Resource year10 = new Resource("year10");

            HTTPResponse year10Response = new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "year 10"
                        );

            year10.AddMethodRoute(new MethodRoute(
                    HTTPRequest.RequestMethodType.GET,
                    year10Response
                    )
                );

            pupils.AddSubResource(year10);
            pupils.AddSubResource(year9);

            Resource school = new Resource("school");

            HTTPResponse schoolResponse = new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "school"
                        );

            school.AddMethodRoute(new MethodRoute(
                    HTTPRequest.RequestMethodType.GET,
                    schoolResponse
                    )
                );

            HTTPServer Server = new HTTPServer(3000, school);

            Server.AddSubRoute(pupils);

            Server.Listen();

            bool result = base.OnStart();

            Trace.TraceInformation("WorkerRole1 has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole1 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole1 has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }
    }
}
