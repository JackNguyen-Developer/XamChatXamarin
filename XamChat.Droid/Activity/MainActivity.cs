using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SlidingMenuSharp;
using Parse;
using System.Linq;
using System.Collections.Generic;
namespace XamChat.Droid
{
	[Activity (Label = "XamChat.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity :BaseActivity<LoginViewModel>
	{			
		Button btn_login;
		Button btn_register;
		EditText editTxt_username;
		EditText editTxt_password;
		SlidingMenu menu ;
		//LoginViewModel viewModel = ServiceContainer.Resolve<LoginViewModel> ();
		protected void findView()
		{
			btn_login = FindViewById<Button> (Resource.Id.btn_login);
			btn_register = FindViewById<Button> (Resource.Id.btn_register);
			editTxt_password = FindViewById<EditText> (Resource.Id.editTxt_Password);
			editTxt_username = FindViewById<EditText> (Resource.Id.editTxt_Username);
		}
		public async void Login(object sender , EventArgs args)
		{
			viewModel.Username=editTxt_username.Text;
			viewModel.Password=editTxt_password.Text;
			try{
				await viewModel.Login();
				if (viewModel.setting.User != null) {					
					Session.UserSession = viewModel.setting.User;
					//Intent intent = new Intent (this, typeof(FriendListActivity));
					Finish ();
					StartActivity (typeof(ConversationActivity));
				}
			}
			catch(Exception e) {
				DisplayError (e);
			}
		}
		public void noti(string noti)
		{			 
			Toast.MakeText (this, noti, ToastLength.Long).Show();
		}
		protected override void OnResume ()
		{
			base.OnResume ();
			editTxt_password.Text = editTxt_username.Text = string.Empty;
		}
		public void creatingSlidingMenu()
		{
			menu = new SlidingMenu(this);	
			menu.Mode = MenuMode.Left;
			menu.TouchModeAbove=TouchMode.Margin;
			menu.ShadowWidthRes=Resource.Dimension.shadow_width;
			menu.ShadowDrawableRes=Resource.Drawable.shadow;
			menu.BehindOffsetRes=Resource.Dimension.slidingmenu_offset;
			menu.FadeDegree=0.35f;
			menu.AttachToActivity (this, SlideStyle.Content);
			menu.SetMenu (Resource.Layout.Menu);
		}
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MainActivity);
			this.ActionBar.Hide ();
			creatingSlidingMenu ();
			findView ();
			btn_register.Click+= async (sender, e) =>  {
				var query = ParseObject.GetQuery ("Conversation").WhereEqualTo("UserOwner",1);
				IEnumerable<Parse.ParseObject> result = await query.FindAsync ();
				foreach (Parse.ParseObject value in result) {
					editTxt_username.Text+=value.Get<int>("Id").ToString()+" "+value.Get<int>("UserOwner")+" "+value.Get<int>("UserFriendOwner");					
				}
			
			};
			btn_login.Click += Login;
		}	
	}
}


