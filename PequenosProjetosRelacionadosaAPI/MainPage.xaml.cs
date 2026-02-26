namespace PequenosProjetosRelacionadosaAPI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnPokemonClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("pokemon");

        private async void OnRAWGClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("rawg");

        private async void OnQuizClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("quiz");

        private async void OnJokesClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("jokes");

        private async void OnCurrencyClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("currency");
    }
}