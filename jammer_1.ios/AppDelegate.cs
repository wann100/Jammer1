
using Foundation;
using Jammer_1.Helpers;
using Jammer_1.Services;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using UIKit;

namespace Jammer_1.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IAuthenticate
    {
        MobileServiceUser user;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            // IMPORTANT: uncomment this code to enable sync on Xamarin.iOS
            // For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
            //SQLitePCL.CurrentPlatform.Init();

            App.Init(this);
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public async Task<bool> AuthenticateAsync()
        {
            bool success = false;
            try
            {
                if (user == null)
                {
                    // The authentication provider could also be Facebook, Twitter, or Microsoft
#pragma warning disable CS1701 // Assuming assembly reference matches identity
                    user = await AzureService.DefaultManager.CurrentClient.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, MobileServiceAuthenticationProvider.Google);
#pragma warning restore CS1701 // Assuming assembly reference matches identity
                    if (user != null)
                    {
                        var authAlert = new UIAlertView("Authentication", "You are now logged in " + user.UserId, null, "OK", null);
                        Settings.currentuser = user;
                        authAlert.Show();
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                var authAlert = new UIAlertView("Authentication failed", ex.Message, null, "OK", null);
                authAlert.Show();
            }
            return success;
        }

        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                if (user != null)
                {
                    foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
                    {
                        NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
                    }

                    await AzureService.DefaultManager.CurrentClient.LogoutAsync();
                    var logoutAlert = new UIAlertView("Authentication", "You are now logged out " + user.UserId, null, "OK", null);
                    logoutAlert.Show();
                }
                user = null;
                success = true;
            }
            catch (Exception ex)
            {
                var logoutAlert = new UIAlertView("Logout failed", ex.Message, null, "OK", null);
                logoutAlert.Show();
            }
            return success;
        }
    }
}
