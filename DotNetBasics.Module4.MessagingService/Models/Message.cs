using DotNetBasics.Module4.MessagingService.Enums;

namespace DotNetBasics.Module4.MessagingService.Models
{
	internal class Message
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public Importance Importance { get; set; }

		public Message(int id, string title, string body, Importance importance)
		{
			if (string.IsNullOrWhiteSpace(title))
				throw new ArgumentException($"ERROR - The '{nameof(title)}' cannot be null or whitespace.", nameof(title));
			if (string.IsNullOrWhiteSpace(body))
				throw new ArgumentException($"ERROR - The '{nameof(body)}' cannot be null or whitespace.", nameof(body));

			Id = id;
			Title = title;
			Body = body;
			Importance = importance;
		}
	}
}
