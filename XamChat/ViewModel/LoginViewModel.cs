using System;
using System.Threading.Tasks;
namespace XamChat
{
	public class LoginViewModel:BaseViewModel
	{
		public string Username{get;set;}
		public string Password{get;set;}
		public async Task Login()
		{
			if (string.IsNullOrEmpty (Username))
				throw new Exception ("Username is blank");
			if (string.IsNullOrEmpty (Password))
				throw new Exception ("Password is blank");
			IsBusy = true;
			try{
				setting.User=await service.Login(Username,Password);
				setting.Save();
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}

