using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.Abstractions.Interfaces;
using drz.Infrastructure;

namespace drz.PdfSharp_ConversionFactor
{
    internal class Engine
    {
        IConsoleService CS => new ConsoleService();

        public Engine(TemplateConversionFactor tmp)
        {
            FileDialogs FD = new FileDialogs();
            //запрашиваем файлы
            if (!FD.FilesDialogOpen())
            {
                CS.ConsoleMsg("Файлы PDF не выбраны",WConsoleColor.Green);
                return;
            }

           ConversionFactor Conversion=new ConversionFactor(tmp);

            string[] PdfFiles = FD.PdfFiles;
            foreach (string pdffile in PdfFiles)
            {
                Conversion.SetPagesConversionFactor(pdffile);
            }
        }
    }
}


