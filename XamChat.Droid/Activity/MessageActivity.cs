
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
namespace XamChat.Droid
{
	[Activity (Label = "MessageActivity")]			
	public class MessageActivity : BaseActivity<ConversationViewModel>
	{		
		
		ListView lv_chat;
		Button btn_send;
		EditText editTxt_mess;
		int conversationId;
		List<MessageM> array_mess;
		async void  getmessagesfromservice()
		{
			await viewModel.getMessages ();
			array_mess = viewModel.messages.ToList();
			MessageAdapter adt = new MessageAdapter (this, array_mess);
			lv_chat.Adapter = adt;
		}
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MessageActivity);
			lv_chat = FindViewById<ListView> (Resource.Id.lv_chat);
			btn_send = FindViewById<Button> (Resource.Id.btn_send);
			editTxt_mess = FindViewById<EditText> (Resource.Id.editTxt_mess);
			btn_send.Click += sendMessage;
			lv_chat.Divider = null;
			lv_chat.ItemClick += delegate {				
			};
			Create_Actionbar ();
			conversationId=Intent.GetIntExtra ("ConversationID",0);
			if(conversationId!=0)
			{
				viewModel.conversation = new Conversation{ Id = conversationId };
				getmessagesfromservice ();
			}
		}
		public void sendMessage(object sender,EventArgs handler)
		{			
			string text = editTxt_mess.Text;
			if (!text.Equals ("")) {
				MessageM newMess = new MessageM
				{ 
					Text=editTxt_mess.Text,
					UserId=Session.UserSession.Id,
					Username=Session.UserSession.Username,
					ConversationId=conversationId,
					Id=1
				};
				viewModel.setting.User = Session.UserSession;
				viewModel.text = editTxt_mess.Text;
				viewModel.conversation = new Conversation{ Id=conversationId};
				array_mess.Add (newMess);
				editTxt_mess.Text = "";
				viewModel.senMessage ();
				MessageAdapter adt = new MessageAdapter (this, array_mess);
				lv_chat.Adapter = adt;
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
			btn_friendlist.Visibility = ViewStates.Gone;
			View homeicon=FindViewById<View>(Android.Resource.Id.Home);
			((View) homeicon.Parent).Visibility=ViewStates.Gone;
			ActionBar.SetCustomView (customview, actionbarParams);
		}
	}
}

