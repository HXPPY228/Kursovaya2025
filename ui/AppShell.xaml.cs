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
        }
    }
}
