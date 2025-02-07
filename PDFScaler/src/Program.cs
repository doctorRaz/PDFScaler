using System;
using System.Windows;
using System.Windows.Forms;

using drz.Abstractions.Interfaces;
using drz.Infrastructure;
using drz.Servise;


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

            ConsoleKey response;

            IConsoleService CS = new ConsoleService();

            TemplateConversionFactor tmp = new TemplateConversionFactor();
            if (!tmp.Istmp)
            {
                CS.ConsoleMsg("Press any Key");
                response = Console.ReadKey().Key;
                return;
            }
            new Mesag();//сообщения

            do
            {
                new Engine(tmp);//движок

                Console.Write("Хотите повторить \"Yes\" ");
                response = Console.ReadKey(/*false*/).Key;
                if (response == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    //Console.Clear();
                }
            } while (response == ConsoleKey.Y);

        }
    }
}
