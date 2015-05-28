using System;
using System.Threading.Tasks;
using Parse;
using System.Collections.Generic;
namespace XamChat
{
	public class ParseServices:IWebServices
	{
		public int SleepDurtion{ get; set;}
		public ParseServices()
		{
			SleepDurtion = 1;
		}
		private Task Sleep()
		{
			return Task.Delay (SleepDurtion);
		}
		public async Task<User> Register (User user)
		{			
			
			var account = ParseObject.GetQuery ("User").WhereExists ("Username");
			await account.FindAsync();
			return user;
		}
		public async Task<User> Login(string username, string password)
		{						
			var query=ParseObject.GetQuery("User")				
				.WhereEqualTo("Username",username)
				.WhereEqualTo("Password",password);
			ParseObject account=await query.FirstAsync();
			if (account != null)
				return new User { Id = account.Get<int> ("Id"), Username = username, password = password };
			else
				return null;
		}
		public async Task<User[]> GetFriend(int userid)
		{
			await Sleep();
			return new User[] 
			{
				new User{Id=2,Username="obobama"},
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
			List<Conversation> list = new List<Conversation> ();
			var query = (ParseObject.GetQuery ("Conversation").WhereEqualTo("UserOwner",userid)).Or(ParseObject.GetQuery ("Conversation").WhereEqualTo("UserFriendOwner",userid));
			IEnumerable<Parse.ParseObject> result = await query.FindAsync ();
			foreach (Parse.ParseObject value in result) {
				int userownerfriend=value.Get<int>("UserFriendOwner");
				int userowner = value.Get<int> ("UserOwner");
				Conversation item;
				if (userownerfriend != userid) {
					var query2 = (ParseObject.GetQuery("User").WhereEqualTo("Id",userownerfriend));
					ParseObject friend = await query2.FirstAsync ();
					item = new Conversation { 
						Id = value.Get<int> ("Id"),
						UserId = value.Get<int> ("UserOwner"),
						UserOwner = new User{ Id = value.Get<int> ("UserOwner") },
						UserOwnerFriend = new User{ Id = value.Get<int> ("UserFriendOwner"), Username = friend.Get<String> ("Username") } 					
					};
				} else {
					
					var query3 = ParseObject.GetQuery ("User").WhereEqualTo ("Id", userowner);
					ParseObject owner = await query3.FirstAsync ();
					string u = owner.Get<String> ("Username");
					item = new Conversation { 
						Id = value.Get<int> ("Id"),
						UserId = value.Get<int> ("UserOwner"),
						UserOwner = new User{ Id = value.Get<int> ("UserOwner") },
						UserOwnerFriend = new User{ Id = value.Get<int> ("UserFriendOwner"), Username = owner.Get<String> ("Username") } 					
					};
				}
				list.Add (item);
			}
			return list.ToArray ();
//			return new Conversation[]
//			{
//				new Conversation { Id = 1, UserId = 2,UserOwner=new User{Id=1},UserOwnerFriend=new User{Id=5,Username="Friend"} },
//				new Conversation { Id = 1, UserId = 3 ,UserOwner=new User{Id=1},UserOwnerFriend=new User{Id=6,Username="GF"}},
//				new Conversation { Id = 1, UserId = 4 ,UserOwner=new User{Id=1},UserOwnerFriend=new User{Id=7,Username="Anonymous"}},
//			};
		}
		public async Task<MessageM[]> getMessage(int conversationId)
		{
			List<MessageM> list = new List<MessageM> ();
			var query = ParseObject.GetQuery ("Message").WhereEqualTo("ConversationId",conversationId);
			IEnumerable<Parse.ParseObject> result = await query.FindAsync ();
			foreach (Parse.ParseObject value in result) {
				MessageM item=new MessageM
				{
					Id=value.Get<int>("Id"),
					UserId=value.Get<int>("UserId"),
					Username=value.Get<string>("Username"),
					ConversationId=value.Get<int>("ConversationId"),
					Text=value.Get<string>("Text")
				};
				list.Add (item);
			}
			return list.ToArray();
//			await Sleep ();
//			return new[]
//			{
//				new MessageM
//				{
//					Id = 1,
//					ConversationId = conversationId,
//					UserId = 2,
//					Text = "Hey",
//				},
//				new MessageM
//				{
//					Id = 2,
//					ConversationId = conversationId,
//					UserId = 1,
//					Text = "What's Up?",
//				},
//				new MessageM
//				{
//					Id = 3,
//					ConversationId = conversationId,
//					UserId = 2,
//					Text = "Have you seen that new movie?",
//				},
//				new MessageM
//				{
//					Id = 4,
//					ConversationId = conversationId,
//					UserId = 1,
//					Text = "It's great!",
//				},
//			};
		}
		public async Task<MessageM> sendMessage(MessageM message)
		{
			await Sleep();
			ParseObject newmess = new ParseObject ("Message");
			newmess["Id"] = message.Id;
			newmess ["ConversationId"] = message.ConversationId;
			newmess ["Text"] = message.Text;
			newmess ["UserId"] = message.UserId;
			newmess ["Username"] = message.Username;
			await newmess.SaveAsync();
			return message;
		}
	}
}

