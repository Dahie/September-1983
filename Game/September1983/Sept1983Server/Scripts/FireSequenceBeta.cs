using System;
using System.Collections.Generic;
using System.Text;

using Sept1983Server;


class FireSequenceBeta : IFireSequence
{

    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public void Launch(Map map)
    {
        //TODO: Implementierung der Abschusssequenz

        for (int i = 0; i < Map.allowedShots; i++)
        {
            int randX = RandomNumber(0, map.GetSize());
            int randY = RandomNumber(0, map.GetSize());
            map.fireShot(randX, randY);
        }
    }

}