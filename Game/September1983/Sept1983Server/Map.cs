using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server
{
    /// <summary>
    /// represents map of a player
    /// </summary>
    class Map
    {
        /// <summary>edge length of map</summary>
        protected static int dimension;
        /// <summary>fields of map</summary>
        protected Field[,] fields;
        /// <summary>max number of shots per shot sequence</summary>
        public static int allowedShots = 5;
        /// <summary>shots left in sequence</summary>
        protected int allowedShotsLeft = allowedShots;
        /// <summary>stores the string of successfully fired shots</summary>
        protected String results = "";

        /// <summary>
        /// Creates map with empty fields
        /// </summary>
        /// <param name="mapDimension">edge length of map</param>
        public Map(int mapDimension)
        {
            fields = new Field[mapDimension, mapDimension];
            for (int i = 0; i < fields.GetLength(0); i++)
                for (int j = 0; j < fields.GetLength(1); j++)
                    fields[i, j] = new Field();
            dimension = mapDimension;
        }

        /// <summary>
        /// fires shot at given coordinates
        /// </summary>
        /// <param name="x">position x</param>
        /// <param name="y">position y</param>
        /// <returns>true if hit</returns>
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

        /// <summary>
        /// returns field at given coordinates (x|y) 
        /// </summary>
        /// <param name="x">position x</param>
        /// <param name="y">position y</param>
        /// <returns>corresponding field</returns>
        public Field getField(int x, int y)
        {
            return fields[x, y];
        }



        public int GetSize()
        {
            return fields.GetLength(0);
        }

        /// <summary>
        /// returns all fired shots
        /// </summary>
        /// <returns>string with all shots fired on this map</returns>
        public String FiredShotsResults()
        {
            return results;
        }

        /// <summary>
        /// resets all successful hits
        /// </summary>
        public void resetResults()
        {
            results = "";
            allowedShotsLeft = allowedShots;
        }

        /// <summary>
        /// edge length of map
        /// </summary>
        public int Dimension
        {
            get
            {
                return dimension;
            }
        }

        /// <summary>
        /// generates ships of size 5, 4, 3, and 2 and places them on map                   
        /// </summary>
        /// <param name="map">map on which ships are placed on</param>
        /// <param name="numberOfShipFields">max number of ship fields</param>
        /// <returns>map with ships</returns>
        public static Map GenerateShipsOnMap(Map map, int numberOfShipFields)
        {
            int numberOfFiver = (int)Math.Floor((double)(((5.0 / 24) * numberOfShipFields) / 5));
            int numberOfFoursomes = (int)Math.Floor((double)(((8.0 / 24) * numberOfShipFields) / 4));
            int numberOfThreesomes = (int)Math.Floor((double)(((9.0 / 24) * numberOfShipFields) / 3));
            int numberOfTwoSomes = (int)Math.Floor((double)(((4.0 / 24) * numberOfShipFields) / 2));

            Ship[] ships = new Ship[numberOfFiver + numberOfFoursomes + numberOfThreesomes + numberOfTwoSomes];
            Random random = new Random();
            int counter = 0;

            for (int i = 0; i < numberOfFiver; i++)
                ships[counter++] = new Ship(5, (random.NextDouble() > 0.5));
            for (int i = 0; i < numberOfFoursomes; i++)
                ships[counter++] = new Ship(4, (random.NextDouble() > 0.5));
            for (int i = 0; i < numberOfThreesomes; i++)
                ships[counter++] = new Ship(3, (random.NextDouble() > 0.5));
            for (int i = 0; i < numberOfTwoSomes; i++)
                ships[counter++] = new Ship(2, (random.NextDouble() > 0.5));

            if (map.Dimension * map.Dimension < numberOfShipFields)
                return null;

            foreach (Ship ship in ships)
            {
                Boolean found = false;
                int randomX;
                int randomY;

                while (!found)
                {
                    randomX = random.Next(map.Dimension);
                    randomY = random.Next(map.Dimension);
                    found = AddShip(map, ship, randomX, randomY);
                }
            }
            return map;
        }

        /// <summary>
        /// generates ships of size 5, 4, 3, and 2 and places them on map
        /// </summary>
        /// <param name="map">map on which ships are placed on</param>
        /// <returns>map with ships</returns>
        public static Map GenerateShipsOnMap(Map map)
        {
            return GenerateShipsOnMap(map, (int)Math.Floor(Math.Pow(map.Dimension, 2) / 10));
        }

        /// <summary>
        /// adds a ship to given coordinates if possible       
        /// </summary>
        /// <param name="map">player map</param>
        /// <param name="ship">ship to be placed</param>
        /// <param name="posX">position x where ship maybe placed</param>
        /// <param name="posY">position y where ship maybe placed</param>
        /// <returns>true, if ship could be placed</returns>
        public static Boolean AddShip(Map map, Ship ship, int posX, int posY)
        {
            int sizeX;
            int sizeY;

            if (ship.horizontal)
            {
                sizeX = ship.size;
                sizeY = 1;
            }
            else
            {
                sizeX = 1;
                sizeY = ship.size;
            }

            for (int y = posY; y < (posY + sizeY); y++)
                for (int x = posX; x < (posX + sizeX); x++)
                    if (x >= map.Dimension || y >= map.Dimension || map.getField(x, y).ship)
                        return false;

            for (int y = posY; y < (posY + sizeY); y++)
                for (int x = posX; x < (posX + sizeX); x++)
                    map.getField(x, y).ship = true;

            return true;
        }
    }
}
