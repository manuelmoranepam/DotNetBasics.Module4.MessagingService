using DotNetBasics.Module4.MessagingService.Classes;
using DotNetBasics.Module4.MessagingService.Models;

namespace DotNetBasics.Module4.MessagingService.Interfaces
{
	internal interface IMessageSender
	{
		void SendMessage(Message message, Recipient recipient);
		void SendMessage(MessageCollection messageCollection, Recipient recipient);
	}
}
