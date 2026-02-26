using PequenosProjetosRelacionadosaAPI.Models;
using PequenosProjetosRelacionadosaAPI.Services;

namespace PequenosProjetosRelacionadosaAPI.Views;

public partial class CurrencyPage : ContentPage
{
    private readonly ICurrencyService _currencyService;

    public CurrencyPage(ICurrencyService currencyService)
    {
        InitializeComponent();
        _currencyService = currencyService;
    }

    private async void OnConvertClicked(object sender, EventArgs e)
    {
        string from = FromEntry.Text?.Trim().ToUpper();
        string to = ToEntry.Text?.Trim().ToUpper();
        if (!decimal.TryParse(AmountEntry.Text, out decimal amount))
        {
            await DisplayAlert("Erro", "Valor inválido.", "OK");
            return;
        }

        LoadingIndicator.IsRunning = true;
        LoadingIndicator.IsVisible = true;
        ResultLabel.Text = "";

        try
        {
            var request = new ConversionRequest { From = from, To = to, Amount = amount };
            var result = await _currencyService.ConvertAsync(request);
            ResultLabel.Text = $"{amount} {from} = {result.Converted:F2} {to}\nTaxa: 1 {from} = {result.Rate:F2} {to}";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }
}