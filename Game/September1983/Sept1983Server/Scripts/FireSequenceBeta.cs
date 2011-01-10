using System;
using System.Collections.Generic;
using System.Text;

using Sept1983Server;


class FireSequenceBeta : IFireSequence
{
    public void Launch(Map map)
    {
        //TODO: Implementierung der Abschusssequenz

        for (int i = 0; i < Map.allowedShots; i++)
        {
            var randX = ProgramServer.RandomNumber(0, map.GetSize());
            var randY = ProgramServer.RandomNumber(0, map.GetSize());
            map.fireShot(randX, randY);
        }
    }

}