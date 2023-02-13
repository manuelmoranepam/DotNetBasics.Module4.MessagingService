using DotNetBasics.Module4.MessagingService.Enums;
using DotNetBasics.Module4.MessagingService.Extensions;
using DotNetBasics.Module4.MessagingService.Models;

namespace DotNetBasics.Module4.MessagingService.Classes
{
	internal static class MessageFeature
	{
		public static void CreateMessage(MessageCollection messageCollection)
		{
			if (messageCollection is null)
				throw new ArgumentNullException(nameof(messageCollection), $"ERROR - The '{nameof(messageCollection)}' cannot be null.");

			var title = ReadMessageTitle();

			var body = ReadMessageBody();

			var importance = ReadImportanceLevel();

			messageCollection.CreateMessage(title, body, importance);
		}

		private static string ReadMessageTitle()
		{
			Console.WriteLine("Enter the Title:");

			var title = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(title))
				throw new ArgumentException($"ERROR - The '{nameof(title)}' cannot be null or whitespace.", nameof(title));

			return title;
		}

		private static string ReadMessageBody()
		{
			Console.WriteLine("Enter the Body:");

			var body = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(body))
				throw new ArgumentException($"ERROR - The '{nameof(body)}' cannot be null or whitespace.", nameof(body));

			return body;
		}

		private static Importance ReadImportanceLevel()
		{
			Console.WriteLine("Select the Importance Level: [Low / 0] [Medium / 1] [High / 2]");

			var importanceString = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(importanceString))
				throw new ArgumentNullException(nameof(importanceString), $"ERROR - The '{nameof(importanceString)}' cannot be null or whitepace.");

			var importance = Enum.Parse<Importance>(importanceString, true);

			if (Enum.IsDefined(importance))
			{
				return importance;
			}
			else
			{
				throw new InvalidCastException($"ERROR - Unable to cast '{importanceString}' into a valid importance leve.");
			}
		}

		public static Importance ReadImportanceLevel(string command)
		{
			if (string.IsNullOrWhiteSpace(command))
				throw new ArgumentException($"ERROR - The '{nameof(command)}' cannot be null or whitespace.", nameof(command));

			try
			{
				var importanceString = command.Split(" --importance:")[1].Split();

				var importance = Enum.Parse<Importance>(importanceString[0], true);

				if (!Enum.IsDefined(importance))
					throw new InvalidCastException($"ERROR - Unable to read the Importance level from the following command: '{command}'");

				return importance;
			}
			catch (Exception)
			{
				throw new InvalidCastException($"ERROR - Failed to read the Importance level from the following command: '{command}'.");
			}
		}

		public static int ReadId(string command)
		{
			if (string.IsNullOrWhiteSpace(command))
				throw new ArgumentException($"ERROR - The '{nameof(command)}' cannot be null or whitespace.", nameof(command));

			var splitCommand = command.Split(" message --id:");

			var isolatedId = splitCommand[1].Split();

			var isId = int.TryParse(isolatedId[0], out int id);

			if (isId)
			{
				return id;
			}
			else
			{
				throw new InvalidCastException($"ERROR - The provided id is not valid.");
			}
		}

		private static void ListMessage(Message message)
		{
			if (message is null)
				throw new ArgumentNullException(nameof(message), $"ERROR - The '{nameof(message)}' cannot be null.");

			Console.WriteLine($"Id: {message.Id}");
			Console.WriteLine($"Title: {message.Title}");
			Console.WriteLine($"Body: {message.Body}");
			Console.WriteLine($"Importance: {Enum.Parse(typeof(Importance), message.Importance.ToString())}\n");
		}

		public static void ListMessage(MessageCollection messageCollection)
		{
			if (messageCollection is null)
				throw new ArgumentNullException(nameof(messageCollection), $"The '{nameof(messageCollection)}' cannot be null.");

			var list = messageCollection.GetMessageList();

			if (list.Any())
			{
				list.ToList()
					.ForEach(ListMessage);
			}
			else
			{
				throw new ArgumentNullException(nameof(messageCollection), "ERROR - The message collection is empty.");
			}
		}

		public static void ListMessage(MessageCollection messageCollection, int id)
		{
			if (messageCollection is null)
				throw new ArgumentNullException(nameof(messageCollection), $"The '{nameof(messageCollection)}' cannot be null.");

			var list = messageCollection.GetMessageList();

			if (list.Any())
			{
				var specifiedMessage = list.Where(message => message.Id == id)
					.FirstOrDefault();

				if (specifiedMessage is not null)
				{
					ListMessage(specifiedMessage);
				}
				else
				{
					throw new ArgumentNullException(nameof(id), $"ERROR - The message id '{id}' is not present in the message collection.");
				}
			}
			else
			{
				throw new ArgumentNullException(nameof(messageCollection), "ERROR - The message collection is empty.");
			}
		}

		public static void ListMessage(MessageCollection messageCollection, Importance importance)
		{
			if (messageCollection is null)
				throw new ArgumentNullException(nameof(messageCollection), $"The '{nameof(messageCollection)}' cannot be null.");

			var list = messageCollection.GetMessageList(importance);

			if (list.Any())
			{
				list.ToList()
					.ForEach(ListMessage);
			}
			else
			{
				throw new ArgumentNullException(nameof(importance), $"ERROR - The message collection does not contain '{importance}' level messages.");
			}
		}

		public static void DeleteMessage(MessageCollection messageCollection, int id)
		{
			if (messageCollection is null)
				throw new ArgumentNullException(nameof(messageCollection), $"The '{nameof(messageCollection)}' cannot be null.");

			var list = messageCollection.GetMessageList();

			if (list.Any())
			{
				var specifiedMessage = list.Where(message => message.Id == id)
					.FirstOrDefault();

				if (specifiedMessage is not null)
				{
					messageCollection.DeleteMessage(specifiedMessage);
				}
				else
				{
					throw new IndexOutOfRangeException($"ERROR - The message id '{id}' is not present in the message collection.");
				}
			}
			else
			{
				throw new ArgumentNullException(nameof(messageCollection), "ERROR - The message collection is empty.");
			}
		}

		public static Message GetMessage(MessageCollection messageCollection, int id)
		{
			if (messageCollection is null)
				throw new ArgumentNullException(nameof(messageCollection), $"The '{nameof(messageCollection)}' cannot be null.");

			var list = messageCollection.GetMessageList();

			if (list.Any())
			{
				var specifiedMessage = list.Where(message => message.Id == id)
					.FirstOrDefault();

				if (specifiedMessage is not null)
				{
					return specifiedMessage;
				}
				else
				{
					throw new IndexOutOfRangeException($"ERROR - The message id '{id}' is not present in the message collection.");
				}
			}
			else
			{
				throw new IndexOutOfRangeException("INFO - The message collection is empty.");
			}
		}
	}
}
