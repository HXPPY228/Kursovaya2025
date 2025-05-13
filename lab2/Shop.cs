using lab2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lab2
{
    public class Shop
    {
        [JsonInclude]
        private List<Item> shopItems;

        public Shop()
        {
            shopItems = new List<Item>
            {
                new Item("Leather Helmet", new Dictionary<string, int> { { "HP", 5 }, { "Armor", 1 } }, 10, EquipmentType.Head, "leather_helmet.png"),
                new Item("Bronze Helmet", new Dictionary<string, int> { { "HP", 10 }, { "Armor", 2 } }, 20, EquipmentType.Head, "bronze_helmet.png"),
                new Item("Chainmail Torso", new Dictionary<string, int> { { "HP", 10 }, { "Armor", 3 } }, 20, EquipmentType.Torso, "chainmail_torso.png"),
                new Item("Leather Pants", new Dictionary<string, int> { { "HP", 7 }, { "Armor", 2 } }, 15, EquipmentType.Legs, "leather_pants.png"),
                new Item("Traveler's Boots", new Dictionary<string, int> { { "HP", 3 }, { "Armor", 1 } }, 8, EquipmentType.Boots, "travelers_boots.png"),
                new Item("Wood Sword", new Dictionary<string, int> { { "DMG", 5 } }, 15, EquipmentType.FirstWeapon, "wood_sword.png"),
                new Item("Wood Dagger", new Dictionary<string, int> { { "DMG", 1 },{ "Stealth", 3 } }, 10, EquipmentType.SecondWeapon, "wood_dagger.png")
            };
        }

        public List<Item> GetShopItems()
        {
            return shopItems;
        }
        public bool SellItem(Item item, Player player)
        {
            if (shopItems.Contains(item))
            {
                int discountedPrice = item.Price - (player.Charisma / 2);
                if (player.Gold >= discountedPrice)
                {
                    player.Gold -= discountedPrice;
                    player.Inventory.Add(item);
                    shopItems.Remove(item);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
