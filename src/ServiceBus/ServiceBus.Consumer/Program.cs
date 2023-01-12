using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

string connectionString = "<NAMESPACE CONNECTION STRING>";
string queueName = "az204-queue";

var client = new ServiceBusClient(connectionString);
ServiceBusProcessor processor = client.CreateProcessor(queueName);

try
{
	processor.ProcessMessageAsync += async args =>
	{
		Console.WriteLine($"Received: {args.Message.Body.ToString()}");

		await args.CompleteMessageAsync(args.Message);
	};

	processor.ProcessErrorAsync += args =>
	{
		Console.WriteLine(args.Exception.ToString());
		return Task.CompletedTask;
	};

	await processor.StartProcessingAsync();

	Console.WriteLine("Wait for a minute and then press any key to end the processing");
	Console.ReadKey();

	Console.WriteLine("\nStopping the receiver...");

	await processor.StopProcessingAsync();
	
	Console.WriteLine("Stopped receiving messages");
}
finally
{
	await client.DisposeAsync();
	await processor.DisposeAsync();
}

Console.WriteLine("Press any key to end the application");
Console.ReadKey();
