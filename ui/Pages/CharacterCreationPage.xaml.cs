using lab2;
using lab3;
using Microsoft.Maui.Controls;

namespace ui;

public partial class CharacterCreationPage : ContentPage
{
    public CharacterCreationPage()
    {
        InitializeComponent();
        GenderPicker.SelectedIndexChanged += GenderPicker_SelectedIndexChanged;
    }

    private void GenderPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (GenderPicker.SelectedItem as string)
        {
            case "Мужской":
                GenderImage.Source = "avatar_male.png";
                break;
            case "Женский":
                GenderImage.Source = "avatar_female.png";
                break;
            default:
                GenderImage.Source = "avatar_default.png";
                break;
        }
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim();
        if (string.IsNullOrEmpty(name) || GenderPicker.SelectedIndex < 0)
        {
            await DisplayAlert("Ошибка", "Укажите имя и пол персонажа.", "OK");
            return;
        }

        bool gender = GenderPicker.SelectedItem as string == "Мужской";

        GameState.Player = new Player(name, gender);
        GameState.Shop = new Shop();
        GameState.StoryProgress = new StoryProgress();

        string xmlPath = Path.Combine(AppContext.BaseDirectory, "BasicShopItems.xml");
        GameState.Shop.AddShopItems(ShopItemLoader.LoadItemsFromXml(xmlPath));

        await Shell.Current.GoToAsync(nameof(StoryPage));
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }
}
