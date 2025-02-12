using System.Collections.Generic;
using System.Windows.Forms;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;

namespace drz.Servise
{
    /// <summary>
    /// Сервис получения файлов
    /// </summary>
    internal class GetFiles
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

        List<Logger> Logger;


        public GetFiles()
        {
            Logger = Program.Logger;
        }

        public  bool GetPDFfiles()
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
                //OFD.ShowReadOnly = true;
            };
            if (OFD.ShowDialog(new Form() { TopMost = true/*, TopLevel =true*/ }) == DialogResult.OK)
            {
                _pdfFiles = OFD.FileNames;
                ConsoleFocus.FocusProcess(DataSetWpfOpt.sAsmFileNameWithoutExtension);
                return true;
            }
            else
            {
                logItem = new Logger("Файлы PDF не выбраны", MesagType.Info);
                Logger.Add(logItem);

                ConsoleFocus.FocusProcess(DataSetWpfOpt.sAsmFileNameWithoutExtension);
                return false;
            }
        }

    }
}
