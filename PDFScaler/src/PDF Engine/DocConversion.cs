
using System.Collections.Generic;

using drz.Servise;
using drz.Abstractions.Interfaces;

#if CONSOLE
using drz.PDFScaler;
#endif

using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;

namespace drz.PdfSharp.Pdf
{
    internal class DocConversion
    {
        #region INIT       

        /// <summary>
        /// Настройка конвертора VP
        /// </summary>
        /// <param name="convertUnit">Единицы в которые преобразуем</param>
        public DocConversion(XGraphicsUnit convertUnit, List<ILogger> logger)
        {
            _convertUnit = convertUnit;

            Logger = logger;         

        }
        #endregion

        /// <summary>
        /// Обработка документаn.
        /// </summary>
        /// <param name="PdfDoc">PDF document.</param>
        /// <returns>успех</returns>
        public bool DocRun(PdfDocument PdfDoc)
        {
            _isModified = false;

            int pageNum = 0;//номер стр для логера

            //перебор страниц
            foreach (PdfPage page in PdfDoc.Pages)
            {
                //get VP
                PdfArray arrBBox = page.Elements.GetObject("/VP") as PdfArray;

                if (arrBBox == null)//если VP нет
                {
                    if (PdfSf(page))
                    {
                        _isModified = true;//если меняли хоть один лист
                        _logItem = new Logger($"\tVP добавлен в Page:{++pageNum}", MesagType.Ok);
                    }
                    else
                    {
                        _isModified = false;
                        _logItem = new Logger($"\tVP сбой в Page:{++pageNum} {Exept}", MesagType.Error);

                    }
                    //Logger.Add(_logItem);
                }
                else
                {
                    _isModified = false;
                    _logItem = new Logger($"\tVP существует в Page:{++pageNum}", MesagType.Info);
                    //Logger.Add(_logItem);
                }
                Logger.Add(_logItem);
            }
            if (IsModified)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
             

        #region ПОЛЯ СВОЙСТВА

        #region Servise

        ILogger _logItem;

        /// <summary>
        /// The logger
        /// </summary>
        public List<ILogger> Logger;

        #endregion

        #region VP




        XGraphicsUnit _convertUnit;
        XGraphicsUnit ConvertUnit => _convertUnit;

        #endregion
        #region PDF

        //PdfPage _page;
        //PdfPage Page => _page;

        //PdfDocument _pdfDoc;

        /// <summary>
        /// Gets the PDF document.
        /// </summary>
        /// <value>
        /// The PDF document.
        /// </value>
        //public PdfDocument PdfDoc => _pdfDoc;

        #endregion

        /// <summary>
        /// The is PDF modified
        /// </summary>
        public bool IsModified => _isModified;

        bool _isModified;

        #endregion




        #region EXAMPLE
#if DEBUG
        /// <summary>
        /// Adds the VP example.
        /// </summary>
        /// <param name="pdfDoc">The PDF document.</param>
        public void GetSF(PdfDocument pdfDoc)
        {

            PdfPage page = pdfDoc.Pages[0];

            PdfArray arrVP = page.Elements.GetObject("/VP") as PdfArray;
            PdfDictionary dicVPitem = arrVP.Elements.Items[0] as PdfDictionary;

            PdfArray dicBBox = dicVPitem.Elements.GetValue("/BBox") as PdfArray;

            PdfDictionary dicMeasure = dicVPitem.Elements.GetObject("/Measure") as PdfDictionary;

            PdfArray arrA = dicMeasure.Elements.GetValue("/A") as PdfArray;

            PdfArray arrD = dicMeasure.Elements.GetValue("/D") as PdfArray;

            PdfString arrR = dicMeasure.Elements.GetValue("/R") as PdfString;

            PdfName nameSubtype = dicMeasure.Elements.GetValue("/Subtype") as PdfName;

            PdfName nameType = dicMeasure.Elements.GetValue("/Type") as PdfName;

            PdfArray arrX = dicMeasure.Elements.GetValue("/X") as PdfArray;

            PdfDictionary dicC = arrX.Elements.Items[0] as PdfDictionary;

            PdfReal ConversionFactor = dicC.Elements.GetValue("/C") as PdfReal;//(0.35278) собственно искомое

            PdfName dicType = dicVPitem.Elements.GetValue("/Type") as PdfName;

        }

#endif
        #endregion
    }

}
