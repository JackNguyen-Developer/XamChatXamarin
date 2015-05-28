using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
namespace XamChat.Droid
{
	public class MessageAdapter:BaseAdapter<MessageM>
	{		
		User currentUser=Session.UserSession;
		Activity context;
		List<MessageM> message_list=new List<MessageM>();

		public MessageAdapter(Activity c,List<MessageM> a)
		{
			context = c;
			if (a != null)
				message_list = a;
		}
		public override MessageM this[int position]
		{
			get { return message_list[position]; }
		}
		public override long GetItemId (int position)
		{
			return position;
		}
		public override int Count {
			get {
				return message_list.Count;
			}
		}
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View view = convertView;
			var item = message_list [position];
			if(item.UserId==currentUser.Id)
				view = context.LayoutInflater.Inflate (Resource.Layout.ChatMessageItemRight, null);
			else
				view = context.LayoutInflater.Inflate (Resource.Layout.ChatMessageItemLeft, null);		
			TextView title = view.FindViewById<TextView> (Resource.Id.list_item);
			ImageView avatar = view.FindViewById<ImageView> (Resource.Id.imgv_avatar);
			title.Text = item.Text;
			return view;
		}
	}
}

