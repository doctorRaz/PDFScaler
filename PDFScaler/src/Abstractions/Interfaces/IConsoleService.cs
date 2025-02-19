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
