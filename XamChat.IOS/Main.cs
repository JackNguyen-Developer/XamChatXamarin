using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace XamChat.IOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.

			//View Models
			ServiceContainer.Register<LoginViewModel>(() =>
				new LoginViewModel());
			ServiceContainer.Register<FriendViewModel>(() =>
				new FriendViewModel());
			ServiceContainer.Register<RegisterViewModel>(() =>
				new RegisterViewModel());
			ServiceContainer.Register<ConversationViewModel>(() =>
				new ConversationViewModel());
			//Models
			ServiceContainer.Register<ISetting>(() =>
				new FakeSetting());
			ServiceContainer.Register<IWebServices>(() =>
				new FakeService());

			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
