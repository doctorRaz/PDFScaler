using System;
using System.IO;
using System.Reflection;
using System.Windows.Interop;

using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace drz.PdfSharp_ConversionFactor
{
    /*PDF documents, such as those created by CAD software, may contain graphics that are intended to represent
     * real-world objects. Users of such documents often require information about the scale and units of
     * measurement of the corresponding real-world objects and their relationship to units in PDF user space. */

    /*Документы PDF, например, созданные с помощью программного обеспечения CAD, могут содержать графические
     * изображения, предназначенные для представления объектов реального мира. Пользователям таких документов 
     * часто требуется информация о масштабе и единицах измерения соответствующих объектов реального 
     * мира и их соотношении с единицами измерения в пользовательском пространстве PDF.*/

    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Assembly asm = Assembly.GetExecutingAssembly();
            //string sAsmDir =Path.GetDirectoryName( asm.Location);
            //string spath=Path.GetDirectoryName(typeof(userUnit).Assembly.Location);
            

           

            TemplateConversionFactor tmp = new TemplateConversionFactor(pdftemp);
            if (!tmp.Istmp)
            {
                Console.WriteLine("Файл шаблона пустой, уходим");
                return;
            }

            COFD fsd = new COFD//save
            {
                Title = "Выберите каталог для напечатанных файлов",
                Multiselect = false, //тут всегда один каталог _data.CHKmultiSelect,
                //InitialDirectory = _dsf.TBLfolderOut,
                IsFolderPicker = true,
            };
            //!хэндл Wpf окна
            //IntPtr wpfHandle = new WindowInteropHelper(window).Handle;
            if (!fsd.ShowDialog(/*wpfHandle*/))
            {

            }

            string PdfFile = @"d:\@Developers\В работе\!Текущее\Programmers\!NET\Console_TEST\@res\exampl2\NOT_DESIGNATION.pdf";

            ConversionFactor.SetPagesConversionFactor(PdfFile, tmp);




            Console.ReadKey();
            return;
            //смена масштаба
            PdfDocument document = PdfReader.Open(pdftemp);
            ChangeConversionFactor.ChangeCF(document, 0.3527777778);
            document.Save(pdftemp);
            document.Close();

            //foreach (KeyValuePair<string, PdfItem?> item in dicPageElments)
            //{
            //    if (item.Key == "/VP")
            //    {
            //        var itt = item.Value as PdfSharp.Pdf.PdfArray;
            //        var ie = itt.Elements;
            //        var ii = ie.Items[0] as PdfDictionary;


            //        foreach (var it in ie)
            //        {

            //        }
            //    }
            //}
            //PdfItem? va = dicPageElments.GetValue("/VP");
            //var sV = page.Elements.Values;
            //// Get the resources dictionary.
            //var resources = page.Elements.GetDictionary("/Resources");
            //var vp = page.Elements.GetDictionary("/vp");
            //var Contents = page.Elements.GetDictionary("/U");



            //if (resources == null)
            //    continue;

            //// Get the external objects dictionary.
            //var xObjects = resources.Elements.GetDictionary("/XObject");
            //if (xObjects == null)
            //    continue;

            //var items = xObjects.Elements.Values;
            //// Iterate the references to external objects.
            //foreach (var item in items)
            //{
            //    var reference = item as PdfReference;
            //    if (reference == null)
            //        continue;

            //    var xObject = reference.Value as PdfDictionary;
            //    // Is external object an image?
            //    if (xObject != null && xObject.Elements.GetString("/Subtype") == "/Image")
            //    {
            //        //  ExportImage(xObject, ref imageCount);
            //    }
            //}




            //test пустой с настройками
            PdfDocument pdfdoc = new PdfDocument();
            pdfdoc.AddPage();
            PdfDictionary.DictionaryElements p = pdfdoc.Pages[0].Elements;
            p.Add("/VP", tmp.arrBBox);

            pdfdoc.Save("test.tmp");















            PdfDocument outputDocument = new PdfDocument();
            outputDocument = PdfReader.Open(PdfFile, PdfDocumentOpenMode.Modify);
            PdfPage pageOut = outputDocument.Pages[0];
            XGraphics gfxOut = XGraphics.FromPdfPage(pageOut);

            var boxOut = pageOut.MediaBox.ToXRect();

            //***
            PdfDocument inputDocument = new PdfDocument();
            inputDocument = PdfReader.Open(pdftemp, PdfDocumentOpenMode.Modify);
            PdfPage pageIn = inputDocument.Pages[0];
            var boxIn = pageIn.MediaBox.ToXRect();
            var elIn = pageIn.Elements;
            XGraphics gfxIn = XGraphics.FromPdfPage(pageIn);

            gfxOut = gfxIn;

            //===


            //var h = page0.Height;
            //var w = page0.Width;
            //page0.Height = XUnit.FromMillimeter(h);
            //page0.Width = XUnit.FromMillimeter(w);
            //outputDocument.Version = 17;


            //inputDocument.Save(pdf);




            //foreach (string patch in patchs)
            //{
            //    XGraphics gfs = testc(patch);
            //    gfxs.Add(gfs);
            //    PdfDocument doc = PDFdoc(patch);
            //    Docs.Add(doc);

            //    PdfPage page = PDFPage(patch);
            //    Pages.Add(page);
            //}



            //Console.ReadKey();
        }
    }
}
