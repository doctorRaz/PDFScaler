using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using drz.PdfSharp_ConversionFactor;

using Microsoft.WindowsAPICodePack.Dialogs;

namespace drz.PdfSharp_ConversionFactor
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
                Title="",
                DefaultExtension="pdf",
                IsFolderPicker = true,
            };
       
            if (CFSD.ShowDialog() ==   CommonFileDialogResult.Ok)
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
                Title = "Select PDF Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "pdf",
                Filter = "pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true,
                Multiselect = true,

                ReadOnlyChecked = false,
                ShowReadOnly = true
            };
            if (OFD.ShowDialog() == DialogResult.OK)
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
