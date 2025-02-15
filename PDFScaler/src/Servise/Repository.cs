using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using drz.PdfVpMod.Abstractions.Interfaces;
using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Servise;

namespace drz.PDFScaler.Servise
{
    /// <summary>
    /// Сервис получения файлов
    /// </summary>
    internal class Repository
    {
        string[] _pdfFiles;

        /// <summary>
        /// Gets the PDF files.
        /// </summary>
        /// <value>
        /// The PDF files.
        /// </value>
        public string[] PdfFiles => _pdfFiles;

         

        Logger logItem;

        List<ILogger> Logger;


        public Repository(List<ILogger> logger)
        {
            Logger = logger;
        }

        public bool GetPDFfilesWin()
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
                _pdfFiles = OFD.FileNames;
                ConsoleFocus.FocusProcess(DataSetWpfOpt.AsmFileNameWithoutExtension);
                return true;
            }
            else
            {
                logItem = new Logger("Файлы PDF не выбраны", MesagType.Info);
                Logger.Add(logItem);

                ConsoleFocus.FocusProcess(DataSetWpfOpt.AsmFileNameWithoutExtension);
                return false;
            }
        }

        public bool GetPDFfiles(string[] args)
        {
            //todo в отдельный класс
            /*
            #region UINSI
         
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

            
            #endregion
            */
            return true;
        }

    }
}
