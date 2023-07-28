using CommunityToolkit.Mvvm.Messaging;
using ProjCF.Mobile.Data.Models;
using ProjCF.Mobile.Data.Models.Enum;
using ProjCF.Mobile.Domain.Extensions;
using ProjCF.Mobile.Domain.Interfaces.Repositories;
using System.Globalization;

namespace ProjCF.Mobile.Views;

public partial class TransacaoListView : ContentPage
{
    private readonly ITransacaoAppRepository _transacaoRepositoy;
    private Color _borderDefaultBackgroundColor;
    private string _labelDefaultText;
    private int Year;
    private int Month;
    public TransacaoListView(ITransacaoAppRepository transacaoRepositoy)
    {
        InitializeComponent();
        _transacaoRepositoy = transacaoRepositoy; 
        NavigationPage.SetHasNavigationBar(this, false);
        this.Year = DateTime.UtcNow.Year;
        this.Month = DateTime.UtcNow.Month;
        Reload();
        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
        {
            Reload();
        });
    }
    private void Reload()
    {
        GetYearAndMounth();
        var items = _transacaoRepositoy.ObterTransacaoPorMesEAno(this.Month, this.Year);
        CollectionViewTransactions.ItemsSource = items;
        BorderTransactions.IsVisible = items.Count() != 0 ? true : false;
        BorderTransactionsEmpty.IsVisible = items.Count() == 0 ? true : false;
        double income = items.Where(a => a.TipoTransacao == TipoTransacao.Receita).Sum(a => a.Valor);
        double expense = items.Where(a => a.TipoTransacao == TipoTransacao.Despesa).Sum(a => a.Valor);
        double balance = income - expense;
        double paid = items.Where(a => a.Pago == true).Sum(a => a.Valor);
        double pending = items.Where(a => a.Pago == false).Sum(a => a.Valor);

        LabelIncome.Text = income.ToString("C");
        LabelExpense.Text = expense.ToString("C");
        LabelBalance.Text = balance.ToString("C");
        LabelPaid.Text = paid.ToString("C");
        LabelPending.Text = pending.ToString("C");
    }

    private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs args)
    {

        var transactionAdd = Handler.MauiContext.Services.GetService<TransacaoAddView>();
        Navigation.PushModalAsync(transactionAdd);
    }

    private void TapGestureRecognizer_Tapped_To_TransactionEdit(object sender, TappedEventArgs e)
    {
        var grid = (Grid)sender;
        var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];

        TransacaoModel transaction = (TransacaoModel)gesture.CommandParameter;

        var transactionEdit = Handler.MauiContext.Services.GetService<TransacaoEditView>();
        transactionEdit.SetTransactionToEdit(transaction);
        Navigation.PushModalAsync(transactionEdit);
    }

    private async void TapGestureRecognizer_Tapped_ToDelete(object sender, TappedEventArgs e)
    {
        await AnimationBorder((Border)sender, true);
        bool result = await App.Current.MainPage.DisplayAlert("Excluir!", "Tem certeza que deseja excluir?", "Sim", "Não");

        if (result)
        {
            TransacaoModel transaction = (TransacaoModel)e.Parameter;
            _transacaoRepositoy.DeletarTransacao(transaction);
            Reload();
        }
        else
        {
            await AnimationBorder((Border)sender, false);
        }
    }
    private async Task AnimationBorder(Border border, bool IsDeleteAnimation)
    {
        var label = (Label)border.Content;
        if (IsDeleteAnimation)
        {
            _borderDefaultBackgroundColor = border.BackgroundColor;
            _labelDefaultText = label.Text;
            await border.RotateYTo(90, 250);
            border.BackgroundColor = Colors.Red;
            label.TextColor = Colors.White;
            label.Text = "X";
            await border.RotateYTo(180, 250);
        }
        else
        {
            await border.RotateYTo(90, 250);
            border.BackgroundColor = _borderDefaultBackgroundColor;
            label.TextColor = Colors.Black;
            label.Text = _labelDefaultText;
            await border.RotateYTo(0, 250);
        }
    }

    private void DecreaseMonthClicked(object sender, EventArgs e)
    {
        this.Month--;
        if (this.Month < 1)
        {
            this.Month = 12;
            this.Year--;
        }

        Reload();
    }

    private void IncreaseMonthClicked(object sender, EventArgs e)
    {
        this.Month++;
        if (this.Month > 12)
        {
            this.Month = 1;
            this.Year++;
        }

        Reload();
    }

    private void GetYearAndMounth()
    {
        string monthName = new DateTime(this.Year, this.Month, 1).ToString("MMMM", CultureInfo.GetCultureInfo("pt-BR"));
        LabelMonthYear.Text = $"{monthName.ToCapitalize()} {this.Year}";

    }
}