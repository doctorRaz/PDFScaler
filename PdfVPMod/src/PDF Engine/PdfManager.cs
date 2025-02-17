
using System.Collections.Generic;
using System.ComponentModel;

using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Interfaces;
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
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="sets"></param>
        public PdfManager(List<ILogger> logger,
                         Setting sets)
        {
            Logger = logger;
            _sets = sets;
        }
        #endregion

        /// <summary>
        /// PDFs обработка.
        /// </summary>
        /// <param name="pdfFiles">The PDF files.</param>
        public void PdfRun(List<string> pdfFiles)
        {
            //новый филер документов
            PdfFiler Filer = new PdfFiler(Logger);

            //новый конвертер
            PdfConversion Conv = new PdfConversion(Logger, Sets);

            //по списку файлов
            foreach (string pdffile in pdfFiles)
            {
                //получаем документ
                if (!Filer.PdfOpen(pdffile))//документа нет пропуск
                {
                    continue;
                }

                PdfDocument pdfDoc = Filer.PdfDoc;

                if (Conv.ConversionRun(Filer.PdfDoc))//режим только добавление VP
                {
                    Filer.PdfSave(pdfDoc, Sets.AddBak);//сохраняем                   
                }
                else//ни один VP не добавлен, сохранять не надо
                {
                    Logger.Add(new Logger($"Изменений нет. Файл не сохранен: {pdffile}", MesagType.Info));
                }
            }
        }

        #region Environ        

        Setting _sets;

        Setting Sets => _sets;

        List<ILogger> Logger;


        //ModeChangVp ChangeVpPage => Sets.Mode;

        //XGraphicsUnit _convertUnit;
        //XGraphicsUnit ConvertUnit => _convertUnit;

        //WinGraphicsUnit WinConvertUnit
        //{
        //    set
        //    {
        //        switch (value)
        //        {
        //            case WinGraphicsUnit.Centimeter: _convertUnit = XGraphicsUnit.Centimeter; break;
        //            case WinGraphicsUnit.Inch: _convertUnit = XGraphicsUnit.Inch; break;
        //            case WinGraphicsUnit.Millimeter: _convertUnit = XGraphicsUnit.Millimeter; break;
        //            case WinGraphicsUnit.Point: _convertUnit = XGraphicsUnit.Point; break;
        //            case WinGraphicsUnit.Presentation: _convertUnit = XGraphicsUnit.Presentation; break;
        //            default: throw new InvalidEnumArgumentException();
        //        }
        //    }
        //}

        #endregion
    }
}


