using System.Collections.Generic;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;
using drz.Servise;

using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace drz.PdfSharp.Pdf
{
    internal class PdfVPsf
    {
        #region INIT       

        /// <summary>
        /// Настройка конвертора VP
        /// </summary>
        /// <param name="convertUnit">Единицы в которые преобразуем</param>
        public PdfVPsf(XGraphicsUnit convertUnit)
        {
            _convertUnit = convertUnit;

            Logger = Program.Logger;

            XUnit unit = new XUnit(1, ConvertUnit);

            _scalefactor = 1 / unit.Point;//пересчет единиц в точку

        }
        #endregion

        /// <summary>
        /// Sets the pages conversion factor.
        /// </summary>
        /// <param name="PdfDoc">The PDF document.</param>
        /// <returns></returns>
        public bool SetPdfSf(PdfDocument PdfDoc)
        {
            IsModified = false;

            int pageNum = 0;//номер стр для логера

            //перебор страниц
            foreach (PdfPage page in PdfDoc.Pages)
            {
                //get VP
                PdfArray arrBBox = page.Elements.GetObject("/VP") as PdfArray;

                if (arrBBox == null)//если VP нет
                {
                    page.Elements.Add("/VP", PdfSf(page));//добавить VP

                    IsModified = true;//если меняли хоть один лист
                    _logItem = new Logger($"\tVP добавлен в Page:{++pageNum}", MesagType.Ok);
                    Logger.Add(_logItem);
                }
                else
                {
                    IsModified = false;
                    _logItem = new Logger($"\tVP существует в Page:{++pageNum}", MesagType.Info);
                    Logger.Add(_logItem);
                }
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

        /// <summary>
        /// PDFs the sf.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public PdfArray PdfSf(PdfPage page)
        {
            #region VP0

            //PdfPage page0 = pdfDoc.Pages[1];

            PdfArray arrVP0 = new PdfArray();
            //page0.Elements.Add("/VP", arrVP0);

            PdfDictionary dicVPitem0 = new PdfDictionary();
            arrVP0.Elements.Add(dicVPitem0);

            #region BBox

            PdfArray dicBBox0 = new PdfArray();
            dicVPitem0.Elements.Add("/BBox", dicBBox0);

            dicBBox0.Elements.Add(new PdfInteger((int)page.MediaBox.X1));
            dicBBox0.Elements.Add(new PdfInteger((int)page.MediaBox.Y1));
            dicBBox0.Elements.Add(new PdfInteger((int)page.MediaBox.X2));
            dicBBox0.Elements.Add(new PdfInteger((int)page.MediaBox.Y2));

            #endregion

            #region Measure

            PdfDictionary dicMeasure0 = new PdfDictionary();
            dicVPitem0.Elements.Add("/Measure", dicMeasure0);

            #region /A
            PdfArray arrA0 = new PdfArray();
            dicMeasure0.Elements.Add("/A", arrA0);

            PdfDictionary arrAitem0 = new PdfDictionary();
            arrA0.Elements.Add(arrAitem0);

            arrAitem0.Elements.Add("/C", new PdfInteger(1));
            arrAitem0.Elements.Add("/U", new PdfString(" "));
            #endregion

            #region /D
            PdfArray arrD0 = new PdfArray();
            dicMeasure0.Elements.Add("/D", arrD0);

            PdfDictionary arrDitem0 = new PdfDictionary();
            arrD0.Elements.Add(arrDitem0);

            arrDitem0.Elements.Add("/C", new PdfInteger(1));
            arrDitem0.Elements.Add("/U", new PdfString(" "));

            #endregion

            #region /R
            PdfString strR0 = new PdfString(" ");
            dicMeasure0.Elements.Add("/R", strR0);

            #endregion

            #region /Subtype
            PdfName nameSubtype0 = new PdfName("/RL");
            dicMeasure0.Elements.Add("/Subtype", nameSubtype0);

            #endregion

            #region  /Type
            PdfName nameType0 = new PdfName("/Measure");
            dicMeasure0.Elements.Add("/Type", nameType0);

            #endregion

            #region  /X
            PdfArray arrX0 = new PdfArray();
            dicMeasure0.Elements.Add("/X", arrX0);

            PdfDictionary dicXitem0 = new PdfDictionary();
            arrX0.Elements.Add(dicXitem0);

            dicXitem0.Elements.Add("/C", new PdfReal(Scalefactor));//0.35278
            dicXitem0.Elements.Add("/U", new PdfString(" "));

            #endregion

            #endregion

            #region  Type
            PdfName dicType0 = new PdfName("/Viewport");
            dicVPitem0.Elements.Add("/Type", dicType0);
            #endregion

            return arrVP0;
            #endregion

            #region Example Get VP sf
#if DEBUG
            //GetSF(pdfDoc);
#endif
            #endregion
        }

        #region ПОЛЯ СВОЙСТВА

        #region Servise

        Logger _logItem;

        /// <summary>
        /// The logger
        /// </summary>
        public List<Logger> Logger;

        #endregion

        #region VP

        double _scalefactor;
        double Scalefactor => _scalefactor;


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
        public bool IsModified;


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
