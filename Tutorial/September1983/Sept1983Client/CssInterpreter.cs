
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
        private const string PromptPre = ">>> ";
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
            Prompt();
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
                    Prompt();
                }
                else if (multi != "" && input == "") //execute the multiline code after block is finished
                {
                    string temp = multi; // make sure that multi is cleared, even if it returns an error
                    multi = "";
                    Evaluate(temp);
                    Prompt();
                }
                else // if (multi == "" && input != "") execute single line expressions or statements
                {
                    Evaluate(input);
                    Prompt();
                }
            }
            catch (Exception ex)
            {
                WriteLine("ERROR: " + ex.Message);
                Prompt();
            }
        }
        
        /// <summary>
        /// Evaluates an expression in Cs-Script
        /// </summary>
        /// <param name="input"></param>
        private void Evaluate( string input )
        {

        }

        public void LoadScript(string className) 
        {
            game.sendSequenceName(className);
        }

        public void WriteLine(string input)
        {
            console.WriteLine(input);
        }

        public void Prompt()
        {
            console.Prompt(PromptPre, Execute);
        }
    }

}