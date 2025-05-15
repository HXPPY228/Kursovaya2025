using lab2;
using lab2.Enums;
using System.Collections.ObjectModel;

namespace ui;

public partial class InventoryPage : ContentPage
{
    private bool _sellMode = false;
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
    private void OnSellModeToggled(object sender, ToggledEventArgs e)
    {
        _sellMode = e.Value;
    }
    private async void OnInventoryItemSelected(object sender, SelectionChangedEventArgs e)
    {
        var collection = (CollectionView)sender;
        var selectedItem = e.CurrentSelection.FirstOrDefault() as Item;
        collection.SelectedItem = null;

        if (selectedItem == null)
            return;

        if (SellModeSwitch.IsToggled)
        {
            bool confirm = await DisplayAlert(
                "�������",
                $"������� {selectedItem.Name} �� {(selectedItem.Price / 2 + GameState.Game.player.Charisma / 4)} (50% �� ��������� �������� + ����� �� �������) ������?",
                "��", "���"
            );
            if (!confirm)
                return;

            bool sold = GameState.Game.player.SellItem(selectedItem, GameState.Game.shop);
            if (sold)
                await DisplayAlert("�����", $"{selectedItem.Name} ������.", "OK");
            else
                await DisplayAlert("������", "�� ������� ������� �������.", "OK");
        }
        else
        {
            bool equipped = GameState.Game.player.EquipItem(selectedItem);
            if (!equipped)
                await DisplayAlert("������", "���������� ����������� �������. ���� ��� �����.", "OK");
        }
        UpdateItems();
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
                await DisplayAlert("������", "���� ���� ����.", "OK");
            }
        }
    }
    private async void OnSellButtonClicked(object sender, EventArgs e)
    {
        if (InventoryItems.Count == 0)
        {
            await DisplayAlert("�������", "��������� ����.", "OK");
            return;
        }

        var selectedItem = InventoryItems.FirstOrDefault(); // ��� ����� ������� ������� SelectedItem
        if (selectedItem == null)
        {
            await DisplayAlert("�������", "�������� ������� ��� �������.", "OK");
            return;
        }

        bool confirm = await DisplayAlert("�������", $"������� {selectedItem.Name}?", "��", "���");
        if (!confirm)
            return;

        bool sold = GameState.Game.player.SellItem(selectedItem, GameState.Game.shop);
        if (sold)
        {
            await DisplayAlert("�������", $"{selectedItem.Name} ������.", "OK");
            UpdateItems();
        }
        else
        {
            await DisplayAlert("������", "�� ������� ������� �������.", "OK");
        }
    }

    private void OnBackButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(GamePage));
    }
}