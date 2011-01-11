using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server
{
    /// <summary>
    /// represents player in game
    /// </summary>
    class Player
    {
        /// <summary>player name</summary>
        public String name;
        /// <summary>player map</summary>
        public Map map;

        // increments on every round
        public int laps = 0;
        /// <summary>
        /// creates player instance  
        /// </summary>
        /// <param name="name">player name</param>
        /// <param name="map">player map</param>
        public Player(String name, Map map)
        {
            this.name = name;
            this.map = map;
        }
    }
}
