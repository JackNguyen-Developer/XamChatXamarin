using NUnit.Framework;
using System;

namespace XamChat.Tests
{
	public static class Test
	{				
		public static void Setup ()
		{
			ServiceContainer.Register<ISetting>(()=>new FakeSetting());
			ServiceContainer.Register<IWebServices>(()=>new FakeService(){SleepDurtion = 0});
		}
	}
}

