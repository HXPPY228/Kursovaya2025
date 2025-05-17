using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Types
{
    public class Slime : Enemy
    {
        public Slime() : base("Slime", 5, 0, 2, 1, 5, 2, "slime.png") { }
    }
    public class Guard : Enemy
    {
        public Guard() : base("Guard", 50, 5, 10, 5, 20, 15, "guard.png") { }
    }
    public class MiniBoss1 : Enemy
    {
        public MiniBoss1() : base("Dan 'Axe' Radley", 30, 3, 8, 3, 15, 10, "mb1.png") { }
    }
    public class FinalBoss : Enemy
    {
        public FinalBoss() : base("Garret 'Claw' Veil", 100, 10, 20, 10, 50, 50, "fb.png") { }
    }
}
