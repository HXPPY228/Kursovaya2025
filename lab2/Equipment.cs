using lab2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace lab2
{
    public class Equipment
    {
        public Item Head { get; set; }
        public Item Torso { get; set; }
        public Item Legs { get; set; }
        public Item Boots { get; set; }
        public Weapon FirstWeapon { get; set; }
        public Weapon SecondWeapon { get; set; }

        public List<Item> GetAll()
        {
            return new List<Item>
                {
                    Head,
                    Torso,
                    Legs,
                    Boots,
                    FirstWeapon,
                    SecondWeapon
                };
        }

        public bool Equip(Item item, Player player)
        {
            switch (item.Type)
            {
                case EquipmentType.Head:
                    if (Head != null) return false;
                    Head = item;
                    break;
                case EquipmentType.Torso:
                    if (Torso != null) return false;
                    Torso = item;
                    break;
                case EquipmentType.Legs:
                    if (Legs != null) return false;
                    Legs = item;
                    break;
                case EquipmentType.Boots:
                    if (Boots != null) return false;
                    Boots = item;
                    break;
                case EquipmentType.FirstWeapon:
                    if (!(item is Weapon w1) || FirstWeapon != null) return false;
                    FirstWeapon = w1;
                    break;
                case EquipmentType.SecondWeapon:
                    if (!(item is Weapon w2) || SecondWeapon != null) return false;
                    SecondWeapon = w2;
                    break;
                default:
                    return false;
            }

            player.UpdateStats(item);
            return true;
        }
        public Item Unequip(EquipmentType type, Player player)
        {
            Item item = null;
            switch (type)
            {
                case EquipmentType.Head:
                    item = Head;
                    Head = null;
                    break;
                case EquipmentType.Torso:
                    item = Torso;
                    Torso = null;
                    break;
                case EquipmentType.Legs:
                    item = Legs;
                    Legs = null;
                    break;
                case EquipmentType.Boots:
                    item = Boots;
                    Boots = null;
                    break;
                case EquipmentType.FirstWeapon:
                    item = FirstWeapon;
                    FirstWeapon = null;
                    break;
                case EquipmentType.SecondWeapon:
                    item = SecondWeapon;
                    SecondWeapon = null;
                    break;
            }

            if (item != null)
            {
                player.UpdateStats(item, -1);
            }
            return item;
        }
    }
}
