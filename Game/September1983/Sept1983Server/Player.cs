using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server
{
    class Player
    {
        public String name;
        public Map map;

        // increments on every round
        public int laps = 0;

        public Player(String name) 
        {
            this.name = name;
        }

        public void startGame() 
        {
            map = new Map();
            // TODO add ships randomly
            //map.addShips();
        }
    }
}
