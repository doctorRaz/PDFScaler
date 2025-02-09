using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using drz.Abstractions.Interfaces;
using drz.Infrastructure;

namespace drz.Servise
{
    internal static class MessagWelcom
    {
        public static string Header = "ВНИМАНИЕ ВАЖНО!";

        public static string[] Welcom = new string[] {
            "\tПрограмма изменяет содержимое файлов PDF",
            "\t\tБудут созданы резервные копии существующих файлов с расширением *.BAK",
            "\t\tВы используете программу на свой страх и риск!"
            
        };

        public static string Futer = "\t\tПродолжить \"Yes\", выход по любой клавише ";

        public static string Replase = "Хотите повторить \"Yes\", выход по любой клавише ";
    }
}
