using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Functions
{
    public static class HttpExampleFunction
    {
        [FunctionName("HttpExampleFunction")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
			string htmlResponse = ConvertQueryToHtmlResponse(req);

			return new ContentResult()
			{
				Content = htmlResponse,
				ContentType = "text/html",
				StatusCode = 200
			};
        }

        private static string ConvertQueryToHtmlResponse(HttpRequest req)
        {
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("<h1>Your request contains the following params:</h1>");
			stringBuilder.AppendLine("<ul>");

			foreach (var queryParam in req.Query)
			{
				stringBuilder.AppendLine($"<li>Param: {queryParam.Key}, value: {queryParam.Value}</li>");
			}

			stringBuilder.AppendLine("</ul>");

			return stringBuilder.ToString();
		}
	}
}
