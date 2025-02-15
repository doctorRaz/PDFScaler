
using System.Collections.Generic;
using System.ComponentModel;

using drz.PdfVpMod.Abstractions.Interfaces;
using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Servise;

using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace drz.PdfVpMod.PdfSharp
{
    /// <summary>
    /// Движок
    /// </summary>
    public class PdfManager
    {
        #region INIT

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfManager" /> class.
        /// <br>Default unit Millimeter <see cref="XGraphicsUnit.Millimeter" /></br>
        /// </summary>
        /// <param name="logger">The logger.</param>
        public PdfManager(List<ILogger> logger)
        {
            Logger = logger;
            _convertUnit = XGraphicsUnit.Millimeter;
            ChangeVpPage = ModeChangVp.Add;
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="PdfManager" /> class.
        ///// </summary>
        ///// <param name="logger">The logger.</param>
        ///// <param name="convertUnit">The convert unit.</param>
        ///// <param name="changeVpPage"></param>
        //public PdfManager(List<ILogger> logger,
        //                  XGraphicsUnit convertUnit,
        //                  ModeChangVp changeVpPage = ModeChangVp.Add)
        //{
        //    Logger = logger;
        //    _convertUnit = convertUnit;
        //    ChangeVpPage = changeVpPage;
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfManager" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="convertUnit">The convert unit.</param>
        /// <param name="changeVpPage"></param>
        public PdfManager(List<ILogger> logger,
                          WinGraphicsUnit convertUnit,
                          ModeChangVp changeVpPage = ModeChangVp.Add)
        {
            Logger = logger;
            WinConvertUnit = convertUnit;
            ChangeVpPage = changeVpPage;
        }
        #endregion

        /// <summary>
        /// PDFs обработка.
        /// </summary>
        /// <param name="PdfFiles">The PDF files.</param>
        public void PdfRun(string[] PdfFiles)
        {
            //новый филер документов
            PdfFiler Filer = new PdfFiler(Logger);

            //новый конвертер
            PdfConversion Conv = new PdfConversion(Logger);

            //по списку файлов
            foreach (string pdffile in PdfFiles)
            {
                //получаем документ
                if (!Filer.PdfOpen(pdffile))//документа нет пропуск
                {
                    continue;
                }

                PdfDocument pdfDoc = Filer.PdfDoc;

                if (Conv.ConversionRun(Filer.PdfDoc,//PDF doc
                                      ConvertUnit,//единицы
                                      ChangeVpPage)//режим только добавление VP
                    )// если хоть один VP добавлен
                {
                    Filer.PdfSave(pdfDoc);//сохраняем                   
                }
                else//ни один VP не добавлен, сохранять не надо
                {
                    logItem = new Logger($"Изменений нет. Файл не сохранен: {pdffile}", MesagType.Info);
                    Logger.Add(logItem);
                }
            }
        }

        #region Environ        

        ModeChangVp ChangeVpPage;

        ILogger logItem;

        List<ILogger> Logger;

        XGraphicsUnit _convertUnit;
        XGraphicsUnit ConvertUnit => _convertUnit;

        WinGraphicsUnit WinConvertUnit
        {
            set
            {
                switch (value)
                {
                    case WinGraphicsUnit.Centimeter: _convertUnit = XGraphicsUnit.Centimeter; break;
                    case WinGraphicsUnit.Inch: _convertUnit = XGraphicsUnit.Inch; break;
                    case WinGraphicsUnit.Millimeter: _convertUnit = XGraphicsUnit.Millimeter; break;
                    case WinGraphicsUnit.Point: _convertUnit = XGraphicsUnit.Point; break;
                    case WinGraphicsUnit.Presentation: _convertUnit = XGraphicsUnit.Presentation; break;
                    default: throw new InvalidEnumArgumentException();
                }
            }
        }

        #endregion
    }
}


