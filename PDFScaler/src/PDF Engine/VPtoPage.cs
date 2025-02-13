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

        public VPtoPage(PdfPage page,
                        XGraphicsUnit convertUnit = XGraphicsUnit.Millimeter,
                        bool changeVpPage = false)
        {
            _page = page;

            XUnit unit = new XUnit(1, convertUnit);

            _scalefactor = 1 / unit.Point;//пересчет единиц в точку

            try
            {
                #region Check VP Page
                _arrVP = Page.Elements.GetObject("/VP") as PdfArray;
                if (ArrVP == null)//если VP нет
                {
                    #region VP NEW
                    _arrVP = new PdfArray();
                    Page.Elements.Add("/VP", ArrVP);//добавить VP
                    #endregion

                    if (AddArrVP())
                    {
                        _mesag = "VP добавлен в";
                        _isChanged = true;
                    }
                    else
                    {
                        _isChanged = false;
                    }
                }
                else if (changeVpPage)//заказано изменить VP scalefactor
                {
                    if(ModArrVP())
                    {
                    _mesag = "VP изменен в";
                    
                    _isChanged = true;
                    }
                }
                else
                {
                    _mesag = "VP существует в";
                    _isChanged = false;
                }
                #endregion


            }
            catch (Exception ex)
            {
                _mesag = ex.Message;
                _isErr = true;
                _isChanged = false;
            }
        }

         bool ModArrVP()
        {
            //todo добавить изменение scalefactor
            throw new NotImplementedException();
            
        }

        bool AddArrVP()
        {
            try
            {
                #region VP

                PdfDictionary dicVPitem = new PdfDictionary();
                ArrVP.Elements.Add(dicVPitem);

                #region BBox

                PdfArray dicBBox = new PdfArray();
                dicVPitem.Elements.Add("/BBox", dicBBox);

                dicBBox.Elements.Add(new PdfInteger((int)Page.MediaBox.X1));
                dicBBox.Elements.Add(new PdfInteger((int)Page.MediaBox.Y1));
                dicBBox.Elements.Add(new PdfInteger((int)Page.MediaBox.X2));
                dicBBox.Elements.Add(new PdfInteger((int)Page.MediaBox.Y2));

                #endregion

                #region Measure

                PdfDictionary dicMeasure = new PdfDictionary();
                dicVPitem.Elements.Add("/Measure", dicMeasure);

                #region /A
                PdfArray arrA = new PdfArray();
                dicMeasure.Elements.Add("/A", arrA);

                PdfDictionary arrAitem = new PdfDictionary();
                arrA.Elements.Add(arrAitem);

                arrAitem.Elements.Add("/C", new PdfInteger(1));
                arrAitem.Elements.Add("/U", new PdfString(" "));
                #endregion

                #region /D
                PdfArray arrD = new PdfArray();
                dicMeasure.Elements.Add("/D", arrD);

                PdfDictionary arrDitem = new PdfDictionary();
                arrD.Elements.Add(arrDitem);

                arrDitem.Elements.Add("/C", new PdfInteger(1));
                arrDitem.Elements.Add("/U", new PdfString(" "));

                #endregion

                #region /R
                PdfString strR = new PdfString(" ");
                dicMeasure.Elements.Add("/R", strR);

                #endregion

                #region /Subtype
                PdfName nameSubtype = new PdfName("/RL");
                dicMeasure.Elements.Add("/Subtype", nameSubtype);

                #endregion

                #region  /Type
                PdfName nameType = new PdfName("/Measure");
                dicMeasure.Elements.Add("/Type", nameType);

                #endregion

                #region  /X
                PdfArray arrX = new PdfArray();
                dicMeasure.Elements.Add("/X", arrX);

                PdfDictionary dicXitem = new PdfDictionary();
                arrX.Elements.Add(dicXitem);

                dicXitem.Elements.Add("/C", new PdfReal(Scalefactor));//.35278
                dicXitem.Elements.Add("/U", new PdfString(" "));

                #endregion

                #endregion

                #region  Type
                PdfName dicType = new PdfName("/Viewport");
                dicVPitem.Elements.Add("/Type", dicType);
                #endregion

                #endregion
                return true;
            }
            catch (Exception ex)
            {
                _mesag = ex.Message;
                _isErr = true;
                return false;
            }
        }

        public PdfPage Page => _page;

        public PdfArray ArrVP => _arrVP;
        public string Mesag => _mesag;

        public bool IsChanged => _isChanged;
        public bool IsErr => _isErr;

        public double Scalefactor => _scalefactor;

        PdfPage _page;
        PdfArray _arrVP;
        bool _isChanged;
        bool _isErr;
        string _mesag;
        double _scalefactor;

    }


}
