using System;
using System.CodeDom;
using System.Linq;

using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace drz.PdfVpMod.PdfSharp
{
    /// <summary>
    /// Добавляет удаляет VP с масштабом вида
    /// </summary>
    internal class PageVpMod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageVpMod"/> class.
        /// </summary>
        public PageVpMod()
        {

        }

        /// <summary>
        /// Pages the VP mod delete.
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
        /// <param name="isModified">true - Add or Modify<br>false - only Add</br> </param>           
        public bool PageVpModAdd(PdfPage page,
                        XGraphicsUnit convertUnit = XGraphicsUnit.Millimeter,
                        bool isModified = false)
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
                        _mesag = "VP не изменен в";

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

        /// <summary>
        /// Modifies the scale VP.
        /// </summary>
        /// <returns>Успех</returns>
        bool ModArrVP()
        {
            bool isMod=false;
            try
            {
                PdfDictionary dicMeasure = null;
                foreach (PdfDictionary item in ArrVP.Elements.Cast<PdfDictionary>())
                {
                    dicMeasure = item.Elements.GetObject("/Measure") as PdfDictionary;
                    if (dicMeasure is PdfDictionary)
                    {
                        break;
                    }
                }

                PdfArray arrX = dicMeasure.Elements.GetValue("/X") as PdfArray;

                foreach (PdfDictionary item in arrX.Elements.Cast<PdfDictionary>())
                {
                    PdfReal ConversionFactor = item.Elements.GetValue("/C") as PdfReal;
                    if (ConversionFactor is PdfReal)
                    {
                        var dd = ConversionFactor.Value;
                        var dr = Math.Round(dd, 5);
                        var sr = Math.Round(Scalefactor, 5);
                        if (dr != sr)
                        {
                            item.Elements.SetValue("/C", new PdfReal(Scalefactor));
                            isMod= true;
                            break;
                        }
                        else
                        {
                            isMod= false;
                            break;
                        }
                    }
                }
                return isMod;
            }
            catch (Exception ex)
            {
                _mesag += ex.Message;
                _isErr = true;
                return false;
            }
        }

        /// <summary>
        /// Adds the scale vp.
        /// </summary>
        /// <returns>Успех</returns>
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
