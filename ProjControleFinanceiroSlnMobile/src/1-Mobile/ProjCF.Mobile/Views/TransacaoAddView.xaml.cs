using CommunityToolkit.Mvvm.Messaging;
using ProjCF.Mobile.Data.Models;
using ProjCF.Mobile.Data.Models.Enum;
using ProjCF.Mobile.Domain.Interfaces.Repositories;
using System.Text;

namespace ProjCF.Mobile.Views;

public partial class TransacaoAddView : ContentPage
{
    private ITransacaoAppRepository _transacaoRepository;
    private int qtdRepete;
    public TransacaoAddView(ITransacaoAppRepository transacaoRepository)
    {
        InitializeComponent();
        _transacaoRepository = transacaoRepository;
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

    private void SaveTransactionInDatabase()
    {
        TransacaoModel transaction = new TransacaoModel()
        {
            TipoTransacao = RadioIncome.IsChecked ? TipoTransacao.Receita : TipoTransacao.Despesa,
            Descricao = EntryName.Text,
            Data = DatePickerDate.Date.Add(DateTime.Now.TimeOfDay),
            Valor = Math.Abs(double.Parse(EntryValue.Text)),
            Pago = CheckBoxPaid.IsChecked ? true : false,
            Repete = CheckBoxRepete.IsChecked ? true : false,
            QtdRepete = this.qtdRepete
        };
        _transacaoRepository.AdicionarTransacao(transaction);
    }

    private bool IsValidData()
    {
        bool valid = true;
        StringBuilder stringBuilder = new StringBuilder();
        double result;
        int repeteResult;
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
        if (CheckBoxRepete.IsChecked)
        {
            if (string.IsNullOrEmpty(EntryQtdRepete.Text) || string.IsNullOrWhiteSpace(EntryQtdRepete.Text))
            {
                stringBuilder.AppendLine("O campo 'Repetir Mensalmente' deve ser preenchido!");
                valid = false;
            }
            if (!string.IsNullOrEmpty(EntryQtdRepete.Text) && !int.TryParse(EntryQtdRepete.Text, out repeteResult))
            {
                stringBuilder.AppendLine("O campo 'Repetir Mensalmente' é inválido!");
                valid = false;
            }
            else if (int.TryParse(EntryQtdRepete.Text, out repeteResult) && repeteResult <= 0)
            {
                stringBuilder.AppendLine("O campo 'Repetir Mensalmente' deve ser maior que 0!");
                valid = false;
            }
            else if (int.TryParse(EntryQtdRepete.Text, out repeteResult))
            {
                this.qtdRepete = repeteResult;
            }
        }
        else
        {
            this.qtdRepete = 0;
        }
        if (valid == false)
        {
            LabelError.IsVisible = true;
            LabelError.Text = stringBuilder.ToString();
        }
        return valid;
    }
}