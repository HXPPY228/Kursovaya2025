using lab2.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using lab2.Types;
using System.Text.Json.Serialization;

namespace lab2
{
    public class GameWindow
    {
        private Player player;
        private StoryProgress storyProgress;
        private Shop shop;
        private List<Item> shopItems;

        public GameWindow()
        {
            player = new Player();
            storyProgress = new StoryProgress();
            shop = new Shop();
            shopItems = shop.GetShopItems();
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
                player.BuyItem(shopItems[index], shop);
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
                    enemy.HP = 0;
                }
            }

            if (!stealthSuccess)
            {
                while (player.FullHP > 0 && enemy.HP > 0)
                {
                    enemy.HP -= Math.Max(0, player.FullDMG - enemy.Armor);
                    if (enemy.HP > 0)
                    {
                        player.HP -= Math.Max(0, enemy.DMG - player.FullArmor);
                    }
                    Console.WriteLine($"Player HP: {player.FullHP}, Enemy HP: {enemy.HP}");
                }

                if (player.FullHP <= 0)
                {
                    Console.WriteLine("You died! Load a save? (y/n)");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        Load("save.json");
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
        
        
        public void Save(string FileName)
        {
            
        }
        

        public void Load(string FileName)
        {
            
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine($"\nHP: {player.FullHP}/{player.FullMaxHP}, Armor: {player.FullArmor}, DMG: {player.FullDMG}, Gold: {player.Gold}, EXP: {player.Experience}, Level: {player.Level}, Strength: {player.Strength}, Stealth: {player.Stealth}, Agility: {player.Agility}, Charisma: {player.Charisma}");
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
                        Save("save.json");
                        break;
                    case "8":
                        Load("save.json");
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