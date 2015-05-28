using System;

namespace XamChat
{
	public class BaseViewModel
	{		
		public readonly IWebServices service=ServiceContainer.Resolve<IWebServices>();
		public readonly ISetting setting=ServiceContainer.Resolve<ISetting>();
		public event EventHandler IsBusyChanged=delegate {};
		bool isBusy=false;
		public bool IsBusy 
		{ 
			get 
			{
				return isBusy;
			}
			set
			{
				isBusy = value;
				IsBusyChanged (this, EventArgs.Empty);
			}
		}
	}
}

