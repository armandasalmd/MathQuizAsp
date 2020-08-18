using MathQuizCore.Enums;
using MathQuizXamarin.ViewModels;
using MathQuizXamarin.Views;
using Xamarin.Forms;

namespace MathQuizXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(this); // BindingContext = ViewModel

        }

        public async void StartGamePage(DifficultyLevel level, int questionCount)
        {
            await Navigation.PushAsync(new GamePage(level, questionCount));
        }

    }
}
