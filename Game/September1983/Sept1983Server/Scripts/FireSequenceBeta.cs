using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server.Scripts
{
    class FireSequenceBeta : Sept1983Server.Scripts.IFireSequence
    {
        public void Launch(Map map)
        {
            //TODO: Implementierung der Abschusssequenz

            for (int i = 0; i < Map.allowedShots; i++ )
            {
                var randX = ProgramServer.RandomNumber(0, map.GetSize() );
                var randY = ProgramServer.RandomNumber(0, map.GetSize() );
                map.fireShot(randX, randY);
            }
        }
    }
}
