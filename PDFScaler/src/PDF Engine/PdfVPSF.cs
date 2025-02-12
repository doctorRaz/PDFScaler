using System.Collections.Generic;

using drz.Abstractions.Interfaces;
using drz.PDFScaler;
using drz.Servise;

using PdfSharp.Pdf;

namespace PdfSharp.Pdf
{
    internal class PdfVPSF
    {
        public PdfVPSF(PdfArray ArrVP)
        {
            Logger = Program.Logger;
            _arrVP = ArrVP;
        }

        /// <summary>
        /// Sets the pages conversion factor.
        /// </summary>
        /// <param name="pdfDoc">The PDF document.</param>
        /// <returns></returns>
        public bool SetPdfSf(PdfDocument PdfDoc)
        {
            //_pdfDoc = pdfDoc;
            IsModified = false;
            int pageNum = 0;
            foreach (PdfPage page in PdfDoc.Pages)
            {

                PdfDictionary.DictionaryElements PdfDicEl = page.Elements;
                PdfArray arrBBox = PdfDicEl.GetObject("/VP") as PdfArray;

                if (arrBBox == null)
                {
                    PdfDicEl.Add("/VP", ArrVP);//todo изменить размер BBox


                    IsModified = true;//если меняли хоть один лист
                    logItem = new Logger($"\tVP добавлен в Page:{++pageNum}", MesagType.Ok);
                    Logger.Add(logItem);
                }
                else
                {
                    IsModified = false;
                    logItem = new Logger($"\tVP существует в Page:{++pageNum}", MesagType.Info);
                    Logger.Add(logItem);
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




        #region Servise

        Logger logItem;

          /// <summary>
        /// The logger
        /// </summary>
        public List<Logger> Logger;

        #endregion

        #region PDF

        double _scalefactor;
        double Scalefactor => _scalefactor;

        PdfPage _page;
        PdfPage Page=>_page;

        PdfDocument _pdfDoc;

        /// <summary>
        /// Gets the PDF document.
        /// </summary>
        /// <value>
        /// The PDF document.
        /// </value>
        public PdfDocument PdfDoc => _pdfDoc;

        #endregion

        /// <summary>
        /// The is PDF modified
        /// </summary>
        public bool IsModified;      

        PdfArray ArrVP => _arrVP;

        PdfArray _arrVP;

     


        /// <summary>
        /// Adds the vp.
        /// </summary>
        /// <param name="pdfDoc">The PDF document.</param>
        public PdfArray PdfSf(PdfDocument pdfDoc)
        {           
            #region VP0

            PdfPage page0 = pdfDoc.Pages[1];

            PdfArray arrVP0 = new PdfArray();
            page0.Elements.Add("/VP", arrVP0);

            PdfDictionary dicVPitem0 = new PdfDictionary();
            arrVP0.Elements.Add(dicVPitem0);

            #region BBox

            PdfArray dicBBox0 = new PdfArray();
            dicVPitem0.Elements.Add("/BBox", dicBBox0);

            dicBBox0.Elements.Add(new PdfInteger((int)page0.MediaBox.X1));
            dicBBox0.Elements.Add(new PdfInteger((int)page0.MediaBox.Y1));
            dicBBox0.Elements.Add(new PdfInteger((int)page0.MediaBox.X2));
            dicBBox0.Elements.Add(new PdfInteger((int)page0.MediaBox.Y2));

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

            dicXitem0.Elements.Add("/C", new PdfReal(0.35278));
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
            GetSF(pdfDoc);
#endif
            #endregion
        }



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
