namespace ui;

public partial class LoadGamePage : ContentPage
{
	public LoadGamePage()
	{
		InitializeComponent();
        LoadSaves();
    }
    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        string selectedSave = SavePicker.SelectedItem as string;

        if (string.IsNullOrEmpty(selectedSave) || selectedSave == "Нет сохранений")
        {
            await DisplayAlert("Ошибка", "Выберите действительное сохранение.", "OK");
            return;
        }
        // Логика для загрузки выбранного сохранения
        //await Shell.Current.GoToAsync("//GamePage");
    }
    private void LoadSaves()
    {
        string savesDirectory = Path.Combine(AppContext.BaseDirectory, "Saves");
        if (Directory.Exists(savesDirectory))
        {
            string[] saveFiles = Directory.GetFiles(savesDirectory, "*.sav");
            foreach (string file in saveFiles)
            {
                SavePicker.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
            if (SavePicker.Items.Count == 0)
            {
                SavePicker.Items.Add("Нет сохранений");
                SavePicker.IsEnabled = false;
            }
        }
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}