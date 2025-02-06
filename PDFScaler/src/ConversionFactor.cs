using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace drz.PdfSharp_ConversionFactor
{
    internal class ConversionFactor
    {
        public static void SetPagesConversionFactor(string PdfFile, TemplateConversionFactor tmp)
        {
            PdfDocument PdfDoc = PdfReader.Open(PdfFile, PdfDocumentOpenMode.Modify);
            Console.WriteLine($"Open: {PdfFile}");
            int pageNum = 0;
            bool IsModifed = false;
            foreach (PdfPage page in PdfDoc.Pages)
            {
                PdfDictionary.DictionaryElements PdfDicEl = page.Elements;
                PdfArray arrBBox1 = PdfDicEl.GetObject("/VP") as PdfArray;

                if (arrBBox1 == null)
                {
                    PdfDicEl.Add("/VP", tmp.arrBBox);
                    IsModifed = true;//если меняли хоть один лист
                    Console.WriteLine($"Added Dict (/VP) Page:{++pageNum}");
                }
                else
                {
                    Console.WriteLine($"Not added Dict (/VP) Page:{++pageNum}");
                }
            }
            try
            {
                if (IsModifed)
                {
                    PdfDoc.Save(PdfFile); //todo в отдельный класс
                    Console.WriteLine($"Saved: {PdfFile}");
                }
                else
                {
                    Console.WriteLine($"Not changed: {PdfFile}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
