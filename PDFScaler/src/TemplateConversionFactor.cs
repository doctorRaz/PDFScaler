using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.Abstractions.Interfaces;
using drz.Infrastructure;
using drz.Servise;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace drz.PdfSharp_ConversionFactor
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
        public PdfArray ArrVP { get; set; }

        Boolean _isArrVP;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TemplateConversionFactor"/> is istmp.
        /// </summary>
        /// <value>
        ///   <c>true</c> if istmp; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsArrVP => _isArrVP;

        string _mesag;

        /// <summary>
        /// Gets the mesag.
        /// </summary>
        /// <value>
        /// The mesag.
        /// </value>
        public string Mesag => _mesag;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateConversionFactor"/> class.
        /// </summary>
        public TemplateConversionFactor()
        {
            _logger = Program.Loger;

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
            ArrVP = PdfDicEl.GetObject("/VP") as PdfArray;
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
