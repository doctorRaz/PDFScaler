using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace drz.PdfSharp_ConversionFactor
{
    internal class ServiseChangeConversionFactor
    {
        /// <summary>
        /// Служебные смена scale factor
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="dScale">The d scale.</param>
        public static void ChangeCF(PdfDocument document,double dScale)
        {
             // Iterate the pages.
            foreach (PdfPage page in document.Pages)
            {
                //"[/VP, [ << /BBox [ 0 0 1190 841 ] /Measure << /A [ << /C 1 /U ( ) >> ] /D [ << /C 1 /U ( ) >> ] /R ( ) /Subtype /RL /Type /Measure /X [ << /C 0.353 /U ( ) >> ] >> /Type /Viewport >> ]]"
                //==================================================
                // "[/VP, [ << /BBox [ 49 0 1140 841 ] /Measure << /A [ << /C 1 /U ( ) >> ] /D [ << /C 1 /U ( ) >> ] /R ( ) /Subtype /RL /Type /Measure /X [ << /C 0.353 /U ( ) >> ] >> /Type /Viewport >> ]]"
                //==================================================

                PdfDictionary.DictionaryElements dicPageElments = page.Elements;

                PdfArray arrBBox = dicPageElments.GetObject("/VP") as PdfArray;
                PdfArray.ArrayElements arrBBoxElements = arrBBox.Elements;
                PdfDictionary dicBBoxElements = arrBBoxElements.Items[0] as PdfDictionary;
                PdfDictionary dicMeasure = dicBBoxElements.Elements.GetObject("/Measure") as PdfDictionary;
                PdfArray arX = dicMeasure.Elements.GetValue("/X") as PdfArray;
                PdfDictionary dicC = arX.Elements.Items[0] as PdfDictionary;
                PdfReal ConversionFactor = dicC.Elements.GetValue("/C") as PdfReal;//(0.35278) собственно искомое

                dicC.Elements.Remove("/C");//поменять не получилось значение, поэтому варварски через удаление
                PdfReal real = new PdfReal(dScale);
                dicC.Elements.Add("/C", real);//ставим новое значение
 
            }
        }

        /// <summary>
        /// смена масштаба шаблона
        /// </summary>
        /// <param name="pdftemp">The pdftemp.</param>
        public void sss(string pdftemp)
        {
            //смена масштаба

            PdfDocument document = PdfReader.Open(pdftemp);
            ChangeCF(document, 0.3527777778);
            document.Save(pdftemp);
        }

        /// <summary>
        /// Pdfnews the add scale.
        /// </summary>
        public void PdfnewAddScale()
        {
            TemplateConversionFactor tmp = new TemplateConversionFactor( );

            //x пустой с настройками
            PdfDocument pdfdoc = new PdfDocument();
            pdfdoc.AddPage();
            PdfDictionary.DictionaryElements p = pdfdoc.Pages[0].Elements;
            p.Add("/VP", tmp.ArrVP);

            pdfdoc.Save("test.tmp");
        }

    }
}
