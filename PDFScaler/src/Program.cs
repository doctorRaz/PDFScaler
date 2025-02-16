using System;
using System.Collections.Generic;
using System.Linq;

using drz.PDFScaler.Interfaces;
using drz.PDFScaler.Servise;
 using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Interfaces;
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
        /// логер событий
        /// </summary>
        public static List<ILogger> Logger;

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

            //logger сообщения
            Logger = new List<ILogger>();

            //читаю файл конфигурации
            Config cfg = new Config(Logger);

            Setting Sets = cfg.Set;


            Repository GF = new Repository(Logger);

            if (GF.GetPDFfiles(args))//в ком строке что то есть
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


            //Manager 
            PdfManager PS = new PdfManager(Logger,
                                            Sets.Unit,
                                           Sets.Mode);

            do
            {
                if (GF.GetPDFfilesWin())
                {
                    PS.PdfRun(GF.PdfFiles,Sets.AddBak);
                }
                foreach (Logger logger in Logger.Cast<Logger>())
                {
                    if (logger.MesagType == MesagType.Error)
                    {
                        Sets.ExitConfirmation = true;//не закрывать консоль
                    }
#if DEBUG
                    CS.ConsoleWriteLine($"{logger.DateTimeStamp}: {logger.CallerName} {logger.Messag}", logger.MesagType);
#else
                    CS.ConsoleWriteLine($"{logger.Messag}", logger.MesagType);
#endif
                }
                if (!Sets.ExitConfirmation)
                {
                    return;
                }
                CS.ConsoleWrite(MessagWelcom.MesagReplase);
                response = Console.ReadKey(/*false*/).Key;
                if (response == ConsoleKey.Y)
                {
                    CS.ConsoleWriteLine("");
                    //logger
                    //Logger.Clear();
                    //Console.Clear();
                }
            } while (response == ConsoleKey.Y);

        }



    }


}
