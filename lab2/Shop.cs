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
            shopItems = new List<Item> {};
        }
        public void AddShopItems(List<Item> items)
        {
            if (items == null || items.Count == 0)
                return;

            foreach (Item item in items)
            {
                if (item != null)
                {
                    shopItems.Add(item);
                }
            }
        }

        public List<Item> GetShopItems()
        {
            return shopItems;
        }
        public bool SellItem(Item item, Player player)
        {
            if (shopItems.Contains(item))
            {
                int discountedPrice = item.Price - (player.Charisma / 4);
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
