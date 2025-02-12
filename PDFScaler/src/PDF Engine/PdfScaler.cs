using System.Collections.Generic;
using System.IO;

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

        //string _mesag;

        /// <summary>
        /// Gets the mesag.
        /// </summary>
        /// <value>
        /// The mesag.
        /// </value>
        //public string Mesag => _mesag;

        ArrVPtemplate _tcf;
        public ArrVPtemplate TCF => _tcf;

        bool _isArrVP;

        /// <summary>
        /// Шаблон загружен
        /// </summary>
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
            Logger = Program.Logger;
            _tcf = new ArrVPtemplate();//получаем шаблон
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

#if !ADDVP
            FileDialogs FD = new FileDialogs();

            //получаем файлы для обработки
            if (!FD.FilesDialogOpen())
            {
                logItem = new Logger("Файлы PDF не выбраны", MesagType.Info);
                Logger.Add(logItem);

                return false;
            }
            // добавлятор VP
            string[] PdfFiles = FD.PdfFiles;
#endif
            string[] PdfFiles = new string[]{@"d:\@Developers\В работе\!Текущее\Programmers\!NET\GitHubMy\PDFScaler\temp\АК.pdf" };
            PdfVPSF Conversion = new PdfVPSF(ArrVP);

            foreach (string pdffile in PdfFiles)
            {
                //получаем документ
                PDFOpen OpenDoc = new PDFOpen(pdffile);
                if (!OpenDoc.IsOpenedPdf)
                {
                    continue;
                }

#if ADDVP
                Conversion.PdfSf(OpenDoc.PdfDoc);
                string sDir=Path.GetDirectoryName(pdffile);
                string sTempFile = Path.Combine(sDir, "temp.pdf");
                OpenDoc.PdfDoc.Save(sTempFile);
                return true;
#endif


                if (Conversion.SetPdfSf(OpenDoc.PdfDoc))//VP добавлен
                {
                    PDFSave savePDF = new PDFSave(OpenDoc.PdfDoc);//todo переделать вызов
                                                                  //вызывать метод с возвртатом bool или вообще на статик??
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


