using System;
using System.Collections.Generic;

using drz.Abstractions.Interfaces;
using drz.Infrastructure;
using drz.Servise;

using drz.PdfSharp.Pdf;


namespace drz.PDFScaler
{
    /*PDF documents, such as those created by CAD software, may contain graphics that are intended to represent
     * real-world objects. Users of such documents often require information about the scale and units of
     * measurement of the corresponding real-world objects and their relationship to units in PDF user space. */

    /*Документы PDF, например, созданные с помощью программного обеспечения CAD, могут содержать графические
     * изображения, предназначенные для представления объектов реального мира. Пользователям таких документов 
     * часто требуется информация о масштабе и единицах измерения соответствующих объектов реального 
     * мира и их соотношении с единицами измерения в пользовательском пространстве PDF.*/

    internal class Program
    {
        public static List<Logger> Logger;

        [STAThread]
        static void Main(string[] args)
        {
            //загрузчик dll
            ReflectionLoader cmd = new ReflectionLoader();

            ConsoleKey response;

            //цветная консоль
            IConsoleService CS = new ConsoleService();

            //logger сообщения
            Logger = new List<Logger>();







            //приветственные сообщения
            CS.ConsoleWrite(MessagWelcom.Header, MesagType.Warning);
            foreach (string item in MessagWelcom.Welcom)
            {
                CS.ConsoleWriteLine(item, MesagType.Ok);
            }
            //предложение продолжить
            CS.ConsoleWrite(MessagWelcom.MesagStart, MesagType.Ok);

            response = Console.ReadKey(/*false*/).Key;
            CS.ConsoleWriteLine("");
            if (response != ConsoleKey.Y)//юзер отказался
            {
                return;
            }

            //движок PdfScaler
            PdfScaler PS = new PdfScaler(WinGraphicsUnit.Millimeter);

            do
            {
                string[] pdfFiles = GetFiles.GetPDFfiles();
                if (pdfFiles.Length > 0)
                {
                    PS.PdfRun(pdfFiles);

                }
                //if (!PS.PdfRun()) //движок
                //{
                foreach (Logger logger in Logger)
                {
                    CS.ConsoleWriteLine(logger.Messag, logger.MesagType);
                }
                //}

                CS.ConsoleWrite(MessagWelcom.MesagReplase);
                response = Console.ReadKey(/*false*/).Key;
                if (response == ConsoleKey.Y)
                {
                    CS.ConsoleWriteLine("");
                    //logger
                    Logger = new List<Logger>();
                    //Console.Clear();
                }
            } while (response == ConsoleKey.Y);

        }



    }


}
