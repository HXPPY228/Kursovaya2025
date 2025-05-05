using lab2;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ui;

public partial class ShopPage : ContentPage
{
    public ObservableCollection<Item> ShopItems { get; set; }
    public ICommand BuyCommand { get; }
    public ShopPage()
	{
        InitializeComponent();
        LoadShopItems();
        LoadCoinGif();
        BuyCommand = new Command<Item>(OnBuyItem);
        BindingContext = this;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateStats();
    }
    private void LoadShopItems()
    {
        ShopItems = new ObservableCollection<Item>(GameState.Game.GetShopItems());
    }

    private void OnBackButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(GamePage));
    }
    private void LoadCoinGif()
    {
        string htmlContent = $@"
                <html>
                    <body style='margin:0; padding:0; background-color:#1C2526; display:flex; justify-content:center; align-items:center;'>
                        <img src='coin.gif' style='width:25px; height:25px; background-color:#1f1f1f;' />
                    </body>
                </html>";

        var htmlSource = new HtmlWebViewSource
        {
            Html = htmlContent,
            BaseUrl = null
        };

        CoinAnimation.Source = htmlSource;
    }
    private void UpdateStats()
    {
        GoldLabel.Text = $"Золото: {GameState.Game.player.Gold}";
    }
    private async void OnBuyItem(Item item)
    {
        int charisma = GameState.Game.player.Charisma;
        int discountedPrice = item.Price - (charisma / 2);

        StringBuilder buffs = new StringBuilder();
        foreach (var stat in item.Stats)
        {
            buffs.Append("\n");
            buffs.Append($"{stat.Key} +{stat.Value}");
        }
        string buffsString = $"Бафы: {buffs}";

        string message = $"Вы хотите купить {item.Name} за {discountedPrice} золота? (Скидка {charisma / 2} золота за Харизму)\n{buffsString}";
        bool answer = await DisplayAlert("Покупка", message, "Да", "Нет");

        if (answer)
        {
            bool success = GameState.Game.player.BuyItem(item, GameState.Game.shop);
            if (success)
            {
                ShopItems.Remove(item);
                UpdateStats();
                await DisplayAlert("Успех", "Предмет куплен и находится в инвентаре!", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", "Недостаточно золота для покупки!", "OK");
            }
        }
    }
}