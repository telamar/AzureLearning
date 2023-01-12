using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

string connectionString = "<NAMESPACE CONNECTION STRING>";
string queueName = "az204-queue";

var client = new ServiceBusClient(connectionString);
ServiceBusSender sender = client.CreateSender(queueName);
ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

try
{
	messageBatch.TryAddMessage(new ServiceBusMessage("Custom Message 1"));
	messageBatch.TryAddMessage(new ServiceBusMessage("Custom Message 2"));

	await sender.SendMessagesAsync(messageBatch);

	Console.WriteLine("Messages has been published to the queue");
}
finally
{
	messageBatch.Dispose();
	await client.DisposeAsync();
	await sender.DisposeAsync();
}

Console.WriteLine("Press any key to end the application");
Console.ReadKey();