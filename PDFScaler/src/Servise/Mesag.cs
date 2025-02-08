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
        IConsoleService CS => new ConsoleService();

        public Mesag()
        {
            //******* DisClaimer ****
            CS.ConsoleMsg("ВАЖНО!!!",
                               WConsoleColor.White,
                               WConsoleColor.DarkRed);
            CS.ConsoleMsg("Программа изменяет содержимое файлов PDF",
                               WConsoleColor.White,
                               WConsoleColor.DarkGreen);

            CS.ConsoleMsg("Будут созданы резервные копии существующих файлов с расширением *.BAK",
                   WConsoleColor.White,
                   WConsoleColor.DarkGreen);

            CS.ConsoleMsg("Вы используете программу на свой страх и рискF",
                   WConsoleColor.White,
                   WConsoleColor.DarkGreen);

            CS.ConsoleMsg("");
            CS.ConsoleMsg("Если Вы готовы продолжить...");
            CS.ConsoleMsg("Press any Key");
            Console.ReadKey();
        }
    }
}
