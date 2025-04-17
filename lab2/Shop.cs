using lab2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Shop
    {
        private List<Item> shopItems;

        public Shop()
        {
            shopItems = new List<Item>
            {
                new Item("Leather Helmet", new Dictionary<string, int> { { "HP", 5 }, { "Armor", 1 } }, 10, EquipmentType.Head),
                new Item("Test Helmet", new Dictionary<string, int> { { "HP", 10 }, { "Armor", 2 } }, 20, EquipmentType.Head),
                new Item("Chainmail Torso", new Dictionary<string, int> { { "HP", 10 }, { "Armor", 3 } }, 20, EquipmentType.Torso),
                new Item("Leather Pants", new Dictionary<string, int> { { "HP", 7 }, { "Armor", 2 } }, 15, EquipmentType.Legs),
                new Item("Traveler's Boots", new Dictionary<string, int> { { "HP", 3 }, { "Armor", 1 } }, 8, EquipmentType.Boots),
                new Weapon("Sword", new Dictionary<string, int> { { "DMG", 5 } }, 15, EquipmentType.FirstWeapon),
                new Weapon("Dagger", new Dictionary<string, int> { { "Stealth", 3 } }, 10, EquipmentType.SecondWeapon)
            };
        }

        public List<Item> GetShopItems()
        {
            return shopItems;
        }
        public void SellItem(Item item, Player player)
        {
            if (shopItems.Contains(item))
            {
                int discountedPrice = item.Price - (player.Charisma / 2);
                if (player.Gold >= discountedPrice)
                {
                    player.Gold -= discountedPrice;
                    player.Inventory.Add(item);
                    shopItems.Remove(item);
                    //Console.WriteLine($"Bought {item.Name} for {discountedPrice} gold!");
                }
                else
                {
                    //Console.WriteLine("Not enough gold!");
                }
            }
            else
            {
                //Console.WriteLine("Item not found in shop!");
            }
        }
    }
}
