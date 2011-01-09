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

        public Player(String name, Map map)
        {
            this.name = name;
            this.map = map;
        }
    }
}
