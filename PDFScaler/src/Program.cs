using System;

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
           
            TemplateConversionFactor tmp = new TemplateConversionFactor( );
            if (!tmp.Istmp)
            {
                Console.WriteLine("Файл шаблона пустой, уходим");
                Console.ReadKey();
                return;
            }

            

            string PdfFile = @"d:\@Developers\В работе\!Текущее\Programmers\!NET\Console_TEST\@res\exampl2\NOT_DESIGNATION.pdf";

            ConversionFactor.SetPagesConversionFactor(PdfFile, tmp);



            Console.WriteLine("Press any key");
            Console.ReadKey();
      
         



             
        }
    }
}
