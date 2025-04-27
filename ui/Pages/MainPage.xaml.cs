using Microsoft.Maui.Controls;
using Microsoft.Maui;
using lab2;

namespace ui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnNewGameClicked(object sender, EventArgs e)
        {
            // Логика для кнопки "Начать новую игру"
            await Shell.Current.GoToAsync("//CharacterCreationPage");

        }

        private async void OnLoadGameClicked(object sender, EventArgs e)
        {
            // Логика для кнопки "Загрузить существующую игру"
            await Shell.Current.GoToAsync("//LoadGamePage");
        }
        private void OnExitClicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }
    }

}
