using System;
using System.Collections.Generic;
using System.IO;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;
using drz.Servise;

using PdfSharp.Pdf;

namespace drz.PDF_Engine
{
    internal class PDFSave
    {
        bool _isSavedPdf;
        public bool IsSavedPdf => _isSavedPdf;

        //PdfDocument _pdfDoc;
        //public PdfDocument PdfDoc => _pdfDoc;

        Logger logItem;

        List<Logger> Logger;
        public PDFSave(PdfDocument PdfDoc)
        {
            Logger = Program.Logger;

            try
            {
           
                if (PdfDoc.Version < 16)
                {
                    PdfDoc.Version = 17;
                }

                string sPDFfile = PdfDoc.FullPath;

                string sTempFile =Path.Combine( DataSetWpfOpt.sTemp,"temp.pdf");

                //подбираем в темп уникальное имя

                //сохраняем в темп, если все Ок

                //подбираем сущ. файлу уникальное имя *.BAK

                //перемещаем из темп сохраненный в родную директорию с оригинальным именем

                //

                //            PdfDoc.Save(PdfFile); //todo в отдельный класс
                //           
                //            return true;
                //        }
                //        else
                //        {
                //            logItem = new Logger($"Not Saved: {PdfFile}", MesagType.Info);
                //            Logger.Add(logItem);
                //            return false;
                //        }
                logItem = new Logger($"Файл сохранен: {sPDFfile}", MesagType.Ok);
                Logger.Add(logItem);
                _isSavedPdf = true;
            }
            catch (Exception ex)
            {
                logItem = new Logger(ex.Message, MesagType.Error);
                Logger.Add(logItem);
                _isSavedPdf= false;
            }
        }
    }
}
