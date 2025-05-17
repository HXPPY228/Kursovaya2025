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
        public Player player { get; set; }
        public StoryProgress storyProgress { get; set; }
        public Shop shop { get; set; }

        public GameWindow(Player player, StoryProgress storyProgress, Shop shop)
        {
            this.player = player;
            this.storyProgress = storyProgress;
            this.shop = shop;
        }
        public bool SpendGold(int gold)
        {
            if (player.Gold < gold)
            {
                return false;
            }
            player.Gold -= gold;
            return true;
        }

        public List<Item> GetShopItems()
        {
            return shop.GetShopItems();
        }

        public Enemy StartFarm()
        {
            var factory = new EnemyFactory();
            Enemy enemy = factory.CreateRandomEnemy();
            return enemy;
        }

        public Enemy StartStory()
        {
            return storyProgress.GetCurrentEnemy();
        }

        public int Rest()
        {
            int healthBefore = player.HP;
            player.Rest();
            int healthRestored = player.HP - healthBefore;
            return healthRestored;
        }
    }
}