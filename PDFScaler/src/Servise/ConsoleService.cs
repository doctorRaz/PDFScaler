/*
*        Copyright doctorRAZ 2014-2025 by Разыграев Андрей
*
*        Licensed under the Apache License, Version 2.0 (the "License");
*        you may not use this file except in compliance with the License.
*        You may obtain a copy of the License at
*
*            http://www.apache.org/licenses/LICENSE-2.0
*
*        Unless required by applicable law or agreed to in writing, software
*        distributed under the License is distributed on an "AS IS" BASIS,
*        WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*        See the License for the specific language governing permissions and
*        limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using drz.PDFScaler.Enum;
using drz.PDFScaler.Infrastructure;
using drz.PDFScaler.Interfaces;
using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Interfaces;
using drz.PdfVpMod.Infrastructure;


namespace drz.PDFScaler.Servise
{
    /// <summary>
    /// Реализация сообщений в консоль
    /// </summary>
    /// <seealso cref="drz.PDFScaler.Interfaces.IConsoleService" />
    internal class ConsoleService :/* MessageService,*/ IConsoleService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleService"/> class.
        /// </summary>
        public ConsoleService()
        {
            //Console.BackgroundColor =ConsoleColor.Black;
            //Console.Clear();
            Console.Title = Title;
        }

        #region Console

#if NC || AC
        public void ConsoleWriteLine(string Message,
                                        WConsoleColor FColor = WConsoleColor.Default,
                                        WConsoleColor BColor = WConsoleColor.Default,
                                        string Title = null,
                                        [CallerMemberName] string CallerName = null)
        { 
         _title = Title;

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
        }
#else

        /// <summary>
        /// Consoles the write line.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="TypeMessage"></param>
        /// <param name="Title">The title.</param>
        /// <param name="CallerName">Name of the caller.</param>
        public void ConsoleWriteLine(string Message,
                            MesagType TypeMessage = MesagType.None,
                             string Title = null,
                             [CallerMemberName] string CallerName = null)
        {
            _title = Title;
            ColorFB CFB = new ColorFB(TypeMessage);
            Console.ForegroundColor = CFB.ColorF;
            Console.BackgroundColor = CFB.ColorB;
#if DEBUG
            Console.WriteLine($"{CallerName}: {Message}");
#else
            Console.WriteLine($"{Message}");
#endif

            Console.ResetColor();

        }

        /// <summary>
        /// Consoles the write.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="TypeMesag">The type mesag.</param>
        /// <param name="Title">The title.</param>
        /// <param name="CallerName">Name of the caller.</param>
        public void ConsoleWrite(string Message,
             MesagType TypeMesag = MesagType.None,
              string Title = null,
              [CallerMemberName] string CallerName = null)
        {
            _title = Title;
            ColorFB CFB = new ColorFB(TypeMesag);
            Console.ForegroundColor = CFB.ColorF;
            Console.BackgroundColor = CFB.ColorB;
#if DEBUG
            Console.Write($"{CallerName}: {Message}");
#else
            Console.Write($"{Message}");
#endif

            Console.ResetColor();
        }

        /// <summary>
        /// Prints the specified logger.
        /// </summary>
        /// <param name="Logger">The logger.</param>
        /// <param name="ExitConfirmation">if set to <c>true</c> [exit confirmation].</param>
        public void Print(List<ILogger> Logger, bool ExitConfirmation)
        {

            foreach (Logger logger in Logger.Cast<Logger>())
            {
                if (logger.MesagType == MesagType.Error)
                {
                    ExitConfirmation = true;//не закрывать консоль
                }
#if DEBUG
                ConsoleWriteLine($"{logger.DateTimeStamp}: {logger.CallerName} {logger.Messag}", logger.MesagType);
#else
                ConsoleWriteLine($"{logger.Messag}", logger.MesagType);
#endif
            }
            Logger.Clear();

            if (ExitConfirmation)
            {

                Console.Write("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
                Console.WriteLine("");
                return;
            }
            else
            {
                return;
            }
        }

#endif
        #endregion

        #region Convert Enum

        /// <summary>
        /// Converts the enum w to console.
        /// </summary>
        /// <param name="WColor">Color of the w.</param>
        /// <param name="fb">The fb.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Цвета
        /// </summary>
        class ColorFB
        {
            public ColorFB(MesagType Tmsg)
            {
                switch (Tmsg)
                {
                    case MesagType.None:
                        _colorF = Console.ForegroundColor;
                        _colorB = Console.BackgroundColor;
                        break;
                    case MesagType.Error:
                        _colorF = ConsoleColor.White;
                        _colorB = ConsoleColor.DarkRed;
                        break;
                    case MesagType.Warning:
                        _colorF = ConsoleColor.Red;
                        _colorB = Console.BackgroundColor;
                        break;
                    case MesagType.Info:
                        _colorF = ConsoleColor.DarkGray;
                        _colorB = Console.BackgroundColor;
                        break;
                    case MesagType.Ok:
                        _colorF = ConsoleColor.Green;
                        _colorB = Console.BackgroundColor;
                        break;
                    default: throw new InvalidEnumArgumentException();
                }
            }

            ConsoleColor _colorF;
            internal ConsoleColor ColorF => _colorF;

            ConsoleColor _colorB;
            internal ConsoleColor ColorB => _colorB;
        }

        string _title;

        string Title
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_title)) return DataSetWpfOpt.TitleAttribute + " " + DataSetWpfOpt.Version;
                else return _title;
            }
        }
    }

}
