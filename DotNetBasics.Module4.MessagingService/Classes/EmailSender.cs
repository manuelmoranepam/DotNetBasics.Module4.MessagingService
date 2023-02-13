using DotNetBasics.Module4.MessagingService.Interfaces;
using DotNetBasics.Module4.MessagingService.Models;

namespace DotNetBasics.Module4.MessagingService.Classes
{
	internal class EmailSender : IMessageSender
	{
		public void SendMessage(Message message, Recipient recipient)
		{
			if (message is null)
				throw new ArgumentNullException(nameof(message), $"ERROR - The '{nameof(message)}' cannot be null.");
			if (recipient is null)
				throw new ArgumentNullException(nameof(recipient), $"ERROR - The '{nameof(recipient)}' cannot be null.");

			Console.WriteLine($"Sending Email to: {recipient.Name}.");
			Console.WriteLine($"Email Address: {recipient.Email}.");
			Console.WriteLine($"Message Title: {message.Title}.");
			Console.WriteLine($"Message Importance: {message.Importance}.");
			Console.WriteLine($"Message Body: {message.Body}.\n");
		}

		public void SendMessage(MessageCollection messageCollection, Recipient recipient)
		{
			if (messageCollection is null)
				throw new ArgumentNullException(nameof(messageCollection), $"ERROR - The '{nameof(messageCollection)}' cannot be null.");
			if (recipient is null)
				throw new ArgumentNullException(nameof(recipient), $"ERROR - The '{nameof(recipient)}' cannot be null.");

			var list = messageCollection.GetMessageList();

			if (list.Any())
			{
				list.ToList()
					.ForEach(message => SendMessage(message, recipient));
			}
			else
			{
				throw new ArgumentNullException(nameof(messageCollection), "ERROR - The message collection is empty.");
			}
		}
	}
}
