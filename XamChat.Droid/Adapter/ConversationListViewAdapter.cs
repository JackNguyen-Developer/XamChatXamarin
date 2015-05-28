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
	public class ConversationListViewAdapter:BaseAdapter<Conversation>
	{
		List<Conversation> conversations=new List<Conversation>();
		Activity context;
		public ConversationListViewAdapter (Activity c,Conversation[] a)
		{	
			context = c;
			conversations .AddRange(a);
		}
		public override Conversation this[int position]
		{
			get { return conversations[position]; }
		}
		public override long GetItemId (int position)
		{
			return position;
		}
		public override int Count {
			get {
				return conversations.Count;
			}
		}
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View view=convertView;
			var item = conversations [position];
			if (view == null)
				view = context.LayoutInflater.Inflate(Resource.Layout.Baseitem, null);
			TextView title = view.FindViewById<TextView> (Resource.Id.list_item);
			ImageView avatar = view.FindViewById<ImageView> (Resource.Id.imgv_avatar);		
			title.Text = item.UserOwnerFriend.Username;
			return view;
		}
	}
}

