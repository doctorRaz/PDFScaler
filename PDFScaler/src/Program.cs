using System;
using System.Collections.Generic;
using System.Linq;

using drz.PDFScaler.Abstractions.Interfaces;
using drz.PDFScaler.Infrastructure;
using drz.PDFScaler.Servise;
using drz.PdfVpMod.Abstractions.Interfaces;
using drz.PdfVpMod.Enum;
using drz.PdfVpMod.PdfSharp;
using drz.PdfVpMod.Servise;


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
        /// <summary>
        /// логгер событий
        /// </summary>
        public static List<ILoger> Loger;

        [STAThread]
        static void Main(string[] args)
        {

            //загрузчик dll
            new ReflectionLoader();
            Prog(args);
        }
        static void Prog(string[] args)
        {
            ConsoleKey response;

            //цветная консоль
            IConsoleService CS = new ConsoleService();

            //loger сообщения
            Loger = new List<ILoger>();

            Repository GF = new Repository(Loger);

            if(GF.GetPDFfiles(args))//в ком строке что то есть
            {

            }
            else//ком строка пустая
            {

            }
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

            WinGraphicsUnit unit = WinGraphicsUnit.Millimeter;
            ModeChangVp mode = ModeChangVp.AddOrMod;

            //Manager 
            PdfManager PS = new PdfManager(Loger,
                                            unit,
                                            mode);
            
            do
            {
                if (GF.GetPDFfilesWin())
                {
                    PS.PdfRun(GF.PdfFiles);
                }
                foreach (Loger loger in Loger.Cast<Loger>())
                {
#if DEBUG
                    CS.ConsoleWriteLine($"{loger.DateTimeStamp}: {loger.CallerName} {loger.Messag}", loger.MesagType);
#else
                    CS.ConsoleWriteLine($"{loger.Messag}", loger.MesagType);
#endif
                }

                CS.ConsoleWrite(MessagWelcom.MesagReplase);
                response = Console.ReadKey(/*false*/).Key;
                if (response == ConsoleKey.Y)
                {
                    CS.ConsoleWriteLine("");
                    //loger
                    //Loger.Clear();
                    //Console.Clear();
                }
            } while (response == ConsoleKey.Y);

        }



    }


}
