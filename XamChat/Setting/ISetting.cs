using System;

namespace XamChat
{
	public interface ISetting
	{
		User User{get;set;}

		void Save();
	}
}

