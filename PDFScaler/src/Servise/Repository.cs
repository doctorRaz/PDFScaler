using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using drz.PdfVpMod.Abstractions.Interfaces;
using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Servise;

using PDFUnisci;

namespace drz.PDFScaler.Servise
{
    /// <summary>
    /// Сервис получения файлов
    /// </summary>
    internal class Repository
    {
        List<string> _pdfFiles;

        /// <summary>
        /// Gets the PDF files.
        /// </summary>
        /// <value>
        /// The PDF files.
        /// </value>
        public List<string> PdfFiles => _pdfFiles;


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
                _pdfFiles = OFD.FileNames.ToList<string>();
                ConsoleFocus.FocusProcess(DataSetWpfOpt.AsmFileNameWithoutExtension);
                return true;
            }
            else
            {
                ILogger logItem = new Logger("Файлы PDF не выбраны", MesagType.Info);
                Logger.Add(logItem);

                ConsoleFocus.FocusProcess(DataSetWpfOpt.AsmFileNameWithoutExtension);
                return false;
            }
        }

        public bool GetPDFfiles(string[] args)
        {
            //todo в отдельный класс

            #region UINSI

            Uinsi.Uinsifiles(args);

            #endregion

            return true;
        }

    }
}
