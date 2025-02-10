using System;
using System.Collections.Generic;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;
using drz.Servise;

using PdfSharp.Pdf;

namespace drz.PDF_Engine
{
    internal class Scaler
    {
        //PdfDocument _pdfDoc;

        /// <summary>
        /// Gets the PDF document.
        /// </summary>
        /// <value>
        /// The PDF document.
        /// </value>
        //public PdfDocument PdfDoc => _pdfDoc;

        Logger logItem;

        /// <summary>
        /// The is PDF modified
        /// </summary>
        public bool IsModified;

        /// <summary>
        /// The logger
        /// </summary>
        public List<Logger> Logger;

        PdfArray ArrVP => _arrVP;

        PdfArray _arrVP;

        public Scaler(PdfArray ArrVP)
        {
            Logger = Program.Loger;
            _arrVP = ArrVP;
        }

        /// <summary>
        /// Sets the pages conversion factor.
        /// </summary>
        /// <param name="pdfDoc">The PDF document.</param>
        /// <returns></returns>
        public bool SetPagesConversionFactor(PdfDocument PdfDoc)
        {

            //_pdfDoc = pdfDoc;
            IsModified = false;
            int pageNum = 0;
            foreach (PdfPage page in PdfDoc.Pages)
            {
                PdfDictionary.DictionaryElements PdfDicEl = page.Elements;
                PdfArray arrBBox1 = PdfDicEl.GetObject("/VP") as PdfArray;

                if (arrBBox1 == null)
                {
                    PdfDicEl.Add("/VP", ArrVP);//todo изменить размер BBox
                    IsModified = true;//если меняли хоть один лист
                    logItem = new Logger($"VP добавлен в Page:{++pageNum}", MesagType.Ok);
                    Logger.Add(logItem);
                }
                else
                {
                    IsModified = false;
                    logItem = new Logger($"VP существует в Page:{++pageNum}", MesagType.Info);
                    Logger.Add(logItem);
                }
            }
            if (IsModified)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
