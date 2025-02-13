using System;
using System.Collections.Generic;
using System.IO;

using drz.Abstractions.Interfaces;
using drz.Enum;
using drz.Servise;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace drz.PdfSharp.Pdf
{
  internal  class PdfFiler
    {
        public PdfFiler(List<ILogger> logger)
        {
            Logger = logger;
        }

        public bool OpenPdf(string pdffile)
        {
            _pdfDoc = new PdfDocument();
            try
            {
                _pdfDoc = PdfReader.Open(pdffile, PdfDocumentOpenMode.Modify);
                logItem = new Logger($"Open: {pdffile}", MesagType.Info);
                //Logger.Add(logItem);
                return true;
            }
            catch (Exception ex)
            {
                logItem = new Logger($"{ex.Message}: {pdffile}", MesagType.Error);
                //Logger.Add(logItem);
                return false;
            }
            finally
            {
                Logger.Add(logItem);
            }

        }

        public bool SavePDF(PdfDocument pdfDoc)//todo add one Class Filer
        {
            string sPDFfile=string.Empty;
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

                logItem = new Logger($"Файл сохранен: {sPDFfile}", MesagType.Ok);
                //Logger.Add(logItem);
                return true;
            }
            catch (Exception ex)
            {
                logItem = new Logger($"{ex.Message} Не удалось сохранить: {sPDFfile}", MesagType.Error);
                //Logger.Add(logItem);
                return false;
            }
            finally
            {
                Logger.Add(logItem);
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


        List<ILogger> Logger { get; set; }

        ILogger logItem;

        //bool _isOpenedPdf;
        PdfDocument _pdfDoc;
        //bool _isSavedPdf;

 

    }
}
