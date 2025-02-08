using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.Abstractions.Interfaces;
using drz.Infrastructure;

namespace drz.Servise
{
    internal class Mesag
    {        
        public Mesag(IConsoleService CS)
        {
            //******* DisClaimer ****
            CS.ConsoleWriteLine("ВАЖНО!!!",
                               WConsoleColor.White,
                               WConsoleColor.DarkRed);
            CS.ConsoleWriteLine("Программа изменяет содержимое файлов PDF",
                               WConsoleColor.White,
                               WConsoleColor.DarkGreen);

            CS.ConsoleWriteLine("Будут созданы резервные копии существующих файлов с расширением *.BAK",
                   WConsoleColor.White,
                   WConsoleColor.DarkGreen);

            CS.ConsoleWriteLine("Вы используете программу на свой страх и рискF",
                   WConsoleColor.White,
                   WConsoleColor.DarkGreen);

            CS.ConsoleWriteLine("");
            CS.ConsoleWriteLine("Если Вы готовы продолжить...");
            CS.ConsoleWriteLine("Press any Key");
            Console.ReadKey();
        }
    }
}
