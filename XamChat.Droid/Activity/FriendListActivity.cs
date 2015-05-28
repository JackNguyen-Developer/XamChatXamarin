
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamChat.Droid
{
	[Activity (Label = "FriendListActivity")]			
	public class FriendListActivity : Activity
	{
		FriendViewModel fvm=ServiceContainer.Resolve<FriendViewModel>();
		public User[] friends;
		ListView lv_friendlist;
		public async void getFriend()
		{
			fvm.setting.User = Session.UserSession;
			await fvm.getFriends ();
			friends = fvm.friendlist;
			FriendListAdapter adt = new FriendListAdapter (this, friends);
			lv_friendlist.Adapter = adt;
		}
		protected override void OnResume ()
		{
			base.OnResume ();
		}	
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.FriendListActivity);
			Create_Actionbar ();
			lv_friendlist = FindViewById<ListView> (Resource.Id.lv_friendlist);
			getFriend ();

			// Create your application here
		}
		public void Create_Actionbar()
		{
			ActionBar.SetDisplayShowTitleEnabled(false);
			ActionBar.SetDisplayHomeAsUpEnabled(false);
			ActionBar.SetHomeButtonEnabled(false);
			ActionBar.SetDisplayShowCustomEnabled(true);

			ActionBar.SetIcon(Android.Resource.Color.Transparent);
			ActionBar.Title = "";
			ActionBar.LayoutParams actionbarParams = 
				new ActionBar.LayoutParams(ActionBar.LayoutParams.MatchParent,ActionBar.LayoutParams.MatchParent);			
			View customview = LayoutInflater.Inflate (Resource.Menu.menu_custom,null);
			TextView title = customview.FindViewById<TextView> (Resource.Id.text_title);
			title.Text = "Friends";
			ImageButton btn_back = customview.FindViewById<ImageButton> (Resource.Id.btn_back);
			btn_back.Click += delegate {
				Finish ();
			};
			ImageButton btn_add = customview.FindViewById<ImageButton> (Resource.Id.btn_add);
			ImageButton btn_friendlist = customview.FindViewById<ImageButton> (Resource.Id.btn_friendlist);
			btn_friendlist.Visibility = ViewStates.Gone;
			View homeicon=FindViewById<View>(Android.Resource.Id.Home);
			((View) homeicon.Parent).Visibility=ViewStates.Gone;
			ActionBar.SetCustomView (customview, actionbarParams);
		}
	}
}

