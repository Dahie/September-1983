using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server.Script
{
    class FireSequenceAlpha : Sept1983Server.Scripts.IFireSequence
    {
        public void Launch(Map map)
        {
            //TODO: Implementierung der Abschusssequenz

            map.fireShot(0, 0);
            map.fireShot(2, 2);
            map.fireShot(0, 2);
            map.fireShot(4, 2);
            map.fireShot(0, 4);
        }
    }
}
