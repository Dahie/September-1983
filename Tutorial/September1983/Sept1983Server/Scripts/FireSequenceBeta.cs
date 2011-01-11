using System;
using System.Collections.Generic;
using System.Text;

using Sept1983Server;


class FireSequenceBeta : IFireSequence
{

    public static int RandomNumber(int seed, int min, int max)
    {
        Random random = new Random(DateTime.Now.Millisecond*(seed+1));
        return random.Next(min, max);
    }

    public void Launch(Map map)
    {
        // Implementierung der Abschusssequenz

        for (int i = 0; i < Map.allowedShots; i++)
        {
            int randX = RandomNumber(i, 0, map.GetSize());
            int randY = RandomNumber(i*2, 0, map.GetSize());
            map.fireShot(randX, randY);
        }
    }

}