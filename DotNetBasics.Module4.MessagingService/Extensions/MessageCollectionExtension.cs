using DotNetBasics.Module4.MessagingService.Classes;
using DotNetBasics.Module4.MessagingService.Enums;
using DotNetBasics.Module4.MessagingService.Models;

namespace DotNetBasics.Module4.MessagingService.Extensions
{
	internal static class MessageCollectionExtension
	{
		public static IEnumerable<Message> GetMessageList(this MessageCollection messageCollection, Importance importance)
		{
			if (messageCollection is null)
				throw new ArgumentNullException(nameof(messageCollection), $"ERROR - The '{nameof(messageCollection)}' cannot be null.");

			var messagesList = messageCollection.GetMessageList()
				.Where(message => message.Importance == importance);

			return messagesList;
		}
	}
}
