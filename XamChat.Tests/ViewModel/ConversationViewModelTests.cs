using System;
using NUnit.Framework;
namespace XamChat.Tests
{
	public class ConversationViewModelTests
	{
		ConversationViewModel conversationViewModel;
		ISetting settings;
		[SetUp]
		public void Setup()
		{
			Test.Setup ();
			settings=ServiceContainer.Resolve<ISetting> ();
			conversationViewModel = new ConversationViewModel ();
		}
		[Test]
		public void getConversationSuccessfully()
		{
			conversationViewModel.setting.User = new User{Id=1};
			conversationViewModel.getConversations ().Wait();
			Assert.That (conversationViewModel.conversations, Is.Not.Null);
		}
		[Test]
		public void getMessages()
		{
			conversationViewModel.setting.User = new User{Id=1};
			conversationViewModel.getConversations ().Wait();
			conversationViewModel.conversation = conversationViewModel.conversations [0];
			conversationViewModel.getMessages ().Wait();
			Assert.That (conversationViewModel.messages, Is.Not.Null);
		}
	}
}

