
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
	[Activity (Label = "ConversationActivity")]			
	public class ConversationActivity : BaseActivity<ConversationViewModel>
	{	
		ListView lv_conversation;
		ConversationViewModel cvm = ServiceContainer.Resolve<ConversationViewModel> ();
		public Conversation[] conversations{get;set;}
		public int[] conver_id;
		protected async void getConversation()
		{
			await cvm.getConversations ();
			conversations = cvm.conversations;
			ConversationListViewAdapter adt = new ConversationListViewAdapter (this, conversations);
			lv_conversation.Adapter = adt;
			lv_conversation.ItemClick += itemclick;
		}
		void itemclick(object sender,AdapterView.ItemClickEventArgs e)
		{
			Conversation item = conversations [e.Position];
			Intent i = new Intent (this, typeof(MessageActivity));
			i.PutExtra ("ConversationID", item.Id);
			i.PutExtra ("UserOwner", item.UserOwner.Id);
			i.PutExtra ("UserOwnerFriend", item.UserOwnerFriend.Id);
			StartActivity (i);
		}
		protected override void OnCreate (Bundle bundle)
		{			
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ConversationActivity);
			Create_Actionbar ();
			cvm.setting.User = Session.UserSession;
			lv_conversation = FindViewById<ListView> (Resource.Id.lv_conversation);
			getConversation();
			// Create your application here
		}
		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) 
			{
			case Android.Resource.Id.Home:
				Finish ();
				return true;
			default:
				return base.OnOptionsItemSelected (item);
			}

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
			title.Text = "Conversations";
			ImageButton btn_back = customview.FindViewById<ImageButton> (Resource.Id.btn_back);
			btn_back.Click += delegate {
				Finish ();
			};
			ImageButton btn_add = customview.FindViewById<ImageButton> (Resource.Id.btn_add);
			btn_add.Visibility = ViewStates.Gone;
			ImageButton btn_friendlist = customview.FindViewById<ImageButton> (Resource.Id.btn_friendlist);
			btn_friendlist.Click += delegate {
				StartActivity(typeof(FriendListActivity));
			};
			View homeicon=FindViewById<View>(Android.Resource.Id.Home);
			((View) homeicon.Parent).Visibility=ViewStates.Gone;
			ActionBar.SetCustomView (customview, actionbarParams);
		}
	}
}

