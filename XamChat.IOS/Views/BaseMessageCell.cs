using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat;

namespace XamChat.IOS

{
	public class BaseMessageCell : UITableViewCell
	{
		public BaseMessageCell (IntPtr handle) : base(handle)
		{
			
		}
		public virtual void Update(MessageM message)
		{
			
		}
	}
}

