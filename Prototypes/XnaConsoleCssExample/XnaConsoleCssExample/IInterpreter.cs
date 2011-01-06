//
// Xna Console
// www.codeplex.com/XnaConsole
// Copyright (c) 2008 Samuel Christie
//
using System;
using System.Collections.Generic;
using System.Text;

namespace XnaConsole
{
    /// <remarks>
    /// Describes an interface for interpreters to be used with XnaConsole
    /// </remarks>
    public interface IInterpreter
    {
        /// <returns>Returns the current prompt from the interpreter as a string</returns>
        string GetPrompt();
        /// <summary>
        /// Executes a string from the console
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Returns execution results, or error messages</returns>
        string Execute(string str);
        /// <summary>
        /// Adds a global variable to the interpreter environment
        /// </summary>
        /// <param name="str"></param>
        /// <param name="obj"></param>
        void AddGlobal(string str, object obj);
    }
}
