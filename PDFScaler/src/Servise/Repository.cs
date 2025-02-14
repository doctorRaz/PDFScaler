using System.Collections.Generic;
using System.Windows.Forms;

using drz.Abstractions.Interfaces;
using drz.Enum;
using drz.PDFScaler;

namespace drz.Servise
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


        Loger logItem;

        List<ILoger> Loger;


        public Repository()
        {
            Loger = Program.Loger;
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

            };
            if (OFD.ShowDialog(new Form() { TopMost = true/*, TopLevel =true*/ }) == DialogResult.OK)
            {
                _pdfFiles = OFD.FileNames;
                ConsoleFocus.FocusProcess(DataSetWpfOpt.AsmFileNameWithoutExtension);
                return true;
            }
            else
            {
                logItem = new Loger("Файлы PDF не выбраны", MesagType.Info);
                Loger.Add(logItem);

                ConsoleFocus.FocusProcess(DataSetWpfOpt.AsmFileNameWithoutExtension);
                return false;
            }
        }

    }
}
