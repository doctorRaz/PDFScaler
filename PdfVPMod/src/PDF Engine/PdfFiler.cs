using System;
using System.Collections.Generic;
using System.IO;

using drz.PdfVpMod.Abstractions.Interfaces;
using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Servise;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace drz.PdfVpMod.PdfSharp
{
    /// <summary>
    /// Open Save PDF doc
    /// </summary>
    internal class PdfFiler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfFiler"/> class.
        /// </summary>
        /// <param name="loger">The loger.</param>
        public PdfFiler(List<ILoger> loger)
        {
            Loger = loger;
        }

        /// <summary>
        /// Opens the PDF.
        /// </summary>
        /// <param name="pdffile">The pdffile.</param>
        /// <returns></returns>
        public bool PdfOpen(string pdffile)
        {
            _pdfDoc = new PdfDocument();
            try
            {
                _pdfDoc = PdfReader.Open(pdffile, PdfDocumentOpenMode.Modify);
                logItem = new Loger($"Open: {pdffile}", MesagType.Info);
                Loger.Add(logItem);
                return true;
            }
            catch (Exception ex)
            {
                logItem = new Loger($"{ex.Message}: {pdffile}", MesagType.Error);
                Loger.Add(logItem);
                return false;
            }
        }

        /// <summary>
        /// Saves the PDF.
        /// </summary>
        /// <param name="pdfDoc">The PDF document.</param>
        /// <returns></returns>
        public bool PdfSave(PdfDocument pdfDoc)
        {
            string sPDFfile = string.Empty;
            try
            {
                sPDFfile = pdfDoc.FullPath;

                //string sTempFile = Path.Combine(DataSetWpfOpt.sTemp, "temp.pdf");
                string sTempFile = Path.Combine(Path.GetTempPath(), "temp.pdf");

                //подбираем в темп уникальное имя
                sTempFile = UtilitesWorkFil.GetFileNameUniqu(sTempFile);

                //сохраняем в темп, если все Ок
                pdfDoc.Save(sTempFile);

                //подбираем сущ. файлу уникальное имя *.BAK
                string sPDFfileBAK = UtilitesWorkFil.GetFileNameUniqu($"{sPDFfile}.BAK");

                //переименовываем сущ. файл
                File.Move(sPDFfile, sPDFfileBAK);

                //перекидываем из темп новый файл на место существующего
                File.Move(sTempFile, sPDFfile);

                logItem = new Loger($"Файл сохранен: {sPDFfile}", MesagType.Ok);
                Loger.Add(logItem);
                return true;
            }
            catch (Exception ex)
            {
                logItem = new Loger($"{ex.Message} Не удалось сохранить: {sPDFfile}", MesagType.Error);
                Loger.Add(logItem);
                return false;
            }
        }

        /// <summary>
        /// Gets the PDF document.
        /// </summary>
        /// <value>
        /// The PDF document.
        /// </value>
        public PdfDocument PdfDoc => _pdfDoc;

        //public bool IsOpenedPdf => _isOpenedPdf;

        //public bool IsSavedPdf => _isSavedPdf;


        List<ILoger> Loger { get; set; }

        ILoger logItem;

        //bool _isOpenedPdf;
        PdfDocument _pdfDoc;
        //bool _isSavedPdf;



    }
}
