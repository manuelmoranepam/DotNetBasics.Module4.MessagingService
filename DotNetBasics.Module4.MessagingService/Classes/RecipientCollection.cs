using DotNetBasics.Module4.MessagingService.Models;

namespace DotNetBasics.Module4.MessagingService.Classes
{
	internal class RecipientCollection
	{
		private readonly IList<Recipient> _recipientList;

		public RecipientCollection()
		{
			_recipientList = new List<Recipient>();
		}

		public void CreateRecipient(string name, string email, string phoneNumber)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException($"ERROR - The '{nameof(name)}' cannot be null or whitespace.", nameof(name));
			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentException($"ERROR - The '{nameof(email)}' cannot be null or whitespace.", nameof(email));
			if (string.IsNullOrWhiteSpace(phoneNumber))
				throw new ArgumentException($"ERROR - The '{nameof(phoneNumber)}' cannot be null or whitespace.", nameof(phoneNumber));

			var id = 0;

			if (_recipientList.Any())
			{
				id = _recipientList.Last().Id + 1;
			}

			_recipientList.Add(new Recipient(id, name, email, phoneNumber));
		}

		public IEnumerable<Recipient> GetRecipientList()
		{
			return _recipientList.AsEnumerable();
		}

		public void DeleteRecipient(Recipient recipient)
		{
			if (recipient is null)
				throw new ArgumentNullException(nameof(recipient), $"ERROR - The '{nameof(recipient)}' cannot be null.");
			try
			{
				if (_recipientList.Any())
				{
					_recipientList.Remove(recipient);
				}
			}
			catch (Exception)
			{
				throw new InvalidOperationException("ERROR - Unable to delete the recipient.");
			}
		}
	}
}
