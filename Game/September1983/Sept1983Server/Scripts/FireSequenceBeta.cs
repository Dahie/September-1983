using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server
{
    class FireSequenceAlpha : IFireSequence
    {
        public void run(Map map)
        {
            //TODO: Implementierung der Abschusssequenz

            for (int i = 0; i < Map.allowedShots; i++ )
            {
                var randX = ProgramServer.RandomNumber(0, Map.Size);
                var randY = ProgramServer.RandomNumber(0, Map.Size);
                map.fireShot(randX, randY);
            }
        }
    }
}
