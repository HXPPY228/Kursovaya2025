using lab3;
using Microsoft.Maui.Storage;

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

        if (string.IsNullOrEmpty(selectedSave) || selectedSave == "��� ����������")
        {
            await DisplayAlert("������", "�������� �������������� ����������.", "OK");
            return;
        }
        selectedSave += ".sav";
        string filePath = Path.Combine(AppContext.BaseDirectory, selectedSave);
        GameSaveData saveData = GameSerializer.LoadGame(filePath);
        GameState.LoadSaveData(saveData);

        await Shell.Current.GoToAsync(nameof(GamePage));
    }
    private void LoadSaves()
    {
        string savesDirectory = Path.Combine(AppContext.BaseDirectory);
        if (Directory.Exists(savesDirectory))
        {
            string[] saveFiles = Directory.GetFiles(savesDirectory, "*.sav");
            foreach (string file in saveFiles)
            {
                SavePicker.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
            if (SavePicker.Items.Count == 0)
            {
                SavePicker.Items.Add("��� ����������");
                SavePicker.IsEnabled = false;
            }
        }
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}