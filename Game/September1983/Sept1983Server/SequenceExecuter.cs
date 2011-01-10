using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSScriptLibrary;

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
        public String LoadScript(String className)
        {
            String msg = "response"; //Rückgabenachricht

            //TODO: Script laden und interpretieren
            var scriptAssembly = CSScript.Load("./Scripts/" + className + ".cs");
            AsmHelper assemblyHelper = new AsmHelper(scriptAssembly);

            var fireSequence = (Scripts.IFireSequence)assemblyHelper.CreateObject(className);

            msg += ExecuteFiringSequence(fireSequence);

            return msg;
        }

        private String ExecuteFiringSequence(Scripts.IFireSequence sequence) {
            String msg = "Shots fired: ";

            sequence.Launch(map);

            msg = map.FiredShotsResults();
            map.resetResults();

            return msg;
        }
    }
}
