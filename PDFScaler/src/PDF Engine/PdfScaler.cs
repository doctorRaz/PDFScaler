using System.Collections.Generic;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;
using drz.Servise;
using drz.Win;

using PdfSharp.Pdf;

namespace drz.PDF_Engine
{
    internal class PdfScaler
    {
        Logger logItem;

        List<Logger> Logger;

        string _mesag;

        /// <summary>
        /// Gets the mesag.
        /// </summary>
        /// <value>
        /// The mesag.
        /// </value>
        public string Mesag => _mesag;

        TemplateConversionFactor _tcf;
        public TemplateConversionFactor TCF => _tcf;

        bool _isArrVP;
        public bool IsArrVP => _isArrVP;

        PdfArray _arrVP;
        /// <summary>
        /// Gets or sets the arr View Port.
        /// </summary>
        /// <value>
        /// The arr View Port.
        /// </value>
        public PdfArray ArrVP => _arrVP;

        public PdfScaler()
        {
            Logger = Program.Loger;
            _tcf = new TemplateConversionFactor();//получаем шаблон
            if (TCF.IsArrVP)
            {
                _arrVP = TCF.ArrVP;//виевпорт из шаблона

                _isArrVP = true;
            }
            else
            {
                _isArrVP = false;
            }
        }

        /// <summary>
        /// PDFs обработка.
        /// </summary>
        /// <returns></returns>
        public bool PdfRun()
        {
            FileDialogs FD = new FileDialogs();

            //получаем файлы для обработки
            if (!FD.FilesDialogOpen())
            {
                logItem = new Logger("Файлы PDF не выбраны", MesagType.Info);
                Logger.Add(logItem);

                return false;
            }
            // добавлятор VP
            Scaler Conversion = new Scaler(ArrVP);

            string[] PdfFiles = FD.PdfFiles;

            foreach (string pdffile in PdfFiles)
            {
                //получаем документ
                OpenPDF OpenDoc = new OpenPDF(pdffile);
                if (!OpenDoc.IsOpenedPdf)
                {
                    continue;
                }

                if (Conversion.SetPagesConversionFactor(OpenDoc.PdfDoc))//VP добавлен
                {
                    SavePDF savePDF = new SavePDF(OpenDoc.PdfDoc);//todo переделать вызов
                                                                  //вызывать метод с возвртатом bool или вообще на статик??
                    if (savePDF.IsSavedPdf)
                    {

                    }
                    else
                    {

                    }
                    //проверка на удачное или нет сохранение
                }
                else//сохранять не надо
                {
                    logItem = new Logger($"Изменений нет. Файл не сохранен: {pdffile}", MesagType.Info);
                    Logger.Add(logItem);
                }
            }
            return true;
        }
    }
}


