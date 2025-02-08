using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.Abstractions.Interfaces;
using drz.Infrastructure;
using drz.PdfSharp_ConversionFactor;

using PdfSharp.Pdf;

namespace drz.PDFScaler
{
    internal class PdfScaler
    {

        string _mesag;

        /// <summary>
        /// Gets the mesag.
        /// </summary>
        /// <value>
        /// The mesag.
        /// </value>
        public string Mesag => _mesag;

        TemplateConversionFactor _tcf;
        public  TemplateConversionFactor TCF=>_tcf;
        IConsoleService CS => new ConsoleService();

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
              _tcf = new TemplateConversionFactor();
            if (TCF.IsArrVP)
            {
                _arrVP = TCF.ArrVP;

                _isArrVP = true;
            }
            else
            {
                _isArrVP = false;
            }


        }


        public bool PdfRun( )
        {
            FileDialogs FD = new FileDialogs();
            //запрашиваем файлы
            if (!FD.FilesDialogOpen())
            {
               _mesag="Файлы PDF не выбраны" ;
                return false;
            }
            //добавляем VP
            ConversionFactor Conversion = new ConversionFactor(tmp);

            string[] PdfFiles = FD.PdfFiles;
            foreach (string pdffile in PdfFiles)
            {
                Conversion.SetPagesConversionFactor(pdffile);
            }
            return true;
        }
    }
}


