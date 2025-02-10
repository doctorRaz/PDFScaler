using System;
using System.Windows.Forms;

using Microsoft.WindowsAPICodePack.Dialogs;

namespace drz.Win
{
    internal class FileDialogs
    {
        string _folderPathc;
        public string FolderPathc => _folderPathc;

        string[] _pdfFiles;

        public string[] PdfFiles => _pdfFiles;

        public bool ComonFolderDialogOpen()
        {
            CommonOpenFileDialog CFSD = new CommonOpenFileDialog
            {
                Title = "",
                DefaultExtension = "pdf",
                IsFolderPicker = true,
            };

            if (CFSD.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        public bool FolderDialogOpen()
        {
            COFD fsd = new COFD//save
            {
                Title = "Select the folder with the PDF files",
                Multiselect = false,
                IsFolderPicker = true,
            };
            if (fsd.ShowDialog())
            {
                _folderPathc = fsd.FileName;

                return true;
            }
            else
            {
                _folderPathc = string.Empty;
                return false;
            }
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
             
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

