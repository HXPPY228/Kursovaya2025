using lab2;
using lab3;
using System.Windows.Input;

namespace ui;

public partial class FightPage : ContentPage
{
    public Enemy Enemy { get; private set; }
    public Player Player => GameState.Game.player;

    public string EnemyHPText => $"HP: {Enemy.HP}";
    public string PlayerHPText => $"HP: {Player.FullHP}";
    public string EnemyStatsText => $"DMG: {Enemy.DMG}, Armor: {Enemy.Armor}, StealthReq: {Enemy.LVL_to_sk}";
    public string PlayerStatsText => $"DMG: {Player.FullDMG}, Armor: {Player.FullArmor}, Stealth: {Player.FullStealth}";

    public string StealthSequence { get; set; }

    public ICommand AttackCommand { get; }
    public ICommand StealthCommand { get; }

    public FightPage()
    {
        InitializeComponent();

        Enemy = GameState.CurrentEnemy;
        StealthSequence = string.Empty;

        AttackCommand = new Command(OnAttack);
        StealthCommand = new Command(OnStealth);

        BindingContext = this;
    }

    private async void OnAttack()
    {
        b1.IsEnabled = false;
        b2.IsEnabled = false;
        // простой обмен ударами
        while (Player.FullHP > 0 && Enemy.HP > 0)
        {
            Enemy.HP -= Math.Max(0, Player.FullDMG - Enemy.Armor);
            if (Enemy.HP > 0)
                Player.HP -= Math.Max(0, Enemy.DMG - Player.FullArmor);
            OnPropertyChanged(nameof(EnemyHPText));
            OnPropertyChanged(nameof(PlayerHPText));
            await Task.Delay(750);
        }

        await ShowResultAndExit();
    }

    private async void OnStealth()
    {
        if (Player.FullStealth < Enemy.LVL_to_sk)
        {
            await DisplayAlert("Неудача", "Ваша скрытность слишком низка. Враг Вас заметил", "OK, пипец.");
            OnAttack();
            return;
        }

            Enemy.HP = 0;
            await ShowResultAndExit();
    }

    private async Task ShowResultAndExit()
    {
        b1.IsEnabled = true;
        b2.IsEnabled = true;
        if (Player.FullHP <= 0)
        {
            bool load = await DisplayAlert("Поражение", "Вы погибли. Загрузить сохранение?", "Да", "Нет");
            if (load)
            {
                GameState.Reset();
                await Shell.Current.GoToAsync("///LoadGamePage");
            }
            else
            {
                Application.Current.Quit();
            }
            return;
        }

        // победа!
        Player.Experience += Enemy.Experience;
        Player.Gold += Enemy.Gold;
        await DisplayAlert("Победа!",
            $"Вы победили {Enemy.Name}!\n+{Enemy.Experience} EXP, +{Enemy.Gold} gold.",
            "OK"
        );

        if (GameState.IsStoryBattle)
        {
            GameState.StoryProgress.NextStory(GameState.Player);
            if (GameState.StoryProgress.CompletedEnemies == 2)
            {
                string xmlPath = Path.Combine(AppContext.BaseDirectory, "ShopItemZ.xml");
                GameState.Shop.AddShopItems(ShopItemLoader.LoadItemsFromXml(xmlPath));
            }
            GameState.IsStoryBattle = false;
        }

        await Shell.Current.GoToAsync("GamePage");
    }
    private void OnBackButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(GamePage));
    }
}