using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;

namespace Functions
{
	public class QueueExampleFunction
	{
		[FunctionName("QueueExampleFunction")]
		public static void Run(
			[QueueTrigger("myqueue-items-source", Connection = "AzureWebJobsStorage")] string myQueueItem,
			[Queue("myqueue-items-target", Connection = "AzureWebJobsStorage")] out string myQueueItemCopy)
		{
			myQueueItemCopy = myQueueItem;
		}
	}
}
