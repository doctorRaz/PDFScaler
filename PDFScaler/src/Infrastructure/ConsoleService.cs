using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using drz.Abstractions.Interfaces;
using drz.PdfSharp_ConversionFactor;

using Microsoft.VisualBasic;

using MessageBox = System.Windows.MessageBox;

#if NC
using HostMgd.ApplicationServices;
using HostMgd.EditorInput;
using Application = HostMgd.ApplicationServices.Application;
#endif

namespace drz.Infrastructure
{
    internal class ConsoleService : MessageService, IConsoleService
    {
        public ConsoleService()
        {
            //Console.BackgroundColor =ConsoleColor.Black;
            //Console.Clear();
            Console.Title=Title;
        }

        #region Console
        public void ConsoleWriteLine(string Message,
                                        WConsoleColor FColor = WConsoleColor.Default,
                                        WConsoleColor BColor = WConsoleColor.Default,
                                        string Title = null,
                                        [CallerMemberName] string CallerName = null)
        {
            _title = Title;
#if NC || AC 
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
            {
                InfoMessage(Message, CallerName);
                return;
            }
            Editor ed = doc.Editor;
#if DEBUG
            ed.WriteMessage($"\n----------------\n{CallerName}:\n----------------\n{Message}");
#else
            ed.WriteMessage($"\n{Message}");
#endif
#else

#if DEBUG 
            Console.ForegroundColor = ConvertEnumWToConsole(FColor, FB.Foreground);
            Console.BackgroundColor = ConvertEnumWToConsole(BColor, FB.Bacground);
            Console.WriteLine($"{CallerName}: {Message}");
            Console.ResetColor();
#else
            Console.ForegroundColor = ConvertEnumWToConsole(FColor, FB.Foreground);
            Console.BackgroundColor = ConvertEnumWToConsole(BColor, FB.Bacground);
            Console.WriteLine($"{Message}");
            Console.ResetColor();
#endif
#endif
        }
        #endregion

        #region Enum
        enum FB
        {
            Foreground,
            Bacground
        }

        #endregion



        #region Convert Enum

        /// <summary>
        /// Converts the enum WindowResult to MessageBoxResult.
        /// </summary>
        /// <param name="WColor">WindowResult</param>
        /// <returns>MessageBoxResult</returns>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException"></exception>
        ConsoleColor ConvertEnumWToConsole(WConsoleColor WColor, FB fb)
        {
            switch (WColor)
            {

                case WConsoleColor.Black: return ConsoleColor.Black;
                case WConsoleColor.DarkBlue: return ConsoleColor.DarkBlue;
                case WConsoleColor.DarkGreen: return ConsoleColor.DarkGreen;
                case WConsoleColor.DarkCyan: return ConsoleColor.DarkCyan;
                case WConsoleColor.DarkRed: return ConsoleColor.DarkRed;
                case WConsoleColor.DarkMagenta: return ConsoleColor.DarkMagenta;
                case WConsoleColor.DarkYellow: return ConsoleColor.DarkYellow;
                case WConsoleColor.Gray: return ConsoleColor.Gray;
                case WConsoleColor.DarkGray: return ConsoleColor.DarkGray;
                case WConsoleColor.Blue: return ConsoleColor.Blue;
                case WConsoleColor.Green: return ConsoleColor.Green;
                case WConsoleColor.Cyan: return ConsoleColor.Cyan;
                case WConsoleColor.Red: return ConsoleColor.Red;
                case WConsoleColor.Magenta: return ConsoleColor.Magenta;
                case WConsoleColor.Yellow: return ConsoleColor.Yellow;
                case WConsoleColor.White: return ConsoleColor.White;
                case WConsoleColor.Default:
                    if (fb == FB.Foreground)
                    {
                        return Console.ForegroundColor;
                    }
                    else
                    {
                        return Console.BackgroundColor;

                    }

                default: throw new InvalidEnumArgumentException();
            }
        }
        #endregion

        string _title;

        string Title
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_title)) return DataSetWpfOpt.sTitleAttribute + " " + DataSetWpfOpt.sVersion;
                else return _title;
            }
        }
    }

}
