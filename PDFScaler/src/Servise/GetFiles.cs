using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;
using drz.Servise;
using drz.Win;

namespace drz.Servise
{
    internal class GetFiles
    {
        string[] _pdfFiles;
        public string[] PdfFiles => _pdfFiles;

        public static string[] GetPDFfiles()
        {
            //получаем файлы для обработки
            FileDialogs FD = new FileDialogs();
            if (!FD.FilesDialogOpen())
            {
                Logger logItem = new Logger("Файлы PDF не выбраны", MesagType.Info);
                Program.Logger.Add(logItem);
                return new string[0];

            }
            // добавлятор VP
            return FD.PdfFiles;


        }

        public bool FilesDialogOpen()
        {
            OpenFileDialog OFD = new OpenFileDialog
            {
                Title = "Выбор файлов PDF",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "PDF",
                Filter = "Files PDF (*.PDF)|*.PDF",

                FilterIndex = 2,
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
                ConsoleFocus.FocusProcess(DataSetWpfOpt.sAsmFileNameWithoutExtension);
                return false;
            }
        }

    }
}
