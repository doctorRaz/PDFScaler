using System;
using System.Collections.Generic;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;
using drz.Servise;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;



namespace drz.PDF_Engine
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
