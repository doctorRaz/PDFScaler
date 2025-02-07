using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.Abstractions.Interfaces;
using drz.Infrastructure;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace drz.PdfSharp_ConversionFactor
{
    /// <summary>
    /// Получаем шаблон BBox
    /// </summary>
    internal class TemplateConversionFactor
    {
        /// <summary>
        /// Gets the pdftemp.
        /// </summary>
        /// <value>
        /// The pdftemp.
        /// </value>
        string pdftemp => Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location),
                                         "lib",
                                         "template_mm.res");

        /// <summary>
        /// Gets or sets the arr VP.
        /// </summary>
        /// <value>
        /// The arr VP.
        /// </value>
        public PdfArray arrVP { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TemplateConversionFactor"/> is istmp.
        /// </summary>
        /// <value>
        ///   <c>true</c> if istmp; otherwise, <c>false</c>.
        /// </value>
        public Boolean Istmp { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateConversionFactor"/> class.
        /// </summary>
        public TemplateConversionFactor()
        {
            IConsoleService CS = new ConsoleService();

            if (!File.Exists(pdftemp))
            {
                Istmp = false;
                CS.ConsoleMsg("Не найден файл шаблона, продолжить работу невозможно!",
                              WConsoleColor.White,
                              WConsoleColor.DarkRed);
                return;
            }

            PdfDocument PdfDoc = PdfReader.Open(pdftemp, PdfDocumentOpenMode.Import);
            PdfDictionary.DictionaryElements PdfDicEl = PdfDoc.Pages[0].Elements;
            arrVP = PdfDicEl.GetObject("/VP") as PdfArray;
            if (arrVP == null)
            {
                Istmp = false;
                CS.ConsoleMsg("Файл шаблона пустой, продолжить работу невозможно!",
                              WConsoleColor.White,
                              WConsoleColor.DarkRed);
                return;
            }
            Istmp = true;
        }

    }
}
