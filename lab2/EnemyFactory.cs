using lab2.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class EnemyFactory
    {
        public Enemy CreateEnemy(string type)
        {
            switch (type.ToLower())
            {
                case "slime": return new Slime();
                case "guard": return new Guard();
                case "miniboss1": return new MiniBoss1();
                case "miniboss2": return new MiniBoss2();
                case "miniboss3": return new MiniBoss3();
                case "finalboss": return new FinalBoss();
                default: throw new ArgumentException("Unknown enemy type");
            }
        }
        public Enemy CreateRandomEnemy()
        {
            Random rand = new Random();
            int enemyType = rand.Next(2);
            switch (enemyType)
            {
                case 0: return CreateEnemy("slime");
                case 1: return CreateEnemy("guard");
                default: throw new ArgumentException("Unknown enemy type");
            }
        }
    }
}
