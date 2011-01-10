using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server
{
    class FireSequence : Sept1983Server.Scripts.IFireSequence
    {
        public void Launch(Map map)
        {
            //TODO: Implementierung der Abschusssequenz

            map.fireShot(1, 1);

        }
    }
}
