using System;
using NUnit.Framework;
namespace XamChat.Tests
{
	public class LoginViewModelTests
	{
		LoginViewModel loginViewModel;
		ISetting settings;
		[SetUp]
		public void setup()
		{
			Test.Setup ();
			settings = ServiceContainer.Resolve<ISetting> ();
			loginViewModel = new LoginViewModel ();
		}
		[Test]
		public void loginSuccessfully()
		{			
			loginViewModel.Username="admin";
			loginViewModel.Password="admin";
			loginViewModel.Login ().Wait ();
			Assert.That (settings.User, Is.Not.Null);
		}
	}
}

