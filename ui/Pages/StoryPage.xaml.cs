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
            StoryText.Text = "Конец сюжета.";
            StoryImage.Source = null;
        }
    }

    private void OnStoryTextTapped(object sender, EventArgs e)
    {
        GameState.CurrentSceneIndex++;
        if (GameState.CurrentSceneIndex == 11)
        {
            Shell.Current.GoToAsync(nameof(GamePage));
        }
        ShowCurrentScene();
    }
}