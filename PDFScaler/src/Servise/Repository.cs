using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Interfaces;
using drz.PdfVpMod.Servise;

using PDFUnisci;

namespace drz.PDFScaler.Servise
{
    /// <summary>
    /// Сервис получения файлов
    /// </summary>
    internal class Repository
    {
        List<ILogger> Logger;

        Setting Sets;

        public Repository(List<ILogger> logger, Setting sets)
        {
            Logger = logger;
            Sets = sets;
        }

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
                ILogger logItem = new Logger("Файлы PDF не выбраны", MesagType.Info);
                Logger.Add(logItem);

                ConsoleFocus.FocusProcess(DataSetWpfOpt.AsmFileNameWithoutExtension);
                return new List<string>();
            }
        }

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
                        Logger.Add(new Logger($"Выбранный файл не является PDF и будет исключен. {argsL[i]}", MesagType.Warning));
                    }
                }
                else if (Directory.Exists(argsL[i]))//если директория
                {
                    IEnumerable<string> files = Directory.EnumerateFiles(argsL[i], "*.pdf");
                    pdfFiles.AddRange(files/*.ToList()*/);

                    //foreach (var item in Directory.EnumerateFiles(argsL[i], "*.pdf"))
                    //{
                    //    argsL.Add(item);
                    //}
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
                            Sets.Mode = ModeChangVp.Del;
                            break;

                        case "-add":
                            Sets.Mode = ModeChangVp.Add;
                            break;

                        case "-mod":
                            Sets.Mode = ModeChangVp.AddOrMod;
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
                            Logger.Add(new Logger($"Параметр аргумента не существует будет исключен. {argsL[i]}", MesagType.Error));
                            break;
                    }

                }
            }

            #endregion

            return pdfFiles;
        }

    }
}
