using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sept1983Server
{
    class SequenceExecuter
    {

        protected Map map;

        public SequenceExecuter(Map map) {
            this.map = map;
        }

        /**
         * Lädt das CS-Script interpretiert als FireSequence und führt 
         * Sequence aus.
         * @returns String Nachricht
         */
        public String LoadScript(String pathToScript)
        {
            String msg = "response"; //Rückgabenachricht

            //TODO: Script laden und interpretieren
            FireSequence fireSequence = new FireSequence();

            msg += ExecuteFiringSequence(fireSequence);

            return msg;
        }

        private String ExecuteFiringSequence(FireSequence sequence) {
            String msg = "Shots fired: ";

            sequence.run(map);

            msg = map.FiredShotsResults();
            map.resetResults();

            return msg;
        }
    }
}
