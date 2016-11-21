using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Redmond.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			global::Xamarin.Forms.Forms.Init();
			Xamarin.FormsMaps.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
