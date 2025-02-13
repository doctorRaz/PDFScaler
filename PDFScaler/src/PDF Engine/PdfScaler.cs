
using System.Collections.Generic;
using System.ComponentModel;

using drz.Abstractions.Interfaces;
using drz.Enum;
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
        ILogger logItem;

        List<ILogger> Logger;

        XGraphicsUnit _convertUnit;
        XGraphicsUnit ConvertUnit => _convertUnit;

        WinGraphicsUnit WinConvertUnit
        {
            set
            {
                switch (value)
                {
                    case WinGraphicsUnit.Centimeter: _convertUnit = XGraphicsUnit.Centimeter; break;
                    case WinGraphicsUnit.Inch: _convertUnit = XGraphicsUnit.Inch; break;
                    case WinGraphicsUnit.Millimeter: _convertUnit = XGraphicsUnit.Millimeter; break;
                    case WinGraphicsUnit.Point: _convertUnit = XGraphicsUnit.Point; break;
                    case WinGraphicsUnit.Presentation: _convertUnit = XGraphicsUnit.Presentation; break;
                    default: throw new InvalidEnumArgumentException();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfScaler" /> class.
        /// <br>Default unit Millimeter <see cref="XGraphicsUnit.Millimeter" /></br>
        /// </summary>
        /// <param name="logger">The logger.</param>
        public PdfScaler(List<ILogger> logger)
        {
            Logger = logger;
            _convertUnit = XGraphicsUnit.Millimeter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfScaler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="convertUnit">The convert unit.</param>
        public PdfScaler(List<ILogger> logger,XGraphicsUnit convertUnit)
        {
            Logger = logger;
            _convertUnit = convertUnit;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfScaler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="convertUnit">The convert unit.</param>
        public PdfScaler(List<ILogger> logger,WinGraphicsUnit convertUnit)
        {
            Logger = logger;
            WinConvertUnit = convertUnit;
        }

        /// <summary>
        /// PDFs обработка.
        /// </summary>
        /// <param name="PdfFiles">The PDF files.</param>
        public void PdfRun(string[] PdfFiles)
        {
            //инит передаем желаемые единицы
            DocConversion Conversion = new DocConversion(Logger);

            //по списку документов
            foreach (string pdffile in PdfFiles)
            {
                //получаем документ
                PDFOpen OpenDoc = new PDFOpen(pdffile);

                if (!OpenDoc.IsOpenedPdf)
                {
                    continue;
                }

                if (Conversion.DocRun(OpenDoc.PdfDoc, ConvertUnit))// если хоть один VP добавлен
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


