namespace ui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("GamePage", typeof(GamePage));
            Routing.RegisterRoute("CharacterCreationPage", typeof(CharacterCreationPage));
            Routing.RegisterRoute("LoadGamePage", typeof(LoadGamePage));
            Routing.RegisterRoute("StoryPage", typeof(StoryPage));
            Routing.RegisterRoute("ShopPage", typeof(ShopPage));
            Routing.RegisterRoute("InventoryPage", typeof(InventoryPage));
            Routing.RegisterRoute("UpgradePage", typeof(UpgradePage));
            Routing.RegisterRoute("FightPage", typeof(FightPage));
        }
    }
}
