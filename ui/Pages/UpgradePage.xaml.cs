namespace ui;

public partial class UpgradePage : ContentPage
{
	public UpgradePage()
	{
		InitializeComponent();
	}
    private async void OnUpgradeButtonClicked(object sender, EventArgs e)
    {
        var selectedRadioButton = RadioStack.Children
            .OfType<RadioButton>()
            .FirstOrDefault(rb => rb.IsChecked && rb.GroupName == "Stats");

        if (selectedRadioButton != null)
        {
            int choice = int.Parse(selectedRadioButton.Value.ToString());
            bool success = GameState.Game.player.UpgradeStats(choice);
            if (success)
            {
                await DisplayAlert("�����", "�������������� ������� ���������!", "OK");
                await Shell.Current.GoToAsync("GamePage");
            }
            else
            {
                StatusLabel.Text = "������������ ����� ��� ��������.";
                StatusLabel.TextColor = Color.FromHex("#D32F2F");
            }
        }
        else
        {
            StatusLabel.Text = "����������, �������� ��������������.";
        }
    }
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("GamePage");
    }
}