using DotNetBasics.Module4.MessagingService.Models;

namespace DotNetBasics.Module4.MessagingService.Classes
{
	internal class RecipientFeature
	{
		public static void CreateRecipient(RecipientCollection recipientCollection)
		{
			if (recipientCollection is null)
				throw new ArgumentNullException(nameof(recipientCollection), $"ERROR - The '{nameof(recipientCollection)}' cannot be null.");

			var name = ReadRecipientName();

			var email = ReadRecipientEmail();

			var phoneNumber = ReadRecipientPhoneNumber();

			recipientCollection.CreateRecipient(name, email, phoneNumber);
		}

		private static string ReadRecipientName()
		{
			Console.WriteLine("Enter the Name:");

			var name = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException($"ERROR - The '{nameof(name)}' cannot be null or whitespace.", nameof(name));

			return name;
		}

		private static string ReadRecipientEmail()
		{
			Console.WriteLine("Enter the Email:");

			var email = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentException($"ERROR - The '{nameof(email)}' cannot be null or whitespace.", nameof(email));

			return email;
		}

		private static string ReadRecipientPhoneNumber()
		{
			Console.WriteLine("Enter the Phone Number:");

			var phoneNumber = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(phoneNumber))
				throw new ArgumentException($"ERROR - The '{nameof(phoneNumber)}' cannot be null or whitespace.", nameof(phoneNumber));

			return phoneNumber;
		}

		public static int ReadId(string command)
		{
			if (string.IsNullOrWhiteSpace(command))
				throw new ArgumentException($"ERROR - The '{nameof(command)}' cannot be null or whitespace.", nameof(command));

			var splitCommand = command.Split(" recipient --id:");

			var isolatedId = splitCommand[1].Split();

			var isId = int.TryParse(isolatedId[0], out int id);

			if (isId)
			{
				return id;
			}
			else
			{
				throw new InvalidCastException($"ERROR - The provided id '{isolatedId}' is not valid.");
			}
		}

		private static void ListRecipient(Recipient recipient)
		{
			if (recipient is null)
				throw new ArgumentNullException(nameof(recipient), $"ERROR - The '{nameof(recipient)}' cannot be null.");

			Console.WriteLine($"Id: {recipient.Id}");
			Console.WriteLine($"Name: {recipient.Name}");
			Console.WriteLine($"Email: {recipient.Email}");
			Console.WriteLine($"Phone Number: {recipient.PhoneNumber}\n");
		}

		public static void ListRecipient(RecipientCollection recipientCollection)
		{
			if (recipientCollection is null)
				throw new ArgumentNullException(nameof(recipientCollection), $"The '{nameof(recipientCollection)}' cannot be null.");

			var list = recipientCollection.GetRecipientList();

			if (list.Any())
			{
				list.ToList()
					.ForEach(ListRecipient);
			}
			else
			{
				throw new ArgumentNullException(nameof(recipientCollection), "ERROR - The recipient collection is empty.");
			}
		}

		public static void ListRecipient(RecipientCollection recipientCollection, int id)
		{
			if (recipientCollection is null)
				throw new ArgumentNullException(nameof(recipientCollection), $"The '{nameof(recipientCollection)}' cannot be null.");

			var list = recipientCollection.GetRecipientList();

			if (list.Any())
			{
				var specifiedRecipient = list.Where(recipient => recipient.Id == id)
					.FirstOrDefault();

				if (specifiedRecipient is not null)
				{
					ListRecipient(specifiedRecipient);
				}
				else
				{
					throw new IndexOutOfRangeException($"ERROR - The recipient id '{id}' is not present in the recipient collection.");
				}
			}
			else
			{
				throw new ArgumentNullException(nameof(recipientCollection), "ERROR - The recipient collection is empty.");
			}
		}

		public static void DeleteRecipient(RecipientCollection recipientCollection, int id)
		{
			if (recipientCollection is null)
				throw new ArgumentNullException(nameof(recipientCollection), $"The '{nameof(recipientCollection)}' cannot be null.");

			var list = recipientCollection.GetRecipientList();

			if (list.Any())
			{
				var specifiedRecipient = list.Where(recipient => recipient.Id == id)
					.FirstOrDefault();

				if (specifiedRecipient is not null)
				{
					recipientCollection.DeleteRecipient(specifiedRecipient);
				}
				else
				{
					throw new IndexOutOfRangeException($"ERROR - The recipient id '{id}' is not present in the recipient collection.");
				}
			}
			else
			{
				throw new ArgumentNullException(nameof(recipientCollection), "ERROR - The recipient collection is empty.");
			}
		}

		public static Recipient GetRecipient(RecipientCollection recipientCollection, int id)
		{
			if (recipientCollection is null)
				throw new ArgumentNullException(nameof(recipientCollection), $"The '{nameof(recipientCollection)}' cannot be null.");

			var list = recipientCollection.GetRecipientList();

			if (list.Any())
			{
				var specifiedRecipient = list.Where(recipient => recipient.Id == id)
					.FirstOrDefault();

				if (specifiedRecipient is not null)
				{
					return specifiedRecipient;
				}
				else
				{
					throw new IndexOutOfRangeException($"ERROR - The recipient id '{id}' is not present in the recipient collection.");
				}
			}
			else
			{
				throw new ArgumentNullException(nameof(recipientCollection), "ERROR - The recipient collection is empty.");
			}
		}
	}
}
