using lab2;
using lab2.Enums;
using System.Collections.ObjectModel;

namespace ui;

public partial class InventoryPage : ContentPage
{
    public ObservableCollection<Item> InventoryItems { get; set; }
    public ObservableCollection<Item> EquippedItems { get; set; }
    public InventoryPage()
	{
		InitializeComponent();
        LoadData();
        BindingContext = this;
    }
    private void LoadData()
    {
        InventoryItems = new ObservableCollection<Item>(GameState.Game.player.GetInventory());

        UpdateEquippedItems();
    }
    private async void OnInventoryItemSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Item selectedItem)
        {
            bool equipped = GameState.Game.player.EquipItem(selectedItem);
            if (equipped)
            {
                UpdateItems();
                ((CollectionView)sender).SelectedItem = null;
            }
            else
            {
                await DisplayAlert("Ошибка", "Невозможно экипировать предмет. Слот уже занят.", "OK");
            }
        }
    }
    private void UpdateItems()
    {
        UpdateEquippedItems();

        InventoryItems = new ObservableCollection<Item>(GameState.Game.player.GetInventory());
        OnPropertyChanged(nameof(InventoryItems));
    }

    private void UpdateEquippedItems()
    {
        var equippedList = GameState.Game.player.GetEquippedItems();
        var newEquippedItems = new ObservableCollection<Item>(equippedList);
        while (newEquippedItems.Count < 6)
        {
            newEquippedItems.Add(null);
        }
        EquippedItems = newEquippedItems;
        OnPropertyChanged(nameof(EquippedItems));
    }
    private async void OnEquipmentSlotTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.GestureRecognizers[0] is TapGestureRecognizer tapGesture)
        {
            string slotType = tapGesture.CommandParameter.ToString();
            EquipmentType equipmentType = Enum.Parse<EquipmentType>(slotType);
            bool unequipped = GameState.Game.player.UnequipItem(equipmentType);
            if (unequipped)
            {
                UpdateItems();
            }
            else
            {
                await DisplayAlert("Ошибка", "Этот слот пуст.", "OK");
            }
        }
    }
    private void OnBackButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(GamePage));
    }
}