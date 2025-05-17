using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public abstract class Enemy : Entity
    {
        public int LVL_to_sk { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Enemy(string name, int hp, int armor, int dmg, int lvl_to_sk, int experience, int gold, string path)
        {
            Name = name;
            HP = hp;
            Armor = armor;
            DMG = dmg;
            LVL_to_sk = lvl_to_sk;
            Experience = experience;
            Gold = gold;
            Path = path;
        }
    }
}
