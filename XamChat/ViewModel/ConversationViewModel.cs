using System;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace XamChat
{
	public class ConversationViewModel:BaseViewModel
	{		
		public Conversation[] conversations{get;set;}
		public Conversation conversation{ get; set;}
		public MessageM[] messages{get;set;}
		public string text{get;set;}
		public async Task getConversations()
		{
			if (setting == null)
				throw new Exception ("Not Log In");
			IsBusy = true;
			try{
				conversations=await service.getConversation(setting.User.Id);
			}
			finally{
				IsBusy = false;	
			}
		}
		public async Task getMessages()
		{
			if (conversation==null)
				throw new Exception ("No Conversation");
			IsBusy = true;
			try
			{
				messages=await service.getMessage(conversation.Id);
			}
			finally{
				IsBusy = false;
			}
		}
		public async Task senMessage()
		{
			if (setting == null)
				throw new Exception ("Not Log In");
			if (conversation==null)
				throw new Exception ("No Conversation");
			IsBusy = true;
			try
			{
				var message=await service.sendMessage(new MessageM
					{
						ConversationId=conversation.Id,
						UserId=setting.User.Id,
						Username=setting.User.Username,
						Text=text,
						Id=100
					});
				var messages=new List<MessageM>();
				if(this.messages!=null)
					messages.AddRange(this.messages);
				messages.Add(message);
				this.messages=messages.ToArray();
			}
			finally{
				IsBusy = false;
			}
		}
	}
}

