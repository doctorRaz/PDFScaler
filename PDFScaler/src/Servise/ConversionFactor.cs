using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.Abstractions.Interfaces;
using drz.Infrastructure;
using drz.Servise;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace drz.PdfSharp_ConversionFactor
{
    internal class ConversionFactor
    {
        TemplateConversionFactor _tmp;
        TemplateConversionFactor tmp => _tmp;

        string _mesag;
        public string Mesag => _mesag;
        public ConversionFactor(TemplateConversionFactor tmp)
        {
            _tmp = tmp;
        }

        public bool SetPagesConversionFactor(string PdfFile)
        {
            bool IsModifed = false;
            IConsoleService CS = new ConsoleService();
            PdfDocument PdfDoc = new PdfDocument();
            try
            {
                PdfDoc = PdfReader.Open(PdfFile, PdfDocumentOpenMode.Modify);
            }
            catch (Exception ex)
            {
                IsModifed = false;
                CS.ConsoleMsg($"{ex.Message}: {PdfFile}", WConsoleColor.White, WConsoleColor.DarkRed);
                return false;
            }

            CS.ConsoleMsg($"Open: {PdfFile}", WConsoleColor.Gray);
            int pageNum = 0;
            foreach (PdfPage page in PdfDoc.Pages)
            {
                PdfDictionary.DictionaryElements PdfDicEl = page.Elements;
                PdfArray arrBBox1 = PdfDicEl.GetObject("/VP") as PdfArray;

                if (arrBBox1 == null)
                {
                    PdfDicEl.Add("/VP", tmp.arrVP);
                    IsModifed = true;//если меняли хоть один лист
                    CS.ConsoleMsg($"Словарь добавлен Page:{++pageNum}", WConsoleColor.Green);
                }
                else
                {
                    CS.ConsoleMsg($"Словарь существует Page:{++pageNum}", WConsoleColor.DarkRed);
                }
            }
            try
            {
                if (IsModifed)
                {
                    PdfSawer pds = new PdfSawer();

                    PdfDoc.Save(PdfFile); //todo в отдельный класс
                    CS.ConsoleMsg($"Saved: {PdfFile}", WConsoleColor.Gray);
                    return true;
                }
                else
                {
                    CS.ConsoleMsg($"Not changed: {PdfFile}", WConsoleColor.DarkRed);
                    return false ;
                }

            }
            catch (Exception ex)
            {
                CS.ConsoleMsg(ex.Message, WConsoleColor.White, WConsoleColor.DarkRed);
                return false;
            }
        }

    }
}
