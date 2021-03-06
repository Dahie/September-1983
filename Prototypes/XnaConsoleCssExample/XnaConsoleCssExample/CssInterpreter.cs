
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using CSScriptLibrary;

namespace XnaConsoleCssExample
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class CssInterpreter : DrawableGameComponent
    {
        const string Prompt = ">>> ";
        const string PromptCont = "... ";
        string multi;

        public XnaConsoleComponent console;

        /// <summary>
        /// Creates a new PythonInterpreter
        /// </summary>
        public CssInterpreter(Game1 game, SpriteFont font) : base((Game)game)
        {
            
            multi = "";

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
            return "SCRIPT EVALUATED";
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

        private void Evaluate( string input )
        {
            string boilerplate = @"
            
                using XnaConsoleCssExample;

                private static CssInterpreter hostEnvironment;

                private static void log(string message)
                {{
                    hostEnvironment.console.WriteLine(message);
                }}

                private static void run(string path)
                {{
                    hostEnvironment.LoadScript(path);
                }}

                public static void Evaluate(CssInterpreter callee)
                {{   
                    hostEnvironment = callee;                                   
                    {0}; // here goes the code that is to be evaluated.  
                }}";


            var script = new AsmHelper(CSScript.LoadMethod(string.Format(boilerplate, input)));
            script.Invoke("*.Evaluate", this); 
        }

        public void LoadScript(string path) 
        {
            // todo: load and run an external script. Exaple:
            
            /*
            var script = CSScript.Load("HelloScript.cs")
                     .CreateInstance("Script")
                     .AlignToInterface<IScript>();

            script.Hello("Hi there...");
            */
        }
    }

}