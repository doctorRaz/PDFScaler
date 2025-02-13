using System;
using System.Collections.Generic;
using System.IO;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;
using drz.Servise;

using PdfSharp.Pdf;

namespace drz.PdfSharp.Pdf
{
    internal class PDFSave
    {
        bool _isSavedPdf;
        public bool IsSavedPdf => _isSavedPdf;

        string sPDFfile;

        Logger logItem;

        List<ILogger> Logger;
        public PDFSave(PdfDocument PdfDoc)//todo add one Class Filer
        {
            Logger = Program.Logger;


            try
            {

                //if (PdfDoc.Version < 16)
                //{
                //    PdfDoc.Version = 17;
                //}

                sPDFfile = PdfDoc.FullPath;

                string sTempFile = Path.Combine(DataSetWpfOpt.sTemp, "temp.pdf");

                //подбираем в темп уникальное имя
                sTempFile = UtilitesWorkFil.GetFileNameUniqu(sTempFile);

                //сохраняем в темп, если все Ок
                PdfDoc.Save(sTempFile);

                //подбираем сущ. файлу уникальное имя *.BAK
                string sPDFfileBAK = UtilitesWorkFil.GetFileNameUniqu($"{sPDFfile}.BAK");

                //переименовываем сущ. файл
                File.Move(sPDFfile, sPDFfileBAK);

                //перекидываем из темп новый файл на место существующего
                File.Move(sTempFile, sPDFfile);

                logItem = new Logger($"Файл сохранен: {sPDFfile}", MesagType.Ok);
                Logger.Add(logItem);
                _isSavedPdf = true;
            }
            catch (Exception ex)
            {
                logItem = new Logger($"{ex.Message}\nНе удалось сохранить: {sPDFfile}", MesagType.Error);
                Logger.Add(logItem);
                _isSavedPdf = false;
            }
        }
    }
}
