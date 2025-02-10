using System;
using System.Collections.Generic;
using System.IO;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;
using drz.Servise;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace drz.PDF_Engine
{
    /// <summary>
    /// Получаем шаблон VP
    /// Потом генерировать его программно
    /// </summary>
    internal class TemplateConversionFactor
    {
        /// <summary>
        /// The logger
        /// </summary>
        List<Logger> _logger;
        public List<Logger> Logger => _logger;

        /// <summary>
        /// Gets the pdftemp.
        /// </summary>
        /// <value>
        /// The pdftemp.
        /// </value>
        string pdftemp => Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location),
                                         "lib",
                                         "template_mm.res");

        PdfArray _arrVP;

        /// <summary>
        /// Gets or sets the arr VP.
        /// </summary>
        /// <value>
        /// The arr VP.
        /// </value>
        public PdfArray ArrVP => _arrVP;

        Boolean _isArrVP;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TemplateConversionFactor"/> is istmp.
        /// </summary>
        /// <value>
        ///   <c>true</c> if istmp; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsArrVP => _isArrVP;

 
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateConversionFactor"/> class.
        /// </summary>
        public TemplateConversionFactor()
        {
            _logger = Program.Logger;

            if (!File.Exists(pdftemp))//файла нет
            {
                _isArrVP = false;
                Logger log=new Logger("Не найден файл шаблона, продолжить работу невозможно!",MesagType.Error);
                _logger.Add(log);   
                  return;
            }

            //пытаемся получить шаблон
            PdfDocument PdfDoc = PdfReader.Open(pdftemp, PdfDocumentOpenMode.Import);
            PdfDictionary.DictionaryElements PdfDicEl = PdfDoc.Pages[0].Elements;
            _arrVP = PdfDicEl.GetObject("/VP") as PdfArray;
            if (ArrVP == null)//шаблона VP  файле нет
            {
                _isArrVP = false;
                Logger log = new Logger("Файл шаблона пустой, продолжить работу невозможно!", MesagType.Error);
                _logger.Add(log);
                return;
            }
            _isArrVP = true;
        }

    }
}
