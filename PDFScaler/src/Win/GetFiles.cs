using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;
using drz.Servise;
using drz.Win;

namespace drz.Win
{
    internal class GetFiles
    {
        public static string[] GetPDFfiles()
        {
            //получаем файлы для обработки
            FileDialogs FD = new FileDialogs();
            if (!FD.FilesDialogOpen())
            {
                Logger logItem = new Logger("Файлы PDF не выбраны", MesagType.Info);
                Program.Logger.Add(logItem);
                return new string[0];

            }
            // добавлятор VP
            return FD.PdfFiles;


        }

    }
}
