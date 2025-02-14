using System;
using System.Collections.Generic;

using drz.Abstractions.Interfaces;
using drz.Servise;

using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace drz.PdfSharp.Pdf
{
    /// <summary>
    /// Добавляет удаляет VP с масштабом вида
    /// </summary>
    internal class PageVpMod
    {

        public PageVpMod()
        {

        }

        /// <summary>
        /// Pages the vp mod delete.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>Успех</returns>
        public bool PageVpModDel(PdfPage page)
        {
            _isErr = false;
            _page = page;
            _arrVP = Page.Elements.GetObject("/VP") as PdfArray;

            if (ArrVP != null)//VP yes
            {
                try
                {
                    Page.Elements.Remove("/VP");
                    _mesag = "VP удален";
                    return true;

                }
                catch (Exception ex)
                {
                    _mesag = ex.Message;
                    _isErr = true;
                    return false;
                }
            }
            else
            {
                _mesag = "VP отсутствует";
                return false;
            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageVpMod"/> class.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="convertUnit">The convert unit.</param>
        /// <param name="isModified">true - Add or Modifi<br>false - only Add</br> </param>           
        public bool PageVpModAdd(PdfPage page,
                        XGraphicsUnit convertUnit = XGraphicsUnit.Millimeter,
                        bool isModified = false)
        {
            _page = page;

            XUnit unit = new XUnit(1, convertUnit);

            _scalefactor = 1 / unit.Point;//пересчет единиц в точку
            //todo обработка режимов isModified
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
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (isModified)//заказано изменить VP scale factor
                {
                    if (ModArrVP())
                    {
                        _mesag = "VP изменен в";

                        return true;
                    }
                    else
                    {
                        _mesag = "VP не удалось изменить";

                        return false;
                    }
                }
                else
                {
                    _mesag = "VP существует в";
                    return false;
                }
                #endregion 
            }
            catch (Exception ex)
            {
                _mesag = ex.Message;
                _isErr = true;
                return false;
            }
        }

        bool ModArrVP()
        {
            //PdfPage page = pdfDoc.Pages[0];

            //PdfArray arrVP = page.Elements.GetObject("/VP") as PdfArray;
            PdfDictionary dicVPitem = null;
            foreach (PdfDictionary item in ArrVP.Elements)
            {
                dicVPitem = item.Elements.GetObject("/Measure1") as PdfDictionary; ;
                if (dicVPitem is PdfDictionary)
                {
                    break;
                }
            }

            //dicVPitem = ArrVP.Elements.Items[0] as PdfDictionary;


            //PdfArray dicBBox = dicVPitem.Elements.GetValue("/BBox") as PdfArray;

            PdfDictionary dicMeasure = dicVPitem.Elements.GetObject("/Measure") as PdfDictionary;

            //PdfArray arrA = dicMeasure.Elements.GetValue("/A") as PdfArray;

            //PdfArray arrD = dicMeasure.Elements.GetValue("/D") as PdfArray;

            //PdfString arrR = dicMeasure.Elements.GetValue("/R") as PdfString;

            //PdfName nameSubtype = dicMeasure.Elements.GetValue("/Subtype") as PdfName;

            //PdfName nameType = dicMeasure.Elements.GetValue("/Type") as PdfName;

            PdfArray arrX = dicMeasure.Elements.GetValue("/X") as PdfArray;



            PdfDictionary dicC = arrX.Elements.Items[0] as PdfDictionary;

            //PdfReal ConversionFactor = dicC.Elements.GetValue("/C") as PdfReal;//(0.35278) собственно искомое

            //ставим желаемое значение
            dicC.Elements.SetValue("/C", new PdfReal(Scalefactor));

            //PdfName dicType = dicVPitem.Elements.GetValue("/Type") as PdfName;

            return true;
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

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public PdfPage Page => _page;

        /// <summary>
        /// Gets the arr view port.
        /// </summary>
        /// <value>
        /// The arr view port.
        /// </value>
        public PdfArray ArrVP => _arrVP;

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Mesag => _mesag;


        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
        public bool IsErr => _isErr;

        /// <summary>
        /// Gets the scale factor.
        /// </summary>
        /// <value>
        /// The scale factor.
        /// </value>
        public double Scalefactor => _scalefactor;



        PdfPage _page;
        PdfArray _arrVP;
        //bool _isChanged;
        bool _isErr;
        string _mesag;
        double _scalefactor;

    }


}
