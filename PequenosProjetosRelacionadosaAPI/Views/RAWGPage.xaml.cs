using PequenosProjetosRelacionadosaAPI.Services;
using Microsoft.Maui.Controls;
using System;

namespace PequenosProjetosRelacionadosaAPI.Views
{
    public partial class RAWGPage : ContentPage
    {
        private readonly IRAWGService _rawgService;

        public RAWGPage(IRAWGService rawgService)
        {
            InitializeComponent();
            _rawgService = rawgService;
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            string query = GameEntry.Text?.Trim();
            if (string.IsNullOrEmpty(query))
            {
                await DisplayAlert("Erro", "Digite um nome.", "OK");
                return;
            }

            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;
            ResultLayout.Children.Clear();

            try
            {
                var games = await _rawgService.SearchGamesAsync(query);
                if (games.Count == 0)
                {
                    ResultLayout.Children.Add(new Label { Text = "Nenhum jogo encontrado." });
                }
                else
                {
                    foreach (var game in games)
                    {
                        var frame = new Frame { BorderColor = Colors.Gray, CornerRadius = 5, Padding = 10 };
                        var stack = new VerticalStackLayout();
                        stack.Children.Add(new Label { Text = game.Name, FontAttributes = FontAttributes.Bold });
                        stack.Children.Add(new Label { Text = $"Lançamento: {game.Released}" }); // Agora funciona
                        stack.Children.Add(new Label { Text = $"Metacritic: {game.Metacritic}" });
                        stack.Children.Add(new Label { Text = $"Plataformas: {string.Join(", ", game.Platforms)}" });
                        frame.Content = stack;
                        ResultLayout.Children.Add(frame);
                    }
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
}