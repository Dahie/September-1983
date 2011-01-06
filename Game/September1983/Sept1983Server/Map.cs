using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server
{
    class Map
    {
        protected Field[,] fields = new Field[24,24];

        public Map()
        {
            foreach (Field field in fields)
                field = new Field();
        }

        public Boolean addShip( int shipSize, int position, Boolean horizontal)
        {
            // TODO: implement body
            return true;
        }

        public Boolean fireShot(int x, int y)
        {
            return true;
        }

        public Field getField( int x, int y) 
        {
            // TODO: implement!
            return fields[x,y];
        }
    }
}
