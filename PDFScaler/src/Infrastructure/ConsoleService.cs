using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using drz.Abstractions.Interfaces;
using drz.Servise;



namespace drz.Infrastructure
{
    internal class ConsoleService :/* MessageService,*/ IConsoleService
    {
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

        public void ConsoleWriteLineFB(string Message,
                                        WConsoleColor FColor = WConsoleColor.Default,
                                        WConsoleColor BColor = WConsoleColor.Default,
                                        string Title = null,
                                        [CallerMemberName] string CallerName = null)
        {
            _title = Title;


            Console.ForegroundColor = ConvertEnumWToConsole(FColor, FB.Foreground);
            Console.BackgroundColor = ConvertEnumWToConsole(BColor, FB.Bacground);

#if DEBUG
            Console.WriteLine($"{CallerName}: {Message}");
#else
            Console.WriteLine($"{Message}"); 
#endif
            Console.ResetColor();

        }

        public void ConsoleWriteLine(string Message,
                            MesagType TypeMesag = MesagType.None,
                             string Title = null,
                             [CallerMemberName] string CallerName = null)
        {
            _title = Title;
            ColorFB CFB = new ColorFB(TypeMesag);
            Console.ForegroundColor = CFB.ColorF;
            Console.BackgroundColor = CFB.ColorB;
#if DEBUG
            Console.WriteLine($"{CallerName}: {Message}");
#else
            Console.WriteLine($"{Message}");
#endif

            Console.ResetColor();

        }

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



#endif
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
                        _colorF = ConsoleColor.DarkRed;
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
                if (string.IsNullOrWhiteSpace(_title)) return DataSetWpfOpt.sTitleAttribute + " " + DataSetWpfOpt.sVersion;
                else return _title;
            }
        }
    }

}
