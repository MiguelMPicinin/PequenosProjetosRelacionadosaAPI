using PequenosProjetosRelacionadosaAPI.Views;

namespace PequenosProjetosRelacionadosaAPI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("pokemon", typeof(PokemonPage));
        Routing.RegisterRoute("rawg", typeof(RAWGPage));
        Routing.RegisterRoute("quiz", typeof(QuizPage));
        Routing.RegisterRoute("jokes", typeof(JokesPage));
        Routing.RegisterRoute("currency", typeof(CurrencyPage));
    }
}