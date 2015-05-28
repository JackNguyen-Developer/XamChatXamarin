using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parse;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamChat.Droid
{
	[Application (Theme ="@android:style/Theme.Holo.Light")]			
	public class AppController : Application
	{
		public AppController(IntPtr javaReference,JniHandleOwnership tranfer):base(javaReference,tranfer)
		{	
		}
		public override void OnCreate ()
		{
			base.OnCreate ();

			ServiceContainer.Register<FriendViewModel> (() => new FriendViewModel ());
			ServiceContainer.Register<LoginViewModel> (() => new LoginViewModel ());
			ServiceContainer.Register<ConversationViewModel> (()=>new ConversationViewModel());
			ServiceContainer.Register<RegisterViewModel> (() => new RegisterViewModel ());
			//Service
			ServiceContainer.Register<IWebServices> (() => new ParseServices ());
			ServiceContainer.Register<ISetting> (() => new FakeSetting());	
			ParseClient.Initialize ("v5lLpyneLopQfiepI52AsdDlM1pR8YutPCBihxmy","JpzclP34JWemDAo6fTioz1l12gGbAHl6fpJiHcvT");
		}
	}
}