using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2;

namespace lab3
{
    public class GameSaveData
    {
        public Player Player { get; set; }
        public Shop Shop { get; set; }
        public List<(string Text, string ImagePath)> StoryScenes { get; set; }
        public int CurrentSceneIndex { get; set; }
    }
}
