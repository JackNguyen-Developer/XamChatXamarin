// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace XamChat.IOS
{
	public partial class ConversationsController : UITableViewController
	{
		readonly ConversationViewModel messageViewModel = ServiceContainer.Resolve<ConversationViewModel>();

		public ConversationsController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			TableView.Source = new TableSource(this);
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			try
			{
				await messageViewModel.getConversations();

				TableView.ReloadData();
			}
			catch (Exception exc)
			{
				//exc.DisplayError();
			}
		}

		class TableSource : UITableViewSource
		{
			const string CellName = "ConversationCell";
			readonly ConversationViewModel messageViewModel = ServiceContainer.Resolve<ConversationViewModel>();
			readonly ConversationsController controller;

			public TableSource(ConversationsController controller)
			{
				this.controller = controller;
			}

			public override int RowsInSection(UITableView tableview, int section)
			{
				return messageViewModel.conversations == null ? 0 : messageViewModel.conversations.Length; 
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var conversation = messageViewModel.conversations [indexPath.Row];
				var cell = tableView.DequeueReusableCell(CellName);
				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellName);
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				}
				cell.TextLabel.Text = conversation.UserOwnerFriend.Username;
				//cell.DetailTextLabel.Text = conversation.LastMessage;
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var conversation = messageViewModel.conversations [indexPath.Row];
				messageViewModel.conversation = conversation;
				controller.PerformSegue("OnConversation", controller);
			}
		}
	}
}