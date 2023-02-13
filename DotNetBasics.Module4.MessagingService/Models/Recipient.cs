namespace DotNetBasics.Module4.MessagingService.Models
{
	internal class Recipient
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }

		public Recipient(int id, string name, string email, string phoneNumber)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException($"ERROR - The '{nameof(name)}' cannot be null or whitespace.", nameof(name));
			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentException($"ERROR - The '{nameof(email)}' cannot be null or whitespace.", nameof(email));
			if (string.IsNullOrWhiteSpace(phoneNumber))
				throw new ArgumentException($"ERROR - The '{nameof(phoneNumber)}' cannot be null or whitespace.", nameof(phoneNumber));

			Id = id;
			Name = name;
			Email = email;
			PhoneNumber = phoneNumber;
		}
	}
}
