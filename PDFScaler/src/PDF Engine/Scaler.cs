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
        PdfDocument _pdfDoc;

        /// <summary>
        /// Gets the PDF document.
        /// </summary>
        /// <value>
        /// The PDF document.
        /// </value>
        public PdfDocument PdfDoc => _pdfDoc;

        Logger logItem;

        /// <summary>
        /// The is modifed
        /// </summary>
        public bool IsModifed;

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
        /// <param name="PdfFile">The PDF file.</param>
        /// <returns></returns>
        public bool SetPagesConversionFactor(PdfDocument pdfDoc)
        {

            _pdfDoc = pdfDoc;

            int pageNum = 0;
            foreach (PdfPage page in PdfDoc.Pages)
            {
                PdfDictionary.DictionaryElements PdfDicEl = page.Elements;
                PdfArray arrBBox1 = PdfDicEl.GetObject("/VP") as PdfArray;

                if (arrBBox1 == null)
                {
                    PdfDicEl.Add("/VP", ArrVP);//todo изменить размер BBox
                    IsModifed = true;//если меняли хоть один лист
                    logItem = new Logger($"Словарь добавлен в Page:{++pageNum}", MesagType.Ok);
                    Logger.Add(logItem);
                }
                else
                {
                    logItem = new Logger($"Словарь существует в Page:{++pageNum}", MesagType.Info);
                    Logger.Add(logItem);
                }
            }
            try
            {
                if (IsModifed)
                {
                    PdfSawer pds = new PdfSawer();
                    if (PdfDoc.Version < 16)
                    {
                        PdfDoc.Version = 17;
                    }
                    PdfDoc.Save(PdfFile); //todo в отдельный класс
                    logItem = new Logger($"Saved: {PdfFile}", MesagType.Ok);
                    Logger.Add(logItem);
                    return true;
                }
                else
                {
                    logItem = new Logger($"Not Saved: {PdfFile}", MesagType.Info);
                    Logger.Add(logItem);
                    return false;
                }

            }
            catch (Exception ex)
            {
                logItem = new Logger(ex.Message, MesagType.Error);
                Logger.Add(logItem);
                return false;
            }
        }

    }
}
