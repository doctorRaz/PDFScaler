/*
*        Copyright doctorRAZ 2014-2025 by Разыграев Андрей
*
*        Licensed under the Apache License, Version 2.0 (the "License");
*        you may not use this file except in compliance with the License.
*        You may obtain a copy of the License at
*
*            http://www.apache.org/licenses/LICENSE-2.0
*
*        Unless required by applicable law or agreed to in writing, software
*        distributed under the License is distributed on an "AS IS" BASIS,
*        WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*        See the License for the specific language governing permissions and
*        limitations under the License.
*/

using System;
using System.Collections.Generic;

using drz.PDFScaler.Infrastructure;
using drz.PDFScaler.Interfaces;
using drz.PDFScaler.Servise;
using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Infrastructure;
using drz.PdfVpMod.Interfaces;
using drz.PdfVpMod.PdfSharp;
using System.IO;
using System.Text;




#if NET
using System.Reflection;
[assembly: AssemblyInformationalVersion("PDFScaler for PDF")]
#endif

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

#if NET
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif

            IConsoleService CS = new ConsoleService();//цветная консоль

            Logger = new List<ILogger>(); //logger сообщения

            Config cfg = new Config(Logger); //читаю конфигурацию

            Setting Sets = cfg.Set;//настройки

            Repository GF = new Repository(Logger, Sets);

            ConsoleKey response = new ConsoleKey();
            // "d:\@Developers\В работе\!Текущее\Programmers\!NET\GitHubMy\PDFScaler\temp" -mod

            //"D:/@Developers/В работе/!Текущее/Programmers/!NET/GitHubMy/PDFScaler/temp/test" "D:/@Developers/В работе/!Текущее/Programmers/!NET/GitHubMy/PDFScaler/temp/NOT_DESIGNATION.pdf" -bakoN -mm -add -exon

            List<string> filesPdf = GF.GetPDFfiles(args);//пытаемся получить из ком строки список файлов и параметры ком строки

            //настройки загружены из default/xml и/или ком строки
            //Manager 
            PdfManager PS = new PdfManager(Logger,
                                            Sets);

            if (filesPdf.Count > 0)//файлы есть
            {
                CS.Print(Logger, false);//что было

                PS.PdfRun(filesPdf);//обработка

                CS.Print(Logger, Sets.ExitConfirmation);//что будет
                return;
            }

            else//файлов нет
            {
                if (UiMenu.Create())
                {
                    CS.Print(Logger, false);//выводим если что есть, только War,Err

                    //предлжение открыть файлы
                    CS.ConsoleWriteLine("Текущие настройки приложения...", MesagType.Ok);
                    CS.ConsoleWriteLine($"\tРежим изменения:\t{Sets.Mode.ToString()}", MesagType.Info);
                    CS.ConsoleWriteLine($"\tЕдиницы:\t\t{Sets.Unit.ToString()}", MesagType.Info);
                    CS.ConsoleWriteLine($"\tРезервная копия:\t{Sets.AddBak.ToString()}", MesagType.Info);
                    CS.ConsoleWriteLine($"\tЗапрос на выход:\t{Sets.ExitConfirmation.ToString()}", MesagType.Info);
                    Console.WriteLine("");
                    CS.ConsoleWrite($"Открыть диалог выбора файлов?\n\t[Y]-да, любая клавиша выход: ", MesagType.Ok);
                    response = Console.ReadKey().Key;
                    Console.WriteLine("");
                    if (response != ConsoleKey.Y)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            filesPdf = GF.GetPDFfilesWin();//пытаемся получить файлы из диалога
            if (filesPdf.Count > 0)
            {
                PS.PdfRun(filesPdf);
            }
            CS.Print(Logger, Sets.ExitConfirmation);

        }

    }

}
