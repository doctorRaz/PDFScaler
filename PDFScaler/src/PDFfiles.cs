using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace drz.PdfSharp_ConversionFactor
{
    /// <summary>
    /// Получаем список PDF файлов
    /// </summary>
    internal class PDFfiles
    {
        List<string> _pdfFiles;

        public List<string> PdfFiles=>_pdfFiles;

        public PDFfiles()
        {
            //var dd = Setting.ForegroundColorOld;
        }
    }
}
