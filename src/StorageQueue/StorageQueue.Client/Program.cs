using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

string queueName = "mycustomqueue";
string connectionString = "<Connection String Storage Account>";

var queueClient = new QueueClient(connectionString, queueName);

await queueClient.CreateIfNotExistsAsync();

Response<SendReceipt> sentMessageResponse = await queueClient.SendMessageAsync("Hello World!");

Console.WriteLine($"Message has been published. The message ID is {sentMessageResponse.Value.MessageId}");

Response<PeekedMessage> peekedMessageResponse = await queueClient.PeekMessageAsync();

Console.WriteLine($"Message has been peeked. The message is {peekedMessageResponse.Value.Body.ToString()}");

Response<QueueMessage> lockedMessageResponse = await queueClient.ReceiveMessageAsync();

Console.WriteLine($"Message has been received and locked. The message is {lockedMessageResponse.Value.Body.ToString()}");

Response<UpdateReceipt> updatedMessageResponse = await queueClient.UpdateMessageAsync(
	lockedMessageResponse.Value.MessageId,
	lockedMessageResponse.Value.PopReceipt,
	"Updated Hello World",
	TimeSpan.FromSeconds(5));

Console.WriteLine($"Message has been updated. The message will be visible on {updatedMessageResponse.Value.NextVisibleOn}");
Console.WriteLine("Sleep for 5 seconds");

await Task.Delay(TimeSpan.FromSeconds(5));

Response<QueueMessage> newLockedMessageResponse = await queueClient.ReceiveMessageAsync();

Console.WriteLine($"Message has been received and locked. The message is {newLockedMessageResponse.Value.Body.ToString()}");

Response deletedResponse = await queueClient.DeleteMessageAsync(
	newLockedMessageResponse.Value.MessageId,
	newLockedMessageResponse.Value.PopReceipt);

Console.WriteLine($"Message has been deleted");

await queueClient.DeleteIfExistsAsync();

Console.WriteLine($"Queue has been deleted");
Console.ReadKey();
