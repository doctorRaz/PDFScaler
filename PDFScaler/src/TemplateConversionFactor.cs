using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Gets or sets the arr bbox.
        /// </summary>
        /// <value>
        /// The arr b box.
        /// </value>
        public PdfArray arrBBox { get; set; }

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
            if (!File.Exists(pdftemp))
            {
                Istmp = false;
                Console.WriteLine("Не найден файл шаблона, продолжить работу невозможно!");
                return;
            }

            PdfDocument PdfDoc = PdfReader.Open(pdftemp, PdfDocumentOpenMode.Import);
            PdfDictionary.DictionaryElements PdfDicEl = PdfDoc.Pages[0].Elements;
            arrBBox = PdfDicEl.GetObject("/VP") as PdfArray;
            if (arrBBox == null)
            {
                Istmp = false;
                Console.WriteLine("Файл шаблона пустой, продолжить работу невозможно!");
                return;
            }
            Istmp = true;
        }

    }
}
