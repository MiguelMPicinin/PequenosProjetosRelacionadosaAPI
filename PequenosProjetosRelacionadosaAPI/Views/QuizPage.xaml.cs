using PequenosProjetosRelacionadosaAPI.Models;
using PequenosProjetosRelacionadosaAPI.Services;

namespace PequenosProjetosRelacionadosaAPI.Views;

public partial class QuizPage : ContentPage
{
    private readonly IQuizService _quizService;
    private List<QuizQuestion> _questions;
    private List<Picker> _answerPickers = new();

    public QuizPage(IQuizService quizService)
    {
        InitializeComponent();
        _quizService = quizService;
    }

    private async void OnLoadQuestionsClicked(object sender, EventArgs e)
    {
        LoadingIndicator.IsRunning = true;
        LoadingIndicator.IsVisible = true;
        QuestionsLayout.Children.Clear();
        _answerPickers.Clear();
        SubmitButton.IsVisible = false;
        ResultLabel.Text = "";

        try
        {
            _questions = await _quizService.GetQuestionsAsync(5);
            if (_questions == null || _questions.Count == 0)
            {
                await DisplayAlert("Erro", "Nenhuma pergunta disponível.", "OK");
                return;
            }

            foreach (var q in _questions)
            {
                var questionLabel = new Label { Text = q.QuestionText, FontAttributes = FontAttributes.Bold };
                var picker = new Picker { Title = "Escolha uma opção" };
                foreach (var opt in q.Options)
                    picker.Items.Add(opt);
                _answerPickers.Add(picker);
                QuestionsLayout.Children.Add(questionLabel);
                QuestionsLayout.Children.Add(picker);
            }
            SubmitButton.IsVisible = true;
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

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var answers = new List<QuizAnswer>();
        for (int i = 0; i < _questions.Count; i++)
        {
            if (_answerPickers[i].SelectedIndex == -1)
            {
                await DisplayAlert("Aviso", $"Responda a pergunta {i + 1}.", "OK");
                return;
            }
            answers.Add(new QuizAnswer
            {
                QuestionId = _questions[i].Id,
                SelectedOptionIndex = _answerPickers[i].SelectedIndex
            });
        }

        LoadingIndicator.IsRunning = true;
        LoadingIndicator.IsVisible = true;

        try
        {
            var result = await _quizService.CheckAnswersAsync(answers);
            ResultLabel.Text = $"Você acertou {result.Score} de {result.Total} perguntas!";
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