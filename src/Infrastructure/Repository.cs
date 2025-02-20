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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Infrastructure;
using drz.PdfVpMod.Interfaces;

namespace drz.PDFScaler.Infrastructure
{
    /// <summary>
    /// Сервис получения файлов
    /// </summary>
    internal class Repository
    {
        List<ILogger> Logger;

        Setting Sets;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="sets">The sets.</param>
        public Repository(List<ILogger> logger, Setting sets)
        {
            Logger = logger;
            Sets = sets;
        }

        /// <summary>
        /// Gets the pd ffiles win.
        /// </summary>
        /// <returns></returns>
        public List<string> GetPDFfilesWin()
        {
            OpenFileDialog OFD = new OpenFileDialog
            {
                Title = "Выбор файлов PDF",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "PDF",
                Filter = "Files PDF (*.PDF)|*.PDF",

                FilterIndex = 0,
                RestoreDirectory = true,
                Multiselect = true,
                ReadOnlyChecked = false,

            };
            if (OFD.ShowDialog(new Form() { TopMost = true/*, TopLevel =true*/ }) == DialogResult.OK)
            {
                List<string> pdfFiles = OFD.FileNames.ToList<string>();
                ConsoleFocus.FocusProcess(DataSetWpfOpt.AsmFileNameWithoutExtension);
                return pdfFiles;
            }
            else
            {
                ILogger logItem = new Logger("Файлы PDF не выбраны", MesagType.Idle);
                Logger.Add(logItem);

                ConsoleFocus.FocusProcess(DataSetWpfOpt.AsmFileNameWithoutExtension);
                return new List<string>();
            }
        }

        /// <summary>
        /// Gets the PDF files.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public List<string> GetPDFfiles(string[] args)
        {

            #region UINSI
            List<string> pdfFiles = new List<string>();

            List<string> argsL = args.ToList();

            for (int i = 0; i < argsL.Count; i++)
            {
                if (File.Exists(argsL[i]))//если файл
                {
                    if (Path.GetExtension(argsL[i]).ToLower() == ".pdf")
                    {
                        pdfFiles.Add(argsL[i]);
                    }
                    else
                    {
                        Logger.Add(new Logger($"Выбранный файл не является PDF и будет исключен. {argsL[i]}", MesagType.Idle));
                    }
                }
                else if (Directory.Exists(argsL[i]))//если директория
                {
                    IEnumerable<string> files = Directory.EnumerateFiles(argsL[i], "*.pdf",SearchOption.AllDirectories);
                    pdfFiles.AddRange(files/*.ToList()*/);
                }
                else//аргументы ком строки
                {

                    switch (argsL[i].ToLower())
                    {
                        case "-mm":
                            Sets.Unit = WinGraphicsUnit.Millimeter;
                            break;

                        case "-in":
                            Sets.Unit = WinGraphicsUnit.Inch;
                            break;

                        case "-pt":
                            Sets.Unit = WinGraphicsUnit.Point;
                            break;

                        case "-cm":
                            Sets.Unit = WinGraphicsUnit.Centimeter;
                            break;

                        case "-pr":
                            Sets.Unit = WinGraphicsUnit.Presentation;
                            break;

                        case "-del":
                            Sets.Mode = ModeChangVp.Delete;
                            break;

                        case "-add":
                            Sets.Mode = ModeChangVp.Add;
                            break;

                        case "-mod":
                            Sets.Mode = ModeChangVp.AddOrModify;
                            break;

                        case "-exon":
                            Sets.ExitConfirmation = true;
                            break;

                        case "-exoff":
                            Sets.ExitConfirmation = false;
                            break;

                        case "-bakon":
                            Sets.AddBak = true;
                            break;

                        case "-bakoff":
                            Sets.AddBak = false;
                            break;

                        default:
                            Logger.Add(new Logger($"Параметр аргумента не существует будет исключен. {argsL[i]}", MesagType.Idle));
                            break;
                    }

                }
            }

            #endregion

            return pdfFiles;
        }

    }
}
