using lab2;

namespace ui;

public partial class GamePage : ContentPage
{
    public GamePage()
    {
        InitializeComponent();

        GameState.Game = new GameWindow(GameState.Player, GameState.Shop);
        string avatarSource = GameState.Game.player.Gender ? "avatar_male.png" : "avatar_female.png";

        AvatarImage.Source = avatarSource;
        NickLabel.Text = GameState.Game.player.Name;
        //GameState.Player.Gold = 30;
        UpdateStats();
        LoadCoinGif();
    }
    private void UpdateStats()
    {
        var player = GameState.Game.player;
        LevelLabel.Text = $"Уровень: {player.Level}";
        ExperienceLabel.Text = $"Опыт: {player.Experience}/{player.Level*5}";
        GoldLabel.Text = $"Золото: {player.Gold}";
        double hpPercentage = (double)player.FullHP / player.FullMaxHP;
        HPBar1.Progress = hpPercentage;
        HPBar2.Progress = hpPercentage;
        HPBar3.Progress = hpPercentage;
        HPBar4.Progress = hpPercentage;
        HPBar5.Progress = hpPercentage;
        HPLabel.Text = $"{player.FullHP}/{player.FullMaxHP}";
        DMGLabel.Text = $"Урон: {player.FullDMG}";
        ArmorLabel.Text = $"Броня: {player.FullArmor}";
        StealthLabel.Text = $"Скрытность: {player.FullStealth}";
        StrengthLabel.Text = $"Сила: {player.Strength}";
        AgilityLabel.Text = $"Ловкость: {player.Agility}";
        CharismaLabel.Text = $"Харизма: {player.Charisma}";

        UpdateEquipmentImages();

        UpdateUI();
    }
    private void UpdateEquipmentImages()
    {
        var equipment = GameState.Game.player.Equipment;
        HeadImage.Source = equipment.Head?.Path;
        TorsoImage.Source = equipment.Torso?.Path;
        LegsImage.Source = equipment.Legs?.Path;
        BootsImage.Source = equipment.Boots?.Path;
        FirstWeaponImage.Source = equipment.FirstWeapon?.Path;
        SecondWeaponImage.Source = equipment.SecondWeapon?.Path;
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
    private void OnRestButtonClicked(object sender, EventArgs e)
    {
        if (GameState.Game.SpendGold(5 * GameState.Game.player.Level)){
            int healthRestored = GameState.Game.Rest();
            DisplayAlert("Отдых", $"Восстановлено здоровья: {healthRestored}.\nЗолото потраченное на ночлег: {5 * GameState.Game.player.Level}.", "OK");
            UpdateStats();
        } else
        {
            DisplayAlert("Нет денег!", $"Золото необходимое на ночлег: {5 * GameState.Game.player.Level}.", "OK");
        }
    }
    private async void OnShopButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ShopPage");
    }
    private async void OnInventoryButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("InventoryPage");
    }
    private void UpdateUI()
    {
        RestButton.IsEnabled = GameState.Player.FullHP < GameState.Player.FullMaxHP;
    }
}