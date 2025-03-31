using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RedemptionRPG
{
    // Item class
    public class Item
    {
        public string Name { get; set; }
        public Dictionary<string, int> Stats { get; set; }
        public int Price { get; set; }

        public Item(string name, Dictionary<string, int> stats, int price)
        {
            Name = name;
            Stats = stats;
            Price = price;
        }
    }

    // Weapon class (inherits from Item)
    public class Weapon : Item
    {
        public int Damage => Stats.ContainsKey("DMG") ? Stats["DMG"] : 0;

        public Weapon(string name, Dictionary<string, int> stats, int price)
            : base(name, stats, price)
        {
        }
    }

    // Enemy class
    public class Enemy
    {
        public int HP { get; set; }
        public int Armor { get; set; }
        public int DMG { get; set; }
        public int LVL_to_sk { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public string Name { get; set; }

        public Enemy(string name, int hp, int armor, int dmg, int lvl_to_sk, int experience, int gold)
        {
            Name = name;
            HP = hp;
            Armor = armor;
            DMG = dmg;
            LVL_to_sk = lvl_to_sk;
            Experience = experience;
            Gold = gold;
        }
    }

    // Slime class (inherits from Enemy)
    public class Slime : Enemy
    {
        public Slime() : base("Slime", 5, 0, 2, 1, 5, 2) { }
    }

    // Guard class (inherits from Enemy)
    public class Guard : Enemy
    {
        public Guard() : base("Guard", 50, 5, 10, 5, 20, 15) { }
    }

    // StoryProgress class
    public class StoryProgress
    {
        public Enemy CurrentEnemy { get; set; }
        public List<Enemy> CompletedEnemies { get; set; }

        public StoryProgress()
        {
            CompletedEnemies = new List<Enemy>();
            // Initial story enemy
            CurrentEnemy = new Enemy("Dan 'Axe' Radley", 30, 3, 8, 3, 15, 10);
        }

        public void AdvanceStory(Player player)
        {
            CompletedEnemies.Add(CurrentEnemy);
            if (CompletedEnemies.Count == 1)
            {
                CurrentEnemy = new Enemy("Garret 'Claw' Veil", 100, 10, 20, 10, 50, 50); // Final boss
            }
            else
            {
                Console.WriteLine("You have defeated the final boss! Choose your ending:");
                Console.WriteLine("1. Rule the land with an iron fist");
                Console.WriteLine("2. Bring peace to the realm");
                string choice = Console.ReadLine();
                Console.WriteLine($"Game Over! You chose to {(choice == "1" ? "rule with an iron fist" : "bring peace")}.");
                Environment.Exit(0);
            }
        }
    }

    // Player class
    public class Player
    {
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Armor { get; set; }
        public int DMG { get; set; }
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

        public Player()
        {
            HP = 20;
            MaxHP = 20;
            Armor = 0;
            DMG = 5;
            Level = 1;
            Experience = 0;
            Gold = 50;
            Strength = 5;
            Stealth = 5;
            Agility = 5;
            Charisma = 5;
            Inventory = new List<Item>();
        }

        public void BuyItem(Item item)
        {
            int discountedPrice = item.Price - (Charisma / 2); // Discount based on Charisma
            if (Gold >= discountedPrice)
            {
                Gold -= discountedPrice;
                Inventory.Add(item);
                Console.WriteLine($"Bought {item.Name} for {discountedPrice} gold!");
            }
            else
            {
                Console.WriteLine("Not enough gold!");
            }
        }

        public void UpgradeStats()
        {
            if (Experience < Level * 10)
            {
                Console.WriteLine("Not enough experience to level up!");
                return;
            }

            Experience -= Level * 10;
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
                    MaxHP += 5;
                    HP = MaxHP;
                    Console.WriteLine("Strength increased!");
                    break;
                case "2":
                    Stealth++;
                    Console.WriteLine("Stealth increased!");
                    break;
                case "3":
                    Agility++;
                    DMG += 2;
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

        public void ViewInventory()
        {
            Console.WriteLine("Equipped Items:");
            Console.WriteLine($"Head: {(Head != null ? Head.Name : "None")}");
            Console.WriteLine($"Torso: {(Torso != null ? Torso.Name : "None")}");
            Console.WriteLine($"Legs: {(Legs != null ? Legs.Name : "None")}");
            Console.WriteLine($"Boots: {(Boots != null ? Boots.Name : "None")}");
            Console.WriteLine($"First Weapon: {(FirstWeapon != null ? FirstWeapon.Name : "None")}");
            Console.WriteLine($"Second Weapon: {(SecondWeapon != null ? SecondWeapon.Name : "None")}");
            Console.WriteLine("\nInventory:");
            for (int i = 0; i < Inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Inventory[i].Name}");
            }

            Console.WriteLine("Enter the number of the item to equip (or 0 to exit):");
            string input = Console.ReadLine();
            if (input == "0") return;

            int index = int.Parse(input) - 1;
            if (index >= 0 && index < Inventory.Count)
            {
                Item item = Inventory[index];
                EquipItem(item);
            }
        }

        private void EquipItem(Item item)
        {
            if (item.Name.Contains("Head"))
            {
                Head = item;
                UpdateStats(item, "HP", "Armor");
            }
            else if (item.Name.Contains("Torso"))
            {
                Torso = item;
                UpdateStats(item, "HP", "Armor");
            }
            else if (item.Name.Contains("Legs"))
            {
                Legs = item;
                UpdateStats(item, "HP", "Armor");
            }
            else if (item.Name.Contains("Boots"))
            {
                Boots = item;
                UpdateStats(item, "HP", "Armor");
            }
            else if (item is Weapon weapon)
            {
                if (FirstWeapon == null)
                {
                    FirstWeapon = weapon;
                }
                else
                {
                    SecondWeapon = weapon;
                }
                UpdateStats(item, "DMG");
            }
            Console.WriteLine($"{item.Name} equipped!");
        }

        private void UpdateStats(Item item, params string[] statTypes)
        {
            foreach (var stat in statTypes)
            {
                if (item.Stats.ContainsKey(stat))
                {
                    if (stat == "HP") MaxHP += item.Stats[stat];
                    else if (stat == "Armor") Armor += item.Stats[stat];
                    else if (stat == "DMG") DMG += item.Stats[stat];
                }
            }
        }

        public void Rest()
        {
            if (HP == MaxHP)
            {
                Console.WriteLine("Health is already fully restored!");
                return;
            }
            HP = Math.Min(MaxHP, HP + (Strength * 2));
            Console.WriteLine($"Rested! HP restored to {HP}.");
        }
    }

    // GameWindow class
    public class GameWindow
    {
        private Player player;
        private StoryProgress storyProgress;
        private List<Item> shopItems;

        public GameWindow()
        {
            player = new Player();
            storyProgress = new StoryProgress();
            shopItems = new List<Item>
            {
                new Item("Leather Helmet", new Dictionary<string, int> { { "HP", 5 }, { "Armor", 1 } }, 10),
                new Item("Chainmail Torso", new Dictionary<string, int> { { "HP", 10 }, { "Armor", 3 } }, 20),
                new Weapon("Sword", new Dictionary<string, int> { { "DMG", 5 } }, 15)
            };
        }

        public void OpenShop()
        {
            Console.WriteLine("Welcome to the Shop!");
            for (int i = 0; i < shopItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {shopItems[i].Name} - {shopItems[i].Price} gold");
            }
            Console.WriteLine("Enter the number of the item to buy (or 0 to exit):");
            string input = Console.ReadLine();
            if (input == "0") return;

            int index = int.Parse(input) - 1;
            if (index >= 0 && index < shopItems.Count)
            {
                player.BuyItem(shopItems[index]);
            }
        }

        public void StartFarm()
        {
            Random rand = new Random();
            Enemy enemy = rand.Next(2) == 0 ? new Slime() : new Guard();
            Fight(enemy, false);
        }

        public void StartStory()
        {
            Fight(storyProgress.CurrentEnemy, true);
        }

        private void Fight(Enemy enemy, bool isStory)
        {
            Console.WriteLine($"You encounter a {enemy.Name} (HP: {enemy.HP}, Armor: {enemy.Armor}, DMG: {enemy.DMG})");
            Console.WriteLine("1. Attack head-on");
            Console.WriteLine("2. Stealth attack");
            string choice = Console.ReadLine();

            bool stealthSuccess = false;
            if (choice == "2" && player.Stealth >= enemy.LVL_to_sk)
            {
                Console.WriteLine("Stealth attack! Press the correct sequence (e.g., 1 2 3):");
                string sequence = Console.ReadLine();
                if (sequence == "1 2 3") // Simplified mini-game
                {
                    stealthSuccess = true;
                    Console.WriteLine("Stealth attack successful! Enemy defeated!");
                }
            }

            if (!stealthSuccess)
            {
                while (player.HP > 0 && enemy.HP > 0)
                {
                    enemy.HP -= Math.Max(0, player.DMG - enemy.Armor);
                    if (enemy.HP > 0)
                    {
                        player.HP -= Math.Max(0, enemy.DMG - player.Armor);
                    }
                    Console.WriteLine($"Player HP: {player.HP}, Enemy HP: {enemy.HP}");
                }

                if (player.HP <= 0)
                {
                    Console.WriteLine("You died! Load a save? (y/n)");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        Load();
                    }
                    return;
                }
            }

            player.Experience += enemy.Experience;
            player.Gold += enemy.Gold;
            Console.WriteLine($"Victory! Gained {enemy.Experience} EXP and {enemy.Gold} gold.");

            if (isStory)
            {
                storyProgress.AdvanceStory(player);
            }
        }

        public void Rest()
        {
            player.Rest();
        }

        public void Save()
        {
            var saveData = new { Player = player, StoryProgress = storyProgress };
            File.WriteAllText("save.json", JsonSerializer.Serialize(saveData));
            Console.WriteLine("Game saved!");
        }

        public void Load()
        {
            if (File.Exists("save.json"))
            {
                string json = File.ReadAllText("save.json");
                var saveData = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                // Note: Proper deserialization requires more setup; this is a placeholder
                Console.WriteLine("Game loaded!");
            }
            else
            {
                Console.WriteLine("Save file not found!");
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine($"\nHP: {player.HP}/{player.MaxHP}, Gold: {player.Gold}, EXP: {player.Experience}, Level: {player.Level}");
                Console.WriteLine("1. Shop");
                Console.WriteLine("2. Farm");
                Console.WriteLine("3. Story");
                Console.WriteLine("4. Rest");
                Console.WriteLine("5. Upgrade Stats");
                Console.WriteLine("6. Inventory");
                Console.WriteLine("7. Save");
                Console.WriteLine("8. Load");
                Console.WriteLine("9. Exit");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        OpenShop();
                        break;
                    case "2":
                        StartFarm();
                        break;
                    case "3":
                        StartStory();
                        break;
                    case "4":
                        Rest();
                        break;
                    case "5":
                        player.UpgradeStats();
                        break;
                    case "6":
                        player.ViewInventory();
                        break;
                    case "7":
                        Save();
                        break;
                    case "8":
                        Load();
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }

    // Main program
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Redemption RPG!");
            GameWindow game = new GameWindow();
            game.Run();
        }
    }
}