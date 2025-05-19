namespace ui;

public partial class StoryPage : ContentPage
{
    public StoryPage()
    {
        InitializeComponent();
        if (GameState.StoryScenes == null)
        {
            GameState.InitializeStory();
        }
        ShowCurrentScene();
    }
    private void ShowCurrentScene()
    {
        if (GameState.CurrentSceneIndex < GameState.StoryScenes.Count)
        {
            var scene = GameState.StoryScenes[GameState.CurrentSceneIndex];
            StoryText.Text = scene.Text;
            StoryImage.Source = scene.ImagePath;
        }
        else
        {
            Application.Current.Quit();
        }
    }

    private void OnStoryTextTapped(object sender, EventArgs e)
    {
        //Shell.Current.GoToAsync(nameof(GamePage));

        GameState.CurrentSceneIndex++;
        if (GameState.CurrentSceneIndex == 11)
        {
            Shell.Current.GoToAsync(nameof(GamePage));
        }
        ShowCurrentScene();
    }
}