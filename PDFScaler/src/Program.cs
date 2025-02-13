using System;
using System.Collections.Generic;

using drz.Abstractions.Interfaces;
using drz.Infrastructure;
using drz.Servise;

using drz.PdfSharp.Pdf;
using System.Linq;


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
        public static List<ILogger> Logger;

        [STAThread]
        static void Main(string[] args)
        {
            //загрузчик dll
            ReflectionLoader cmd = new ReflectionLoader();
            Prog(args);
        }
        static void Prog(string[] args)
        {
            ConsoleKey response;

            //цветная консоль
            IConsoleService CS = new ConsoleService();

            //logger сообщения
            Logger = new List<ILogger>();

            //todo в отдельный класс
            #region UINSI
            /*
            List<string> argsL = args.ToList();
            List<string> Files = new List<string>();

            for (int i = 0; i < argsL.Count; i++)
            {
                if (File.Exists(argsL[i]))
                {
                    if (Path.GetExtension(argsL[i]).ToLower() == ".pdf")
                    {
                        Files.Add(argsL[i]);
                    }
                    else if (Path.GetExtension(argsL[i]).ToLower() == ".png" || Path.GetExtension(argsL[i]).ToLower() == ".jpg" || Path.GetExtension(argsL[i]).ToLower() == ".jpeg")
                    {
                        Images.Add(argsL[i]);
                    }
                    else
                    {
                        LogHelper.Log($"The selected file is not a PDF or valid immage format (.png | .jpg | .jpeg), and will be excluded. {argsL[i]}", LogType.Warning);
                    }
                }
                else if (Directory.Exists(argsL[i]))
                {
                    foreach (var item in Directory.EnumerateFiles(argsL[i]))
                    {
                        argsL.Add(item);
                    }
                }
                else
                {
                    switch (argsL[i].ToLower())
                    {
                        case "-np":

                            if (argsL.Count() > i + 1)
                            {
                                createNewPageFormat = argsL[i + 1];
                                i++;
                            }
                            else
                            {
                                createNewPageFormat = "A4";
                            }

                            Config.ExitConfirmation = 0;

                            break;

                        case "-o":
                            autoOpenFile = true;
                            break;

                        case "-b":
                            PDFInterface.Bookmarks = 1;
                            break;

                        case "-s":
                            splitAll = true;
                            break;

                        case "-flat":
                            flat = true;
                            break;

                        case "-singlepagesplit":
                            if (argsL.Count() >= i + 1)
                            {
                                bool r = Int32.TryParse(argsL[i + 1].ToLower(), out singlePageSplit);
                                if (!r)
                                {
                                    singlePageSplit = 0;
                                    break;
                                }
                            }
                            i++;
                            break;

                        default:
                            LogHelper.Log($"The argument option does not exist will be excluded. {argsL[i]}", LogType.Error);
                            break;
                    }
                }
            }

            */
            #endregion

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
            GetFiles GF = new GetFiles();
            do
            {
                if (GF.GetPDFfiles())
                {
                    PS.PdfRun(GF.PdfFiles);
                }
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
                    Logger = new List<ILogger>();
                    //Console.Clear();
                }
            } while (response == ConsoleKey.Y);

        }



    }


}
