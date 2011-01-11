using System;
using System.Collections.Generic;
using System.Text;

using Sept1983Server;


class FireSequenceGamma : IFireSequence
{

    public static int RandomNumber(int seed, int min, int max)
    {
        Random random = new Random(DateTime.Now.Millisecond*(seed+1));
        return random.Next(min, max);
    }

    public void Launch(Map map)
    {
        // Implementierung der Abschusssequenz

        int randX = RandomNumber(1, 1, map.GetSize()-1);
        int randY = RandomNumber(1*2, 1, map.GetSize()-1);

        for (int i = 0; i < Map.allowedShots; i++)
        {
            map.fireShot(randX, randY);
            map.fireShot(randX-1, randY);
            map.fireShot(randX, randY-1);
            map.fireShot(randX, randY+1);
            map.fireShot(randX+1, randY);
        }
    }

}