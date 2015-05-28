using System;
using System.Threading.Tasks;
namespace XamChat
{
	public class RegisterViewModel:BaseViewModel
	{
		public string Username{ get; set;}
		public string Password{get;set;}
		public string ComfirmPassword{get;set;}
		public async Task Register()
		{
			if (string.IsNullOrEmpty (Username))
				throw new Exception ("Username is blank");
			if (string.IsNullOrEmpty (Password))
				throw new Exception ("Password is blank");
			if (string.IsNullOrEmpty (ComfirmPassword))
				throw new Exception ("Comfirm password is blank");
			IsBusy = true;
			try{
				setting.User=await service.Register(new User{Username=this.Username,password=this.Password});
				setting.Save();
			}
			finally{
				IsBusy = false;
			}
		}
	}
}

