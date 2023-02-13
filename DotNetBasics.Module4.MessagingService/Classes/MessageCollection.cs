using DotNetBasics.Module4.MessagingService.Enums;
using DotNetBasics.Module4.MessagingService.Models;

namespace DotNetBasics.Module4.MessagingService.Classes
{
	internal class MessageCollection
	{
		private readonly IList<Message> _messageList;

		public MessageCollection()
		{
			_messageList = new List<Message>();
		}

		public void CreateMessage(string title, string body, Importance importance)
		{
			if (string.IsNullOrWhiteSpace(title))
				throw new ArgumentException($"ERROR - The '{nameof(title)}' cannot be null or whitespace.", nameof(title));
			if (string.IsNullOrWhiteSpace(body))
				throw new ArgumentException($"ERROR - The '{nameof(body)}' cannot be null or whitespace.", nameof(body));

			var id = 0;

			if (_messageList.Any())
			{
				id = _messageList.Last().Id + 1;
			}

			_messageList.Add(new Message(id, title, body, importance));
		}

		public IEnumerable<Message> GetMessageList()
		{
			return _messageList.AsEnumerable();
		}

		public void DeleteMessage(Message message)
		{
			if (message is null)
				throw new ArgumentNullException(nameof(message), $"ERROR - The '{nameof(message)}' cannot be null.");
			try
			{
				if (_messageList.Any())
				{
					_messageList.Remove(message);
				}
			}
			catch (Exception)
			{
				throw new InvalidOperationException("ERROR - Unable to delete the message from the collection.");
			}
		}
	}
}
