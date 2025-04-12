using lab2.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Weapon : Item
    {
        public int Damage => Stats.ContainsKey("DMG") ? Stats["DMG"] : 0;

        public Weapon(string name, Dictionary<string, int> stats, int price, EquipmentType type)
            : base(name, stats, price, type)
        {
        }
    }
}
