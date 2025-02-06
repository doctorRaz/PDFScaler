using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.PdfSharp_ConversionFactor;

namespace drz.src
{
    internal class Dialogs
    {
        string _folderPathc;
        public string FolderPathc => _folderPathc;
        public void OFD()
        {
            COFD fsd = new COFD//save
            {
                Title = "Выберите каталог с файлами PDF",
                Multiselect = false,
                IsFolderPicker = true,
            };
            if (fsd.ShowDialog())
            {
                _folderPathc = fsd.FileName;
            }
            _folderPathc = string.Empty;
        }

    }
}
