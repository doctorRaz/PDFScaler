using System;

using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace drz.PdfSharp.Pdf
{
    /// <summary>
    /// Добавляет VP с масштабом вида
    /// </summary>
    public class VPtoPage
    {

        public VPtoPage(XGraphicsUnit convertUnit)
        {          
            XUnit unit = new XUnit(1, convertUnit);

            _scalefactor = 1 / unit.Point;//пересчет единиц в точку
                       
        
        }

        /// <summary>
        /// PDFs the sf.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public bool PdfSf(PdfPage page)
        {
            try
            {
                #region VP0

                //PdfPage page0 = pdfDoc.Pages[1];

                PdfArray arrVP0 = new PdfArray();
                page.Elements.Add("/VP", arrVP0);//добавить VP

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

                #endregion
            }
            catch (Exception ex)
            {
                _exept = ex.Message;
                return false;
            }
            return true;

            #region Example Get VP sf
#if DEBUG
            //GetSF(pdfDoc);
#endif
            #endregion
        }

        public string Exept => _exept;

        double Scalefactor => _scalefactor;

        string _exept;
        double _scalefactor;

    }


}
