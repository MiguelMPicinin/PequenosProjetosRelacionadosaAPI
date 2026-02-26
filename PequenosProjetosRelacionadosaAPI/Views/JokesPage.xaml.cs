using PequenosProjetosRelacionadosaAPI.Services;
using Microsoft.Maui.Controls;
using System;

namespace PequenosProjetosRelacionadosaAPI.Views
{
    public partial class JokesPage : ContentPage
    {
        private readonly IJokeService _jokeService;

        public JokesPage(IJokeService jokeService)
        {
            InitializeComponent();
            _jokeService = jokeService;
        }

        private async void OnRandomJokeClicked(object sender, EventArgs e)
        {
            await ExecuteWithLoading(async () =>
            {
                var joke = await _jokeService.GetRandomJokeAsync();
                ResultLayout.Children.Clear();
                ResultLayout.Children.Add(new Label { Text = joke });
            });
        }

        private async void OnAllJokesClicked(object sender, EventArgs e)
        {
            await ExecuteWithLoading(async () =>
            {
                var jokes = await _jokeService.GetAllJokesAsync(); // Agora existe
                ResultLayout.Children.Clear();
                foreach (var joke in jokes)
                {
                    ResultLayout.Children.Add(new Label { Text = $"• {joke}" });
                }
            });
        }

        private async void OnCountJokesClicked(object sender, EventArgs e)
        {
            await ExecuteWithLoading(async () =>
            {
                int count = await _jokeService.GetJokeCountAsync();
                ResultLayout.Children.Clear();
                ResultLayout.Children.Add(new Label { Text = $"Total de piadas: {count}" });
            });
        }

        private async Task ExecuteWithLoading(Func<Task> action)
        {
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;
            ResultLayout.Children.Clear();

            try
            {
                await action();
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