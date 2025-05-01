using lab2.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using lab2.Types;
using System.Text.Json.Serialization;
using static System.Reflection.Metadata.BlobBuilder;

namespace lab2
{
    public class GameWindow
    {
        private Player player;
        private StoryProgress storyProgress;
        private Shop shop;

        public GameWindow(Player player)
        {
            this.player = player;
            storyProgress = new StoryProgress();
            shop = new Shop();
        }

        public Shop OpenShop()
        {
            Console.WriteLine("Welcome to the Shop!");
            return shop;
        }

        public Enemy StartFarm()
        {
            var factory = new EnemyFactory();
            Enemy enemy = factory.CreateRandomEnemy();
            return enemy;
        }

        public Enemy StartStory()
        {
            return storyProgress.CurrentEnemy;
        }

        //private void Fight(Enemy enemy, bool isStory)
        //{
        //    Console.WriteLine($"You encounter a {enemy.Name} (HP: {enemy.HP}, Armor: {enemy.Armor}, DMG: {enemy.DMG})");
        //    Console.WriteLine("1. Attack head-on");
        //    Console.WriteLine("2. Stealth attack");
        //    string choice = Console.ReadLine();

        //    bool stealthSuccess = false;
        //    if (choice == "2" && player.FullStealth >= enemy.LVL_to_sk)
        //    {
        //        Console.WriteLine("Stealth attack! Press the correct sequence (e.g., 1 2 3):");
        //        string sequence = Console.ReadLine();
        //        if (sequence == "1 2 3") // Simplified mini-game
        //        {
        //            stealthSuccess = true;
        //            Console.WriteLine("Stealth attack successful! Enemy defeated!");
        //            enemy.HP = 0;
        //        }
        //    }

        //    if (!stealthSuccess)
        //    {
        //        while (player.FullHP > 0 && enemy.HP > 0)
        //        {
        //            enemy.HP -= Math.Max(0, player.FullDMG - enemy.Armor);
        //            if (enemy.HP > 0)
        //            {
        //                player.HP -= Math.Max(0, enemy.DMG - player.FullArmor);
        //            }
        //            Console.WriteLine($"Player HP: {player.FullHP}, Enemy HP: {enemy.HP}");
        //        }

        //        if (player.FullHP <= 0)
        //        {
        //            Console.WriteLine("You died! Load a save? (y/n)");
        //            if (Console.ReadLine().ToLower() == "y")
        //            {
        //                //Load("save.json");
        //            }
        //            return;
        //        }
        //    }

        //    player.Experience += enemy.Experience;
        //    player.Gold += enemy.Gold;
        //    Console.WriteLine($"Victory! Gained {enemy.Experience} EXP and {enemy.Gold} gold.");

        //    if (isStory)
        //    {
        //        storyProgress.AdvanceStory(player);
        //    }
        //}

        public int Rest()
        {
            int healthBefore = player.HP;
            player.Rest();
            int healthRestored = player.HP - healthBefore;
            return healthRestored;
        }

        public bool Save(string FileName)
        {
            // Placeholder: Assuming save is successful
            return true;
        }

        public bool Load(string FileName)
        {
            // Placeholder: Assuming load is successful
            return true;
        }
    }
}