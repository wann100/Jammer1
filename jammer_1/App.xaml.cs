using Jammer_1.views;
using Jammer_1.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Jammer_1
{
	public partial class App : Application
	{
        public static IAuthenticate Authenticator { get; private set; }
        public App()
		{
		  InitializeComponent();
          Current.MainPage = new NavigationPage(new LoginPage());
		    //SetMainPage();
		}
        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }
        public static void SetMainPage()
		{
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new testpage())
                    {
                        Title = "Users",
                        Icon = Device.OnPlatform<string>("tab_feed.png",null,null)
                    },
                    new NavigationPage(new LoginPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform<string>("tab_about.png",null,null)
                    },
                }
            };
        }
	}
}
