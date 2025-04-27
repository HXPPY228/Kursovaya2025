using lab2.Enums;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Player : Entity
    {
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int MaxHP { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public int Strength { get; set; }
        public int Stealth { get; set; }
        public int Agility { get; set; }
        public int Charisma { get; set; }
        public Equipment Equipment { get; private set; }
        public List<Item> Inventory { get; set; }

        public int AddedHP { get; private set; }
        public int AddedDMG { get; private set; }
        public int AddedArmor { get; private set; }
        public int AddedStealth { get; private set; }

        public int FullHP => (HP + Strength) + AddedHP;
        public int FullMaxHP => (MaxHP + Strength) + AddedHP;
        public int FullDMG => (DMG + (Strength / 2) + (Agility / 2)) + AddedDMG;
        public int FullArmor => (Armor + Agility) + AddedArmor;
        public int FullStealth => (Stealth + (Agility / 2)) + AddedStealth;

        public Player(string name, bool gender)
        {
            Name = name;
            Gender = gender;
            HP = 20;
            MaxHP = 20;
            Armor = 0;
            DMG = 5;
            Level = 1;
            Experience = 100;
            Gold = 100;
            Strength = 5;
            Stealth = 5;
            Agility = 5;
            Charisma = 5;
            AddedHP = 0;
            AddedDMG = 0;
            AddedArmor = 0;
            AddedStealth = 0;
            Inventory = new List<Item>();
            Equipment = new Equipment();
        }

        public void BuyItem(Item item, Shop shop)
        {
            shop.SellItem(item, this);
        }

        public bool UpgradeStats(int choice)
        {
            int cost = Level * 5;
            if (Experience < cost)
                return false;

            Experience -= cost;
            Level++;

            switch (choice)
            {
                case 1:
                    Strength++;
                    break;
                case 2:
                    Stealth++;
                    break;
                case 3:
                    Agility++;
                    break;
                case 4:
                    Charisma++;
                    break;
                default:
                    return false;
            }

            return true;
        }

        public List<Item> GetEquippedItems()
        {
            return Equipment.GetAll();
        }

        public List<Item> GetInventory()
        {
            return Inventory;
        }

        public bool EquipItem(Item item)
        {
            if (!Equipment.Equip(item, this))
                return false;

            return Inventory.Remove(item);
        }

        internal void UpdateStats(Item item, int multiplier = 1)
        {
            if (item.Stats.ContainsKey("HP"))
                AddedHP += item.Stats["HP"] * multiplier;
            if (item.Stats.ContainsKey("DMG"))
                AddedDMG += item.Stats["DMG"] * multiplier;
            if (item.Stats.ContainsKey("Armor"))
                AddedArmor += item.Stats["Armor"] * multiplier;
            if (item.Stats.ContainsKey("Stealth"))
                AddedStealth += item.Stats["Stealth"] * multiplier;
        }

        public void Rest()
        {
            if (FullHP == FullMaxHP)
                return;
            HP = MaxHP;
        }
    }
}
