using ProjControleFinanceiro.Mobile.Views;

namespace ProjControleFinanceiro.Mobile;

public partial class App : Application
{
    public App(TransacaoListView listPage)
    {
        InitializeComponent();
        App.Current!.UserAppTheme = AppTheme.Light;

        MainPage = new NavigationPage(listPage);
    }
}
