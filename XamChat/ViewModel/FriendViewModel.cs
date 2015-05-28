using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace XamChat
{
	public class FriendViewModel:BaseViewModel
	{
		public User[] friendlist{ get; set;}
		public string username{ get; set;}
		public async Task getFriends()
		{
			if (setting.User == null)
				throw new Exception ("Not Log In");
			IsBusy = true;
			try{
				friendlist=await service.GetFriend(setting.User.Id);
			}
			finally{
				IsBusy = false;
			}
		}
		public async Task addFriend()
		{
			if (setting.User == null)
				throw new Exception ("Not Log In");
			if (string.IsNullOrEmpty (username))
				throw new Exception ("Username is blank");
			IsBusy = true;
			try{
				var friend=await service.addFriend(setting.User.Id,username);
				List<User> friends=new List<User>();
				if(friendlist!=null)
					friends.AddRange(friendlist);
				friends.Add(friend);
				friendlist=friends.OrderBy(f => f.Username).ToArray();
			}
			finally{
				IsBusy = false;
			}
		}
	}
}

