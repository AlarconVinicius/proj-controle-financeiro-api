using ProjCF.Mobile.Views;

namespace ProjCF.Mobile;

public partial class App : Application
{
    public App(TransacaoListView listPage)
    {
        InitializeComponent();
        App.Current!.UserAppTheme = AppTheme.Light;

        MainPage = new NavigationPage(listPage);
    }
}
