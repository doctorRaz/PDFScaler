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
    internal class PDFOpen
    {
        PdfDocument _pdfDoc;
        /// <summary>
        /// Документ PDF, забираем отсюда поссылке
        /// </summary>
        public PdfDocument PdfDoc => _pdfDoc;

        Logger logItem;

        List<Logger> Logger;

        bool _isOpenedPdf;

        /// <summary>
        /// Флаг успех неуспех
        /// </summary>
        public bool IsOpenedPdf => _isOpenedPdf;

        /// <summary>
        /// Открывает документ
        /// </summary>
        /// <param name="pdffile"></param>
        public PDFOpen(string pdffile)
        {
              Logger = Program.Logger;

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
