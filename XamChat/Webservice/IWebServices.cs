using System;
using System.Threading.Tasks;
namespace XamChat
{
	public interface IWebServices
	{		
		Task<User> Login(string username,string password);
		Task<User> Register (User user);
		Task<User[]> GetFriend(int userid);
		Task<User> addFriend(int userid,string username);
		Task<Conversation[]> getConversation(int userid);
		Task<MessageM[]> getMessage(int conversationID);
		Task<MessageM> sendMessage(MessageM message);
	}
}

