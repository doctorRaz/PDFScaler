using System.Collections.Generic;
using System.Runtime.CompilerServices;

using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Interfaces;


namespace drz.PDFScaler.Interfaces
{
    /// <summary>
    /// Интерфейс сообщений консоли
    /// </summary>
    partial interface IConsoleService
    {
        ///// <summary>
        ///// Вывод сообщения в консоль, принудительная раскраска 
        ///// </summary>
        ///// <param name="Message">Выводимое сообщение</param>
        ///// <param name="FColor">Color of the foreground</param>
        ///// <param name="BColor">Color of the background</param>
        ///// <param name="Title">The title.</param>
        ///// <param name="CallerName">Вызывающий метод. При использовании обязательно использование <code>[CallerMemberName]</code></param>
        //void ConsoleWriteLineFB(string Message,
        //                         WConsoleColor FColor = WConsoleColor.Default,
        //                         WConsoleColor BColor = WConsoleColor.Default,
        //                         string Title = null,
        //                         [CallerMemberName] string CallerName = null);


        /// <summary>
        /// Consoles the write line.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="TypeMesag">The type mesag.</param>
        /// <param name="Title">The title.</param>
        /// <param name="CallerName">Name of the caller.</param>
        void ConsoleWriteLine(string Message,
                            MesagType TypeMesag = MesagType.None,
                             string Title = null,
                             [CallerMemberName] string CallerName = null);

        /// <summary>
        /// Consoles the write.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="TypeMesag">The type mesag.</param>
        /// <param name="Title">The title.</param>
        /// <param name="CallerName">Name of the caller.</param>
        void ConsoleWrite(string Message,
                    MesagType TypeMesag = MesagType.None,
                     string Title = null,
                     [CallerMemberName] string CallerName = null);

        /// <summary>
        /// Prints the specified logger.
        /// </summary>
        /// <param name="Logger">The logger.</param>
        /// <param name="ExitConfirmation">if set to <c>true</c> [exit confirmation].</param>
        void Print(List<ILogger> Logger, bool ExitConfirmation);
    }


}
