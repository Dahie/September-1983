using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server
{
    /// <summary>
    /// represents ship on map
    /// </summary>
    public class Ship
    {
        /// <summary>size of shipp in number of fields</summary>
        public int size;
        /// <summary>horizontal positioning</summary>
        public Boolean horizontal;

        /// <summary>
        /// creates an instance of a ship 
        /// </summary>
        /// <param name="size">size of shipp in number of fields</param>
        /// <param name="horizontal">horizontal positioning</param>
        public Ship(int size, Boolean horizontal)
        {
            this.size = size;
            this.horizontal = horizontal;
        }
    }
}
