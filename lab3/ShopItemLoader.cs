using lab2.Enums;
using lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab3
{
    public static class ShopItemLoader
    {
        public static List<Item> LoadItemsFromXml(string xmlFilePath)
        {
            var items = new List<Item>();
            var doc = XDocument.Load(xmlFilePath);

            foreach (var itemElem in doc.Descendants("Item"))
            {
                string name = itemElem.Element("Name")?.Value ?? "Unknown";
                int price = int.Parse(itemElem.Element("Price")?.Value ?? "0");
                string typeStr = itemElem.Element("Type")?.Value ?? "Head";
                string image = itemElem.Element("Image")?.Value ?? "";

                EquipmentType type = Enum.TryParse(typeStr, out EquipmentType parsedType)
                    ? parsedType
                    : EquipmentType.Head;

                var stats = new Dictionary<string, int>();
                var statElems = itemElem.Element("Stats")?.Elements("Stat");
                if (statElems != null)
                {
                    foreach (var stat in statElems)
                    {
                        string key = stat.Attribute("key")?.Value ?? "";
                        int value = int.Parse(stat.Attribute("value")?.Value ?? "0");
                        if (!string.IsNullOrEmpty(key))
                            stats[key] = value;
                    }
                }

                items.Add(new Item(name, stats, price, type, image));
            }

            return items;
        }
    }
}
