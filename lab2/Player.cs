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
        public int MaxHP { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public int Strength { get; set; }
        public int Stealth { get; set; }
        public int Agility { get; set; }
        public int Charisma { get; set; }
        public Item Head { get; set; }
        public Item Torso { get; set; }
        public Item Legs { get; set; }
        public Item Boots { get; set; }
        public Weapon FirstWeapon { get; set; }
        public Weapon SecondWeapon { get; set; }
        public List<Item> Inventory { get; set; }

        public int AddedHP { get; set; }
        public int AddedDMG { get; set; }
        public int AddedArmor { get; set; }
        public int AddedStealth { get; set; }

        public int FullHP => (HP + Strength) + AddedHP;
        public int FullMaxHP => (MaxHP + Strength) + AddedHP;
        public int FullDMG => (DMG + (Strength / 2) + (Agility / 2)) + AddedDMG;
        public int FullArmor => (Armor + Agility) + AddedArmor;
        public int FullStealth => (Stealth + (Agility / 2)) + AddedStealth;

        public Player()
        {
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
        }

        public void BuyItem(Item item, Shop shop)
        {
            shop.SellItem(item, this);
        }

        public void UpgradeStats()
        {
            if (Experience < Level * 5)
            {
                Console.WriteLine("Not enough experience to level up!");
                return;
            }

            Experience -= Level * 5;
            Level++;
            Console.WriteLine($"Level up! You are now level {Level}. Choose a stat to upgrade:");
            Console.WriteLine("1. Strength");
            Console.WriteLine("2. Stealth");
            Console.WriteLine("3. Agility");
            Console.WriteLine("4. Charisma");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Strength++;
                    Console.WriteLine("Strength increased!");
                    break;
                case "2":
                    Stealth++;
                    Console.WriteLine("Stealth increased!");
                    break;
                case "3":
                    Agility++;
                    Console.WriteLine("Agility increased!");
                    break;
                case "4":
                    Charisma++;
                    Console.WriteLine("Charisma increased!");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        public List<Item> GetEquippedItems()
        {
            return new List<Item>{
                Head,
                Torso,
                Legs,
                Boots,
                FirstWeapon,
                SecondWeapon
            };
        }
        public List<Item> GetInventory()
        {
            return Inventory;
        }

        public void EquipItem(Item item)
        {
            switch (item.Type)
            {
                case EquipmentType.Head:
                    if (Head == null)
                    {
                        Head = item;
                        UpdateStats(item);
                    }
                    else
                    {
                        //Console.WriteLine("Слот для головного убора уже занят.");
                        return;
                    }
                    break;
                case EquipmentType.Torso:
                    if (Torso == null)
                    {
                        Torso = item;
                        UpdateStats(item);
                    }
                    else
                    {
                        //Console.WriteLine("Слот для брони на торс уже занят.");
                        return;
                    }
                    break;
                case EquipmentType.Legs:
                    if (Legs == null)
                    {
                        Legs = item;
                        UpdateStats(item);
                    }
                    else
                    {
                        //Console.WriteLine("Слот для штанов уже занят.");
                        return;
                    }
                    break;
                case EquipmentType.Boots:
                    if (Boots == null)
                    {
                        Boots = item;
                        UpdateStats(item);
                    }
                    else
                    {
                        //Console.WriteLine("Слот для обуви уже занят.");
                        return;
                    }
                    break;
                case EquipmentType.FirstWeapon:
                    if (item is Weapon weapon)
                    {
                        if (FirstWeapon == null)
                        {
                            FirstWeapon = weapon;
                            UpdateStats(item);
                        }
                        else
                        {
                            //Console.WriteLine("Слот для основного оружия уже занят.");
                            return;
                        }
                    }
                    break;
                case EquipmentType.SecondWeapon:
                    if (item is Weapon weapon2)
                    {
                        if (SecondWeapon == null)
                        {
                            SecondWeapon = weapon2;
                            UpdateStats(item);
                        }
                        else
                        {
                            //Console.WriteLine("Слот для вторичного оружия уже занят.");
                            return;
                        }
                    }
                    break;
                default:
                    //Console.WriteLine("Неизвестный тип экипировки.");
                    return;
            }
            Inventory.Remove(item);
            //Console.WriteLine($"{item.Name} надет!");
        }

        private void UpdateStats(Item item, int multiplier = 1)
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
            {
                Console.WriteLine("Health is already fully restored!");
                return;
            }
            HP = MaxHP;
        }
    }
}
