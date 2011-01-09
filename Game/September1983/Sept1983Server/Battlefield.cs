using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server
{
    /// <summary>
    /// helper class for drawing a string-based battlefield from specific point of view
    /// </summary>
    class Battlefield
    {
        /// <summary>character used to indicate empty water</summary>
        public const String CHAR_WATER = " ";
        /// <summary>character used to indicate a shot into water</summary>
        public const String CHAR_WATER_SHOT = "O";
        /// <summary>character used to indicate a ship field NOT shot</summary>
        public const String CHAR_SHIP = "S";
        /// <summary>character used to indicate a ship field that has been hit</summary>
        public const String CHAR_SHIP_SHOT = "X";

        /// <summary>
        /// returns a processed with map of current player including and map of opponent with ship positions hidden
        /// </summary>
        /// <param name="mapOfCurrentPlayer">map of current player</param>
        /// <param name="mapOfOpponent">map of opponent</param>
        /// <returns>processed string e.g. for console</returns>
        public static String Draw(Map mapOfCurrentPlayer, Map mapOfOpponent)
        {
            int mapDim = mapOfOpponent.Dimension;
            String line = "\n";

            line += DrawHeader(mapDim);
            line += drawResultRows(mapOfCurrentPlayer, mapOfOpponent);

            line += "---+";
            for (int i = 0; i < mapDim; i++)
                line += "--";
            line += "-+---+";
            for (int i = 0; i < mapDim; i++)
                line += "--";
            line += "-+\n";

            return line;
        }

        /// <summary>
        /// returns string with header information of player and opponent map
        /// including column numbers
        /// </summary>
        /// <param name="mapDim">edge length of map</param>
        /// <returns>processed string</returns>
        private static String DrawHeader(int mapDim)
        {
            String oppWaters = "OPPONENTS WATERS";
            String yourWaters = "YOUR WATERS";

            String line = "____";
            for (int i = 0; i < ((2 * mapDim - oppWaters.Length) / 2); i++)
                line += "_";
            line += oppWaters;
            for (int i = 0; i < ((2 * mapDim - oppWaters.Length) / 2); i++)
                line += "_";

            line += "______";
            for (int i = 0; i < ((2 * mapDim - yourWaters.Length) / 2); i++)
                line += "_";
            line += yourWaters;
            for (int i = 0; i < ((2 * mapDim - yourWaters.Length) / 2); i++)
                line += "_";
            line += "__\n";

            for (int row = (int)Math.Floor(Math.Log10(mapDim)); row >= 0; row--)
            {
                line += "   |";
                line += drawNumbersHorizontal(mapDim, row > 0);

                line += " |   |";
                line += drawNumbersHorizontal(mapDim, row > 0);

                line += " | \n";
            }

            line += "---+";
            for (int i = 0; i < mapDim; i++)
                line += "--";
            line += "-+---+";
            for (int i = 0; i < mapDim; i++)
                line += "--";
            line += "-+\n";

            return line;
        }

        /// <summary>
        /// returns column numbers
        /// </summary>
        /// <param name="mapDim">edge length of map</param>
        /// <param name="tenRow">row of group of ten</param>
        /// <returns>processed string</returns>
        private static String drawNumbersHorizontal(int mapDim, Boolean tenRow)
        {
            String line = "";
            for (int j = 1; j <= mapDim; j++)
            {
                if (tenRow)
                    line += " " + (int)(j - Math.Floor((double)(j / 10)) * 10);
                else
                    line += (j < 10) ? "  " : " " + (int)Math.Floor((double)(j / 10));
            }
            return line;
        }

        /// <summary>
        /// returns rows with shots, hits, and ship
        /// </summary>
        /// <param name="mapOfCurrentPlayer">map of current player</param>
        /// <param name="mapOfOpponent">map of opponent</param>
        /// <returns>processed string</returns>
        private static String drawResultRows(Map mapOfCurrentPlayer, Map mapOfOpponent)
        {
            String line = "";
            int mapDim = mapOfCurrentPlayer.Dimension;

            for (int y = 0; y < mapDim; y++)
            {
                // opponent Field: Show hits and none Hits
                line += " ";
                line += (y < 10) ? " " : "";
                line += y + "|";
                for (int x = 0; x < mapDim; x++)
                    if (mapOfOpponent.getField(x, y).shot)
                        line += (mapOfOpponent.getField(x, y).ship) ? " " + CHAR_SHIP_SHOT : " " + CHAR_WATER_SHOT;
                    else
                        line += "  ";
                line += " | ";

                line += (y < 10) ? " " : "";
                line += y + "|";
                for (int x = 0; x < mapDim; x++)
                {
                    if (mapOfCurrentPlayer.getField(x, y).ship)
                        line += (mapOfCurrentPlayer.getField(x, y).shot) ? " " + CHAR_SHIP_SHOT : " " + CHAR_SHIP;
                    else
                        line += (mapOfCurrentPlayer.getField(x, y).shot) ? " " + CHAR_WATER_SHOT : " " + CHAR_WATER;
                }
                line += " | \n";
            }

            return line;
        }
    }
}
