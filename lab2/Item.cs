using lab2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Item
    {
        public string Name { get; set; }
        public Dictionary<string, int> Stats { get; set; }
        public int Price { get; set; }
        public EquipmentType Type { get; set; }

        public Item(string name, Dictionary<string, int> stats, int price, EquipmentType type)
        {
            Name = name;
            Stats = stats;
            Price = price;
            Type = type;
        }
    }
}
