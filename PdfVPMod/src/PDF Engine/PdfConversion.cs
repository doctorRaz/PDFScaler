
using System.Collections.Generic;

using drz.PdfVpMod.Abstractions.Interfaces;
using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Servise;

using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace drz.PdfVpMod.PdfSharp
{
    internal class PdfConversion
    {
        #region INIT       

        /// <summary>
        /// Настройка конвертора VP
        /// </summary>
        /// <param name="logger">The logger.</param>
        public PdfConversion(List<ILogger> logger)
        {
            Logger = logger;
        }
        #endregion

        /// <summary>
        /// Обработка документаn.
        /// </summary>
        /// <param name="PdfDoc">PDF document.</param>
        /// <param name="convertUnit">The convert unit.</param>
        /// <param name="changeVpPage">if set to <c>true</c> [change vp page].</param>
        /// <returns>
        /// успех
        /// </returns>
        public bool ConversionRun(PdfDocument PdfDoc,
                           XGraphicsUnit convertUnit,
                           ModeChangVp changeVpPage = ModeChangVp.Add)
        {
            _convertUnit = convertUnit;
            ChangeVpPage = changeVpPage;
            _isModified = false;

            int pageNum = 0;//номер стр для логера

            PageVpMod PVM = new PageVpMod();
            //перебор страниц
            foreach (PdfPage page in PdfDoc.Pages)
            {
                if (ChangeVpPage == ModeChangVp.Del)//Delete VP
                {
                    if (PVM.PageVpModDel(page))
                    {
                        _isModified = true;//если меняли хоть один лист
                        _logItem = new Logger($"\t{PVM.Mesag} Page:{++pageNum}", MesagType.Ok);
                    }
                    else
                    {
                        if (PVM.IsErr)//Page not chang exept
                        {
                            _isModified = false;
                            _logItem = new Logger($"\tVP сбой в Page:{++pageNum} {PVM.Mesag}", MesagType.Error);
                        }
                        else//Page not chang exist
                        {
                            _isModified = false;
                            _logItem = new Logger($"\t{PVM.Mesag} Page:{++pageNum}", MesagType.Info);
                        }
                    }
                }
                else
                {
                    bool isModified = false;
                    if (ChangeVpPage == ModeChangVp.AddOrMod)
                    { isModified = true; }

                    bool IsChanged = PVM.PageVpModAdd(page, ConvertUnit, isModified);

                    if (IsChanged)//Page chang
                    {
                        _isModified = true;//если меняли хоть один лист
                        _logItem = new Logger($"\t{PVM.Mesag} Page:{++pageNum}", MesagType.Ok);
                    }
                    else//Page not chang
                    {
                        if (PVM.IsErr)//Page not chang exept
                        {
                            _isModified = false;
                            _logItem = new Logger($"\tVP сбой в Page:{++pageNum} {PVM.Mesag}", MesagType.Error);
                        }
                        else//Page not chang exist
                        {
                            _isModified = false;
                            _logItem = new Logger($"\t{PVM.Mesag} Page:{++pageNum}", MesagType.Info);
                        }
                    }
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


        #region ENVIRON

        #region Servise

        ILogger _logItem;

        /// <summary>
        /// The logger
        /// </summary>
        public List<ILogger> Logger;

        ModeChangVp ChangeVpPage;

        #endregion

        #region VP




        XGraphicsUnit _convertUnit;
        XGraphicsUnit ConvertUnit => _convertUnit;

        #endregion
        #region PDF

        //PdfPage _page;
        //PdfPage Page => _page;

        //PdfDocument _pdfDoc;


        //public PdfDocument PdfDoc => _pdfDoc;

        #endregion

        /// <summary>
        /// The is PDF modified
        /// </summary>
        public bool IsModified => _isModified;

        bool _isModified;

        #endregion




        //        #region EXAMPLE
        //#if DEBUG
        //        /// <summary>
        //        /// Adds the VP example.
        //        /// </summary>
        //        /// <param name="pdfDoc">The PDF document.</param>
        //        public void GetSF(PdfDocument pdfDoc)
        //        {

        //            PdfPage page = pdfDoc.Pages[0];

        //            PdfArray arrVP = page.Elements.GetObject("/VP") as PdfArray;
        //            PdfDictionary dicVPitem = arrVP.Elements.Items[0] as PdfDictionary;

        //            PdfArray dicBBox = dicVPitem.Elements.GetValue("/BBox") as PdfArray;

        //            PdfDictionary dicMeasure = dicVPitem.Elements.GetObject("/Measure") as PdfDictionary;

        //            PdfArray arrA = dicMeasure.Elements.GetValue("/A") as PdfArray;

        //            PdfArray arrD = dicMeasure.Elements.GetValue("/D") as PdfArray;

        //            PdfString arrR = dicMeasure.Elements.GetValue("/R") as PdfString;

        //            PdfName nameSubtype = dicMeasure.Elements.GetValue("/Subtype") as PdfName;

        //            PdfName nameType = dicMeasure.Elements.GetValue("/Type") as PdfName;

        //            PdfArray arrX = dicMeasure.Elements.GetValue("/X") as PdfArray;

        //            PdfDictionary dicC = arrX.Elements.Items[0] as PdfDictionary;

        //            PdfReal ConversionFactor = dicC.Elements.GetValue("/C") as PdfReal;//(0.35278) собственно искомое

        //            PdfName dicType = dicVPitem.Elements.GetValue("/Type") as PdfName;

        //        }

        //#endif
        //        #endregion
    }

}
