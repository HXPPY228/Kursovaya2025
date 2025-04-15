using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.Types;

namespace lab2
{
    public class StoryProgress
    {
        public Enemy CurrentEnemy { get; set; }
        public List<Enemy> CompletedEnemies { get; set; }

        public StoryProgress()
        {
            CompletedEnemies = new List<Enemy>();
            var factory = new EnemyFactory();
            CurrentEnemy = factory.CreateEnemy("miniboss1");
        }

        public void AdvanceStory(Player player)
        {
            CompletedEnemies.Add(CurrentEnemy);
            if (CompletedEnemies.Count == 1)
            {
                var factory = new EnemyFactory();
                CurrentEnemy = factory.CreateEnemy("finalboss");
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
}
