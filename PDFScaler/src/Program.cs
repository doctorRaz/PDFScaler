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
            

           

            TemplateConversionFactor tmp = new TemplateConversionFactor( );
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
      
         



             
        }
    }
}
