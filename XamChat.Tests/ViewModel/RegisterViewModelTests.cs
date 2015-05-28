using System;
using NUnit.Framework;
namespace XamChat.Tests
{
	public class RegisterViewModelTests
	{
		ISetting settings;
		RegisterViewModel registerViewModel;
		[SetUp]
		public void Setup()
		{
			Test.Setup ();
			settings = ServiceContainer.Resolve<ISetting> ();
			registerViewModel = new RegisterViewModel ();
		}
		[Test]
		public void registerSuccessfully()
		{
			registerViewModel.Username="Username";
			registerViewModel.Password="Password";
			registerViewModel.ComfirmPassword="Password";
			registerViewModel.Register ().Wait();
			Assert.That (settings.User, Is.Not.Null);
		}
	}
}

