using CommunityToolkit.Mvvm.Messaging;
using ProjCF.Mobile.Data.Models;
using ProjCF.Mobile.Data.Models.Enum;
using ProjCF.Mobile.Domain.Interfaces.Repositories;
using System.Text;

namespace ProjCF.Mobile.Views;

public partial class TransacaoEditView : ContentPage
{
    private TransacaoModel _transacaoModel;
    private ITransacaoAppRepository _transacaoRepository;
    public TransacaoEditView(ITransacaoAppRepository transacaoRepository)
    {
        InitializeComponent();
        _transacaoRepository = transacaoRepository;
    }

    public void SetTransactionToEdit(TransacaoModel transaction)
    {
        _transacaoModel = transaction;
        if (transaction.TipoTransacao == TipoTransacao.Receita)
        {
            RadioIncome.IsChecked = true;
        }
        else
        {
            RadioExpense.IsChecked = true;
        }
        if (transaction.Pago)
        {
            CheckBoxPaid.IsChecked = true;
        }
        else
        {
            CheckBoxPaid.IsChecked = false;
        }
        EntryName.Text = transaction.Descricao;
        DatePickerDate.Date = transaction.Data.Date;
        EntryValue.Text = transaction.Valor.ToString();
    }
    private void TapGestureRecognizer_Tapped_ToClose(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void OnButtonClicked_Save(object sender, EventArgs e)
    {
        if (IsValidData() == false)
        {
            return;
        }
        SaveTransactionInDatabase();
        Navigation.PopModalAsync();
        WeakReferenceMessenger.Default.Send<string>(string.Empty);
    }

    private async void OnButtonClicked_Delete(object sender, EventArgs e)
    {
        bool result = await App.Current.MainPage.DisplayAlert("Excluir!", "Tem certeza que deseja excluir?", "Sim", "Não");

        if (result)
        {
            Button button = (Button)sender;
            TransacaoModel transaction = (TransacaoModel)button.CommandParameter;
            await Navigation.PopModalAsync();
            _transacaoRepository.DeletarTransacao(transaction);
            WeakReferenceMessenger.Default.Send<string>(string.Empty);
        }
        else
        {
        }
    }

    private void SaveTransactionInDatabase()
    {
        TransacaoModel transaction = new TransacaoModel()
        {
            Id = _transacaoModel.Id,
            TipoTransacao = RadioIncome.IsChecked ? TipoTransacao.Receita : TipoTransacao.Despesa,
            Descricao = EntryName.Text,
            Data = DatePickerDate.Date,
            Valor = Math.Abs(double.Parse(EntryValue.Text)),
            Pago = CheckBoxPaid.IsChecked ? true : false
        };
        _transacaoRepository.AtualizarTransacao(transaction);
    }

    private bool IsValidData()
    {
        bool valid = true;
        StringBuilder stringBuilder = new StringBuilder();
        double result;
        if (string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
        {
            stringBuilder.AppendLine("O campo 'Nome' deve ser preenchido!");
            valid = false;
        }
        if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
        {
            stringBuilder.AppendLine("O campo 'Valor' deve ser preenchido!");
            valid = false;
        }
        if (!string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out result))
        {
            stringBuilder.AppendLine("O campo 'Valor' é inválido!");
            valid = false;
        }
        if (valid == false)
        {
            LabelError.IsVisible = true;
            LabelError.Text = stringBuilder.ToString();
        }
        return valid;
    }
}