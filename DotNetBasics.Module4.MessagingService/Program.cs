using DotNetBasics.Module4.MessagingService.Classes;
using DotNetBasics.Module4.MessagingService.Helpers;

internal class Program
{
	private static void Main(string[] args)
	{
		var _command = string.Empty;
		var _helpFilePath = $"{AppContext.BaseDirectory}Files/Help.txt";
		var _messageCollection = new MessageCollection();
		var _recipientCollection = new RecipientCollection();

		PrintTitle();

		while (_command != "exit")
		{
			try
			{
				Console.WriteLine("Type a command or type help to get the list of commands.\n");

				_command = ReadCommand().ToLower();

				ExecuteCommand(_command, _helpFilePath, _messageCollection, _recipientCollection);
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}.\n");
			}
		}
	}

	private static void PrintTitle()
	{
		Console.WriteLine("==================================================");
		Console.WriteLine(".NET Basics - Module 4 - Messaging Service");
		Console.WriteLine("==================================================\n");
	}

	private static string ReadCommand()
	{
		Console.Write("> ");

		var command = Console.ReadLine();

		Console.WriteLine();

		if (string.IsNullOrWhiteSpace(command))
			throw new ArgumentException($"ERROR - The '{nameof(command)}' cannot be null or whitespace.", nameof(command));

		return command;
	}

	private static void ExecuteCommand(string command, string helpFile, MessageCollection messageCollection, RecipientCollection recipientCollection)
	{
		if (string.IsNullOrWhiteSpace(command))
			throw new ArgumentException($"ERROR - The '{nameof(command)}' cannot be null or whitespace.", nameof(command));
		if (string.IsNullOrWhiteSpace(helpFile))
			throw new ArgumentException($"ERROR - The '{nameof(helpFile)}' cannot be null or whitespace.", nameof(helpFile));
		if (messageCollection is null)
			throw new ArgumentNullException(nameof(messageCollection), $"ERROR - The '{nameof(messageCollection)}' cannot be null.");
		if (recipientCollection is null)
			throw new ArgumentNullException(nameof(recipientCollection), $"ERROR - The '{nameof(recipientCollection)}' cannot be null.");

		var isValidCommand = ExecuteMessageFeature(command, messageCollection);

		if (!isValidCommand)
			isValidCommand = ExecuteRecipientFeature(command, recipientCollection);
		if (!isValidCommand)
			isValidCommand = ExecuteSendMessageFeature(command, messageCollection, recipientCollection);
		if (!isValidCommand)
			isValidCommand = ExecuteApplicationFeature(command, helpFile);
		if (!isValidCommand)
			throw new InvalidOperationException("ERROR - Invalid command.");
	}

	private static bool ExecuteMessageFeature(string command, MessageCollection messageCollection)
	{
		if (string.IsNullOrWhiteSpace(command))
			throw new ArgumentException($"ERROR - The '{nameof(command)}' cannot be null or whitespace.", nameof(command));
		if (messageCollection is null)
			throw new ArgumentNullException(nameof(messageCollection), $"ERROR - The '{nameof(messageCollection)}' cannot be null.");

		var isValidCommand = false;

		if (command is "create message")
		{
			isValidCommand = true;

			MessageFeature.CreateMessage(messageCollection);

			Console.WriteLine($"INFO - Message created successfully.\n");
		}
		else if (command.StartsWith("list message --id:"))
		{
			isValidCommand = true;

			var id = MessageFeature.ReadId(command);

			MessageFeature.ListMessage(messageCollection, id);
		}
		else if (command is "list message --all")
		{
			isValidCommand = true;

			MessageFeature.ListMessage(messageCollection);
		}
		else if (command.StartsWith("list message --importance:"))
		{
			isValidCommand = true;

			var importance = MessageFeature.ReadImportanceLevel(command);

			MessageFeature.ListMessage(messageCollection, importance);
		}
		else if (command.StartsWith("delete message --id:"))
		{
			isValidCommand = true;

			var id = MessageFeature.ReadId(command);

			MessageFeature.DeleteMessage(messageCollection, id);

			Console.WriteLine($"INFO - Message deleted successfully.\n");
		}

		return isValidCommand;
	}

	private static bool ExecuteRecipientFeature(string command, RecipientCollection recipientCollection)
	{
		if (string.IsNullOrWhiteSpace(command))
			throw new ArgumentException($"ERROR - The '{nameof(command)}' cannot be null or whitespace.", nameof(command));
		if (recipientCollection is null)
			throw new ArgumentNullException(nameof(recipientCollection), $"ERROR - The '{nameof(recipientCollection)}' cannot be null.");

		var isValidCommand = false;

		if (command is "create recipient")
		{
			isValidCommand = true;

			RecipientFeature.CreateRecipient(recipientCollection);

			Console.WriteLine($"INFO - Recipient created successfully.\n");
		}
		else if (command.StartsWith("list recipient --id:"))
		{
			isValidCommand = true;

			var id = RecipientFeature.ReadId(command);

			RecipientFeature.ListRecipient(recipientCollection, id);
		}
		else if (command is "list recipient --all")
		{
			isValidCommand = true;

			RecipientFeature.ListRecipient(recipientCollection);
		}
		else if (command.StartsWith("delete recipient --id:"))
		{
			isValidCommand = true;

			var id = RecipientFeature.ReadId(command);

			RecipientFeature.DeleteRecipient(recipientCollection, id);

			Console.WriteLine($"INFO - Recipient deleted successfully.\n");
		}

		return isValidCommand;
	}

	private static bool ExecuteSendMessageFeature(string command, MessageCollection messageCollection, RecipientCollection recipientCollection)
	{
		if (string.IsNullOrWhiteSpace(command))
			throw new ArgumentException($"ERROR - The '{nameof(command)}' cannot be null or whitespace.", nameof(command));
		if (messageCollection is null)
			throw new ArgumentNullException(nameof(messageCollection), $"ERROR - The '{nameof(messageCollection)}' cannot be null.");
		if (recipientCollection is null)
			throw new ArgumentNullException(nameof(recipientCollection), $"ERROR - The '{nameof(recipientCollection)}' cannot be null.");

		var isValidCommand = false;

		if (command.StartsWith("send message --id:") && command.Contains(" recipient --id:") && command.Contains(" --type:"))
		{
			isValidCommand = true;

			var messageId = MessageFeature.ReadId(command);
			var message = MessageFeature.GetMessage(messageCollection, messageId);

			var recipientId = RecipientFeature.ReadId(command);
			var recipient = RecipientFeature.GetRecipient(recipientCollection, recipientId);

			var serviceType = MessageSenderFeature.ReadServiceType(command);

			MessageSenderFeature.SendMessage(message, recipient, serviceType);
		}
		else if (command.StartsWith("send message --all") && command.Contains(" recipient --id:") && command.Contains(" --type:"))
		{
			isValidCommand = true;

			var recipientId = RecipientFeature.ReadId(command);
			var recipient = RecipientFeature.GetRecipient(recipientCollection, recipientId);

			var serviceType = MessageSenderFeature.ReadServiceType(command);

			MessageSenderFeature.SendMessage(messageCollection, recipient, serviceType);
		}
		else if (command.StartsWith("send message --importance:") && command.Contains(" recipient --id:") && command.Contains(" --type:"))
		{
			isValidCommand = true;

			var importance = MessageFeature.ReadImportanceLevel(command);

			var recipientId = RecipientFeature.ReadId(command);
			var recipient = RecipientFeature.GetRecipient(recipientCollection, recipientId);

			var serviceType = MessageSenderFeature.ReadServiceType(command);

			MessageSenderFeature.SendMessage(messageCollection, importance, recipient, serviceType);
		}

		return isValidCommand;
	}

	private static bool ExecuteApplicationFeature(string command, string helpFile)
	{
		if (string.IsNullOrWhiteSpace(command))
			throw new ArgumentException($"ERROR - The '{nameof(command)}' cannot be null or whitespace.", nameof(command));
		if (string.IsNullOrWhiteSpace(helpFile))
			throw new ArgumentException($"ERROR - The '{nameof(helpFile)}' cannot be null or whitespace.", nameof(helpFile));

		var isValidCommand = false;

		if (command is "help")
		{
			isValidCommand = true;

			PrintHelp(helpFile);
		}
		else if (command is "clear")
		{
			isValidCommand = true;

			Console.Clear();

			PrintTitle();
		}
		else if (command is "exit")
		{
			isValidCommand = true;

			PrintExitMessage();
		}

		return isValidCommand;
	}

	private static void PrintHelp(string helpFile)
	{
		var fileHelper = new FileHelper(helpFile);
		var fileContent = fileHelper.GetFileContent();

		Console.WriteLine(fileContent + "\n");
	}

	private static void PrintExitMessage()
	{
		Console.WriteLine("INFO - Press any key to continue.\n");

		Console.Read();
	}
}