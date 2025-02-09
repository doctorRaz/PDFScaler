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
            using (OpenFileDialog OFD = new OpenFileDialog())
            {
                OFD.Title = "Select PDF Files";
                OFD.CheckFileExists = true;
                OFD.CheckPathExists = true;
                OFD.DefaultExt = "PDF";
                OFD.Filter = "Files PDF (*.PDF)|*.PDF";

                OFD.FilterIndex = 2;
                OFD.RestoreDirectory = true;
                OFD.Multiselect = true;
                OFD.ReadOnlyChecked = false;
                //OFD.ShowReadOnly = true;

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
}
