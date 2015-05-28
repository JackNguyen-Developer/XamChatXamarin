using System;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace XamChat
{
	public class FakeService:IWebServices
	{		
		public int SleepDurtion{ get; set;}
		public FakeService()
		{
			SleepDurtion = 0;
		}
		private Task Sleep()
		{
			return Task.Delay (SleepDurtion);
		}
		public async Task<User> Register (User user)
		{
			await Sleep();
			return user;
		}
		public async Task<User> Login(string username, string password)
		{
			await Sleep();
			return new User { Id = 1, Username=username};
		}
		public async Task<User[]> GetFriend(int userid)
		{
			await Sleep();
			return new User[] 
			{
				new User{Id=2,Username="o=bobama"},
				new User{Id=3,Username="bobloblaw"}
			};
		}
		public async Task<User> addFriend(int userid,string username)
		{
			await Sleep ();
			return new User{Id=4,Username=username};
		}
		public async Task<Conversation[]> getConversation(int userid)
		{
			await Sleep();
			List<Conversation> list = new List<Conversation> ();
			return new Conversation[]
			{
				new Conversation { Id = 1, UserId = 2,UserOwner=new User{Id=1},UserOwnerFriend=new User{Id=5,Username="Friend"} },
				new Conversation { Id = 1, UserId = 3 ,UserOwner=new User{Id=1},UserOwnerFriend=new User{Id=6,Username="GF"}},
				new Conversation { Id = 1, UserId = 4 ,UserOwner=new User{Id=1},UserOwnerFriend=new User{Id=7,Username="Anonymous"}},
			};
		}
		public async Task<MessageM[]> getMessage(int conversationId)
		{
			await Sleep ();
			return new[]
			{
				new MessageM
				{
					Id = 1,
					ConversationId = conversationId,
					UserId = 2,
					Text = "Hey",
				},
				new MessageM
				{
					Id = 2,
					ConversationId = conversationId,
					UserId = 1,
					Text = "What's Up?",
				},
				new MessageM
				{
					Id = 3,
					ConversationId = conversationId,
					UserId = 2,
					Text = "Have you seen that new movie?",
				},
				new MessageM
				{
					Id = 4,
					ConversationId = conversationId,
					UserId = 1,
					Text = "It's great!",
				},
			};
		}
		public async Task<MessageM> sendMessage(MessageM message)
		{
			await Sleep();
			return message;
		}
	}
}

