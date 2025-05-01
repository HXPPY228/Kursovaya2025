using lab2;

namespace ui;

public partial class GamePage : ContentPage
{
    public GamePage()
    {
        InitializeComponent();

        GameState.Game = new GameWindow(GameState.Player);
        string avatarSource = GameState.Player.Gender ? "avatar_male.png" : "avatar_female.png";

        AvatarImage.Source = avatarSource;
        NickLabel.Text = GameState.Player.Name;

        GameState.Player.HP -= 5;

        UpdateStats();
        LoadCoinGif();
    }
    private void UpdateStats()
    {
        var player = GameState.Player;
        LevelLabel.Text = $"�������: {player.Level}";
        ExperienceLabel.Text = $"����: {player.Experience}/{player.Level*5}";
        GoldLabel.Text = $"������: {player.Gold}";
        double hpPercentage = (double)player.FullHP / player.FullMaxHP;
        HPBar1.Progress = hpPercentage;
        HPBar2.Progress = hpPercentage;
        HPBar3.Progress = hpPercentage;
        HPBar4.Progress = hpPercentage;
        HPBar5.Progress = hpPercentage;
        HPLabel.Text = $"{player.FullHP}/{player.FullMaxHP}";
        DMGLabel.Text = $"����: {player.FullDMG}";
        ArmorLabel.Text = $"�����: {player.FullArmor}";
        StealthLabel.Text = $"����������: {player.FullStealth}";
        StrengthLabel.Text = $"����: {player.Strength}";
        AgilityLabel.Text = $"��������: {player.Agility}";
        CharismaLabel.Text = $"�������: {player.Charisma}";
        UpdateUI();
    }
    private void LoadCoinGif()
    {
        // HTML content to display the GIF
        string htmlContent = $@"
            <html>
                <body style='margin:0; padding:0; background-color:#1C2526; display:flex; justify-content:center; align-items:center;'>
                    <img src='coin.gif' style='width:25px; height:25px; background-color:#1f1f1f;' />
                </body>
            </html>";

        // Use HtmlWebViewSource to load the HTML content
        var htmlSource = new HtmlWebViewSource
        {
            Html = htmlContent,
            BaseUrl = null
        };

        CoinAnimation.Source = htmlSource;
    }
    private void OnRestButtonClicked(object sender, EventArgs e)
    {
        int healthRestored = GameState.Game.Rest();
        // Optionally display a message
        DisplayAlert("�����", $"������������� ��������: {healthRestored}", "OK");
        UpdateStats();
    }
    private void UpdateUI()
    {
        RestButton.IsEnabled = GameState.Player.FullHP < GameState.Player.FullMaxHP;
    }
}