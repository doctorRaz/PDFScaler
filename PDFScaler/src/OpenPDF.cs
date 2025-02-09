using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.Abstractions.Interfaces;
using drz.PdfSharp_ConversionFactor;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;



namespace drz.Servise
{
    /// <summary>
    /// Открываем PDF
    /// </summary>
    internal class OpenPDF
    {
        PdfDocument _pdfDoc;
        public PdfDocument PdfDoc => _pdfDoc;

        Logger logItem;

        List<Logger> Logger;

        bool _isOpenedPdf;
        public bool IsOpenedPdf => _isOpenedPdf;

        public OpenPDF(string pdffile)
        {
              Logger = Program.Loger;

              _pdfDoc = new PdfDocument();

            try
            {
                _pdfDoc = PdfReader.Open(pdffile, PdfDocumentOpenMode.Modify);
                logItem = new Logger($"Open: {pdffile}", MesagType.Info);
                Logger.Add(logItem);
                _isOpenedPdf=true;
            }
            catch (Exception ex)
            {
                logItem = new Logger($"{ex.Message}: {pdffile}", MesagType.Error);
                Logger.Add(logItem);
                _isOpenedPdf=false;
            }
        }
    }
}
