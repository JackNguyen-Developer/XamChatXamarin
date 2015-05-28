using System;

namespace XamChat
{
	public class Conversation
	{		
		public int Id {get;set;}
		public User UserOwner{ get; set;}
		public User UserOwnerFriend{ get; set;}
		public int UserId{get;set;}
		public string Username{get;set;}
	}
}

