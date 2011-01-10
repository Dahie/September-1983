
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using CSScriptLibrary;
using XnaConsole;

namespace Sept1983Client
{
    /// <remarks>
    /// This class implements a basic console interpreter using the C#-Scripting-Engine (Cs-Script)
    /// </remarks>
    public class CssInterpreter : DrawableGameComponent
    {
        private const string Prompt = ">>> ";
        private const string PromptCont = "... ";
        private string multi;
        private GameClient game;

        private XnaConsoleComponent console;

        /// <summary>
        /// Creates a new CssInterpreter
        /// </summary>
        public CssInterpreter(GameClient parentGame, SpriteFont font) : base((GameClient)parentGame)
        {            
            multi = "";
            game = parentGame;
            console = new XnaConsoleComponent(game, font);
            game.Components.Add(console);
            console.Prompt(Prompt, Execute);
        }

        /// <summary>
        /// Get string output from IronPythons MemoryStream standard out
        /// </summary>
        /// <returns></returns>
        private string getOutput()
        {
            //todo: do we need this for anything?
            // daniel: no, maybe just for jokingly saying, that the command was registered and is now executed
            return "SCRIPT EVALUATED AND VALID, THE RED BUTTON WILL BE PUSHED";
        }

        /// <summary>
        /// Executes CS-Script commands from the console.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Returns the execution results or error messages.</returns>
        public void Execute(string input)
        {
            try
            {             
                if ((input != "") && ((input[input.Length - 1].ToString() != ";") || (multi != ""))) //multiline block incomplete, ask for more
                {
                    multi += input + "\n";
                    console.Prompt(Prompt, Execute);
                }
                else if (multi != "" && input == "") //execute the multiline code after block is finished
                {
                    string temp = multi; // make sure that multi is cleared, even if it returns an error
                    multi = "";
                    Evaluate(temp);
                    console.WriteLine(getOutput());
                    console.Prompt(Prompt, Execute);
                }
                else // if (multi == "" && input != "") execute single line expressions or statements
                {
                    Evaluate(input);
                    console.WriteLine(console.Chomp(getOutput()));
                    console.Prompt(Prompt, Execute);
                }
            }
            catch (Exception ex)
            {
                console.WriteLine("ERROR: " + ex.Message);
                console.Prompt(Prompt, Execute);
            }
        }
        
        /// <summary>
        /// Evaluates an expression in Cs-Script
        /// </summary>
        /// <param name="input"></param>
        private void Evaluate( string input )
        {
            string boilerplate = @"
            
                using Sept1983Client;

                private static CssInterpreter hostEnvironment;

                private static void log(string message)
                {{
                    hostEnvironment.WriteLine(message);
                }}

                private static void run(string className)
                {{
                    hostEnvironment.LoadScript(className);
                }}

                public static void Evaluate(CssInterpreter callee)
                {{   
                    hostEnvironment = callee;                                   
                    {0}; // here goes the code that is to be evaluated.  
                }}";


            var script = new AsmHelper(CSScript.LoadMethod(string.Format(boilerplate, input)));
            script.Invoke("*.Evaluate", this); 
        }

        public void LoadScript(string className) 
        {
            game.sendSequenceName(className);
        }

        public void WriteLine(string input)
        {
            Console.WriteLine(input);
            console.WriteLine(input);
        }
    }

}