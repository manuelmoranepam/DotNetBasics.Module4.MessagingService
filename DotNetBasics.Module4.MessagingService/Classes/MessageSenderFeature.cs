using DotNetBasics.Module4.MessagingService.Enums;
using DotNetBasics.Module4.MessagingService.Extensions;
using DotNetBasics.Module4.MessagingService.Interfaces;
using DotNetBasics.Module4.MessagingService.Models;

namespace DotNetBasics.Module4.MessagingService.Classes
{
	internal static class MessageSenderFeature
	{
		public static ServiceType ReadServiceType(string command)
		{
			if (string.IsNullOrWhiteSpace(command))
				throw new ArgumentException($"ERROR - The '{nameof(command)}' cannot be null or whitespace.", nameof(command));

			var splitCommand = command.Split(" --type:");

			var isServiceType = Enum.TryParse<ServiceType>(splitCommand[1], true, out var serviceType);

			if (!isServiceType)
			{
				throw new InvalidCastException($"ERROR - The provided type '{splitCommand[1]}' is not a valid service type.");
			}

			return serviceType;
		}

		public static void SendMessage(Message message, Recipient recipient, ServiceType serviceType)
		{
			if (message is null)
				throw new ArgumentNullException(nameof(message), $"ERROR - The '{nameof(message)}' cannot be null.");
			if (recipient is null)
				throw new ArgumentNullException(nameof(recipient), $"ERROR - The '{nameof(recipient)}' cannot be null.");

			var messageSender = GetMessageSender(serviceType);

			messageSender.SendMessage(message, recipient);
		}

		public static void SendMessage(MessageCollection messageCollection, Recipient recipient, ServiceType serviceType)
		{
			if (messageCollection is null)
				throw new ArgumentNullException(nameof(messageCollection), $"ERROR - The '{nameof(messageCollection)}' cannot be null.");
			if (recipient is null)
				throw new ArgumentNullException(nameof(recipient), $"ERROR - The '{nameof(recipient)}' cannot be null.");

			var messageSender = GetMessageSender(serviceType);

			messageSender.SendMessage(messageCollection, recipient);
		}

		public static void SendMessage(MessageCollection messageCollection, Importance importance, Recipient recipient, ServiceType serviceType)
		{
			if (messageCollection is null)
				throw new ArgumentNullException(nameof(messageCollection), $"ERROR - The '{nameof(messageCollection)}' cannot be null.");
			if (recipient is null)
				throw new ArgumentNullException(nameof(recipient), $"ERROR - The '{nameof(recipient)}' cannot be null.");

			var importanceList = messageCollection.GetMessageList(importance);

			if (importanceList.Any())
			{
				var messageSender = GetMessageSender(serviceType);

				importanceList
					.ToList()
					.ForEach(message => messageSender.SendMessage(message, recipient));
			}
			else
			{
				throw new ArgumentNullException(nameof(messageCollection), $"INFO - The message collection does not contain {importance} level messages.\n");
			}
		}

		private static IMessageSender GetMessageSender(ServiceType serviceType)
		{
			return serviceType switch
			{
				ServiceType.Sms => new SmsSender(),
				ServiceType.Email => new EmailSender(),
				ServiceType.Pop => new PopSender(),
				_ => throw new ArgumentNullException(nameof(serviceType), $"ERROR - The provided type {serviceType} is not a valid service type."),
			};
		}
	}
}
