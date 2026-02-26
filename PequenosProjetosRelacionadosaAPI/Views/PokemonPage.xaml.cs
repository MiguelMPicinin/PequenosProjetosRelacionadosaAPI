using PequenosProjetosRelacionadosaAPI.Services;

namespace PequenosProjetosRelacionadosaAPI.Views;

public partial class PokemonPage : ContentPage
{
    private readonly IPokemonServices _pokemonService;

    public PokemonPage(IPokemonServices pokemonService)
    {
        InitializeComponent();
        _pokemonService = pokemonService;
    }

    private async void OnSearchClicked(object sender, EventArgs e)
    {
        string name = PokemonEntry.Text?.Trim();
        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlert("Erro", "Digite um nome.", "OK");
            return;
        }

        LoadingIndicator.IsRunning = true;
        LoadingIndicator.IsVisible = true;
        ResultLayout.Children.Clear();

        try
        {
            var pokemon = await _pokemonService.GetPokemonAsync(name);

            ResultLayout.Children.Add(new Label { Text = $"Nome: {pokemon.Name}", FontAttributes = FontAttributes.Bold });
            ResultLayout.Children.Add(new Label { Text = $"Tipos: {string.Join(", ", pokemon.Types)}" });
            ResultLayout.Children.Add(new Label { Text = $"Habilidades: {string.Join(", ", pokemon.Abilities)}" });
            ResultLayout.Children.Add(new Label { Text = "Estatísticas:", FontAttributes = FontAttributes.Bold });
            foreach (var stat in pokemon.Stats)
            {
                ResultLayout.Children.Add(new Label { Text = $"  {stat.Key}: {stat.Value}" });
            }
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