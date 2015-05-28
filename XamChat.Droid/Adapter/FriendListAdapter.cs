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
	public class FriendListAdapter:BaseAdapter<User>
	{
		List<User> friends=new List<User>();
		Activity context;
		public FriendListAdapter (Activity c,User[] a)
		{	
			context = c;
			if (a != null)
				friends.AddRange (a);
			else
				friends = new List<User> ();
		}
		public override User this[int position]
		{
			get { return friends[position]; }
		}
		public override long GetItemId (int position)
		{
			return position;
		}
		public override int Count {
			get {
				return friends.Count;
			}
		}
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View view=convertView;
			var item = friends [position];
			if (view == null)
				view = context.LayoutInflater.Inflate(Resource.Layout.Baseitem, null);
			TextView title = view.FindViewById<TextView> (Resource.Id.list_item);
			title.Text = item.Username;
			return view;
		}
	}
}

