
using System.Collections.Generic;
using System.ComponentModel;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;
using drz.Servise;

using PdfSharp.Drawing;

namespace drz.PdfSharp.Pdf
{
    /// <summary>
    /// Движок
    /// </summary>
    internal class PdfScaler
    {
        Logger logItem;

        List<Logger> Logger;

        XGraphicsUnit _convertUnit;
        XGraphicsUnit ConvertUnit => _convertUnit;

        WinGraphicsUnit WinConvertUnit
        {
            set
            {
                switch (value)
                {
                    case WinGraphicsUnit.Presentation: _convertUnit = XGraphicsUnit.Presentation; break;
                    case WinGraphicsUnit.Point: _convertUnit = XGraphicsUnit.Point; break;
                    case WinGraphicsUnit.Millimeter: _convertUnit = XGraphicsUnit.Millimeter; break;
                    case WinGraphicsUnit.Centimeter: _convertUnit = XGraphicsUnit.Centimeter; break;
                    case WinGraphicsUnit.Inch: _convertUnit = XGraphicsUnit.Inch; break;
                    default: throw new InvalidEnumArgumentException();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfScaler"/> class.
        /// <br>Default unit Millimeter <see cref="XGraphicsUnit.Millimeter"/></br> 
        /// </summary>
        public PdfScaler()
        {
            Logger = Program.Logger;
            _convertUnit = XGraphicsUnit.Millimeter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfScaler"/> class.
        /// </summary>
        /// <param name="convertUnit">The convert unit.</param>
        public PdfScaler(XGraphicsUnit convertUnit)
        {
            Logger = Program.Logger;
            _convertUnit = convertUnit;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfScaler"/> class.
        /// </summary>
        /// <param name="convertUnit">The convert unit.</param>
        public PdfScaler(WinGraphicsUnit convertUnit)
        {
            Logger = Program.Logger;
            WinConvertUnit =  convertUnit;
        }

        /// <summary>
        /// PDFs обработка.
        /// </summary>
        /// <returns></returns>
        public void PdfRun(string[] PdfFiles)
        {

            //инит передаем желаемые единицы
            PdfVPsf Conversion = new PdfVPsf(ConvertUnit, Logger);

            //по списку документов
            foreach (string pdffile in PdfFiles)
            {
                //получаем документ
                PDFOpen OpenDoc = new PDFOpen(pdffile);
                if (!OpenDoc.IsOpenedPdf)
                {
                    continue;
                }

                if (Conversion.SetPdfSf(OpenDoc.PdfDoc))// если хоть один VP добавлен
                {
                    PDFSave savePDF = new PDFSave(OpenDoc.PdfDoc);
                }
                else//ни один VP не добавлен, сохранять не надо
                {
                    logItem = new Logger($"Изменений нет. Файл не сохранен: {pdffile}", MesagType.Info);
                    Logger.Add(logItem);
                }
            }
        }
    }
}


