using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drz.PdfSharp_ConversionFactor
{
    internal class Setting
    {
        static ConsoleColor _ForegroundColorOld;
        static ConsoleColor _BackgroundColorOld;


        public static ConsoleColor ForegroundColorOld => _ForegroundColorOld;
        public static ConsoleColor BackgroundColorOld => _BackgroundColorOld;

        static Setting()
        {
            _ForegroundColorOld = Console.ForegroundColor;
            _BackgroundColorOld = Console.BackgroundColor;
        }


    }
}
