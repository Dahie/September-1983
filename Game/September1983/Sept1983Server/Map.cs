using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server
{
    class Map
    {
        public static int Size = 24;
        protected Field[,] fields = new Field[Size,Size];
        public static int allowedShots = 5;
        protected int allowedShotsLeft = allowedShots;

        // stores the string of successfully fired shots
        protected String results = "";

        public Map()
        {
            //foreach (Field field in fields)
            //    field = new Field();
        }

        public Boolean addShip( int shipSize, int position, Boolean horizontal)
        {
            // TODO: implement body
            return true;
        }

        public Boolean fireShot(int x, int y)
        {
            if(allowedShotsLeft > 0) 
            {
                var field = getField(x, y);
                allowedShots--;
                if(field.ship && field.shot) 
                {
                    // ship and already hit
                    results += "(" + x + "," + y + ") ship already hit";
                    return true;
                } 
                else if(field.ship && !field.shot) 
                {
                    // ship and not yet hit
                    // hit it!
                    field.shot = true;
                    results += "(" + x + "," + y + ") ship hit";
                    return true;
                } 
                else if(!field.ship && field.shot) 
                {
                    // water and already hit
                    results += "(" + x + "," + y + ") nothing hit";
                    return false;
                }
                else if (!field.ship && !field.shot)
                {
                    // water and not yet hit!
                    // splash!
                    results += "(" + x + "," + y + ") water already hit";
                    return false;
                }
            }
            

            return false;
        }

        public Field getField( int x, int y) 
        {
            // TODO: implement!
            // what's missing? - ds

            return fields[x, y];
        }

        public String FiredShotsResults() {
            return results;
        }

        public void resetResults() {
            results = "";
            allowedShotsLeft = allowedShots;
        }
    }
}
