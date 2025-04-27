using lab2;

namespace ui;

public partial class GamePage : ContentPage
{
    public GamePage()
    {
        InitializeComponent();

        GameState.Game = new GameWindow(GameState.Player);
        string avatarSource = GameState.Player.Gender ? "avatar_male.png" : "avatar_female.png";
    }
}