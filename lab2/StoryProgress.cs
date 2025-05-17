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
        public int CompletedEnemies { get; set; }
        public StoryProgress()
        {
            CompletedEnemies = 0;
        }
        public Enemy GetCurrentEnemy()
        {
            var factory = new EnemyFactory();

            return CompletedEnemies switch
            {
                0 => factory.CreateEnemy("miniboss1"),
                1 => factory.CreateEnemy("finalboss"),
                _ => throw new NotImplementedException(), // ИГРА ПРОЙДЕНА!
            };
        }
        public void NextStory(Player player)
        {
            CompletedEnemies++;
        }
    }
}
