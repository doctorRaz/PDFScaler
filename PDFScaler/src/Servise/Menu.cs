using System;
using System.Collections.Generic;
using System.IO;

using drz.PDFScaler.Infrastructure;
using drz.PDFScaler.Interfaces;
using drz.PdfVpMod.Enum;

using IWshRuntimeLibrary;



namespace drz.PDFScaler.Servise
{
    /// <summary>
    /// меню
    /// </summary>
    public static class UiMenu
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        public static bool Create()
        {
            IConsoleService CS = new ConsoleService();//цветная консоль

            #region Title
            //CS.ConsoleWriteLine($"{DataSetWpfOpt.Product} {DataSetWpfOpt.VersionFull}\n", MesagType.Ok);
            #endregion

            #region License
            //todo перевести лицензию
            CS.ConsoleWriteLine("\t\tApache License\n" +
                "\tVersion 2.0, January 2004\n", MesagType.Ok);
            CS.ConsoleWriteLine($"   Copyright(C) {DataSetWpfOpt.Trademark} by {DataSetWpfOpt.Copyright}\n", MesagType.Ok);

            CS.ConsoleWrite("   Лицензировано в соответствии с Apache License, Version 2.0 (\"Лицензия\");\n" +
                "   Вы не можете использовать этот файл, кроме как в соответствии с Лицензией.\n" +
                "   Вы можете получить копию Лицензии по адресу: ", MesagType.None);
            CS.ConsoleWriteLine("http://www.apache.org/licenses/LICENSE-2.0", MesagType.Ok);

            CS.ConsoleWriteLine("\n   Если применимое законодательство не требует иного или это не согласовано в письменной форме,\n" +
                "   программное обеспечение, распространяемое по Лицензии, распространяется на условиях ", MesagType.None);
            CS.ConsoleWrite("   \"КАК ЕСТЬ\", БЕЗ ГАРАНТИЙ ИЛИ УСЛОВИЙ ЛЮБОГО РОДА, ", MesagType.Warning);
            CS.ConsoleWriteLine("как явных, так и подразумеваемых.\n" +
                "   См.Лицензию для получения информации о конкретных языках, регулирующих разрешения\n" +
                "   и ограничения в соответствии с Лицензией.", MesagType.None);
            CS.ConsoleWriteLine("   ========================================================================================", MesagType.None);
            #endregion
            #region About
            CS.ConsoleWriteLine("\tНазначение програмы:", MesagType.Ok);
            CS.ConsoleWriteLine($"   {DataSetWpfOpt.Product} изменяет содержимое файлов PDF, добавляя к каждой странице PDF файла,\nвидовой экран с заданным масштабом измерений.");
            CS.ConsoleWriteLine("   В результате подложки PDF вставленные внешними ссылками будут иметь одинаковый масштаб\nв AutoCAD и nanoCAD.");
            CS.ConsoleWriteLine("   После обработки всех файлов будут созданы резервные копии оригинальных файлов *.bak\n(настройка по умолчанию)");
            CS.ConsoleWriteLine("!!!!Обрабатываемые файлы не должны быть открыты для просмотра и не загружены в чертеж,\nкак внешние ссылки", MesagType.Warning);
            CS.ConsoleWrite("Больше информации: ", MesagType.None);
            CS.ConsoleWriteLine("https://doctorraz.blogspot.com/2025/02/pdfscaler-autocad-nanocad.html", MesagType.Ok);
            CS.ConsoleWriteLine("   ========================================================================================", MesagType.None);
            #endregion

            #region Config XML
            CS.ConsoleWriteLine("\tНастройки:", MesagType.Ok);
            CS.ConsoleWriteLine($"При каждом запуске {DataSetWpfOpt.Product} считывает конфигурационный файл: ");
            CS.ConsoleWriteLine($"<{Config.XMLpatch}>", MesagType.Ok);
            CS.ConsoleWriteLine($"Оции запуска {DataSetWpfOpt.Product} можно изменить в текстовом редакторе.\n");
            
            #endregion

            #region Using
            CS.ConsoleWriteLine("\t!!!Опции командной строки разово переопределяют конфигурационный файл!!!",MesagType.Warning);
            CS.ConsoleWriteLine($"Опции командной строки: {DataSetWpfOpt.Product} [options] filenames", MesagType.Ok);
            Console.WriteLine("Опции:");

            Console.WriteLine("\tизменение масштаба видового экрана (VP) страницы:");
            Console.WriteLine("\t\t-mm\tМиллиметры (значение по умолчанию)");
            Console.WriteLine("\t\t-in\tДюймы");
            Console.WriteLine("\t\t-pt\tПойнты-точки");
            Console.WriteLine("\t\t-cm\tСантиметры");
            Console.WriteLine("\t\t-pr\tЕдиницы презентации");
            Console.WriteLine("");

            Console.WriteLine("\tдобавление/удаление/изменение VP страницы:");
            Console.WriteLine("\t\t-add\tДобавляет VP (значение по умолчанию)");
            Console.WriteLine("\t\t-del\tУдаляет VP");
            Console.WriteLine("\t\t-mod\tДобавляет VP или изменяет единицы существующего");
            Console.WriteLine("");

            Console.WriteLine("\tсохранение PDF:");
            Console.WriteLine("\t\t-bakon\tсоздается копия оригинального PDF (*.bak) (значение по умолчанию)");
            Console.WriteLine("\t\t-bakoff\tоригинальный файл перезаписывается");
            Console.WriteLine("");

            Console.WriteLine("\tрежим закрытия окна консоли:");
            Console.WriteLine("\t\t-exon\tтребуется подтверждения (значение по умолчанию)");
            Console.WriteLine($"\t\t-exoff\tподтверждение требуется только если в работе {DataSetWpfOpt.Product} были ошибки");
            //Console.WriteLine("");

            CS.ConsoleWriteLine("   ========================================================================================", MesagType.None);
            #endregion

            //CS.ConsoleWriteLine("Инструкция:", MesagType.Ok);

            CS.ConsoleWriteLine($"В этом меню отображается опция создания или удаления ссылки на ярлыки {DataSetWpfOpt.Product} в \"Send to\"...\n", MesagType.Ok);

            string sendto = Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
            string pathPdfAdd = Path.Combine(sendto, $"{DataSetWpfOpt.Product} ADD.lnk");
            string pathPdfMod = Path.Combine(sendto, $"{DataSetWpfOpt.Product} МОDIFY.lnk");
            string pathPdfDel = Path.Combine(sendto, $"{DataSetWpfOpt.Product} DELETE.lnk");


            if (System.IO.File.Exists(pathPdfAdd))
            {
                CS.ConsoleWrite($"Вы хотите удалить ярлыки {DataSetWpfOpt.Product} из 'SEND TO'? [Y]-да, другая клавиша продолжить: ", MesagType.Warning);

                ConsoleKey response = Console.ReadKey().Key;
                Console.WriteLine("");

                if (response == ConsoleKey.Y)
                {
                    List<string> paths = new List<string>() { pathPdfAdd, pathPdfMod, pathPdfDel };
                    foreach (string path in paths)
                    {
                        try
                        {
                            System.IO.File.Delete(path);
                            CS.ConsoleWriteLine($"Ярлык {Path.GetFileName(path)} удален", MesagType.Info);
                        }
                        catch (Exception ex)
                        {
                            CS.ConsoleWriteLine(ex.Message, MesagType.Error);
                        }
                    }
                    CS.ConsoleWriteLine("Для выхода нажмите любую клавишу...", MesagType.Info);
                    Console.ReadKey();

                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                CS.ConsoleWrite($"Вы можете добавить ярлыки {DataSetWpfOpt.Product} в 'SEND TO'? [Y]-да, другая клавиша продолжить: ", MesagType.Ok);
                ConsoleKey response = Console.ReadKey().Key;
                Console.WriteLine("");

                if (response == ConsoleKey.Y)
                {
                    string targetPath = Path.Combine(Path.GetDirectoryName(DataSetWpfOpt.AsmFulPath), $"{DataSetWpfOpt.AsmFileNameWithoutExtension}.exe");

                    //Add
                    WshShell shell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(pathPdfAdd);
                    shortcut.Description = $"Ярлык {DataSetWpfOpt.Product} добавить VP";
                    shortcut.TargetPath = targetPath;
                    shortcut.Save();
                    CS.ConsoleWriteLine($"Ярлык \"{Path.GetFileName(pathPdfAdd)}\" добавлен", MesagType.Info);

                    //Mod
                    shell = new WshShell();
                    shortcut = (IWshShortcut)shell.CreateShortcut(pathPdfMod);
                    shortcut.Description = $"Ярлык {DataSetWpfOpt.Product} изменить VP";
                    shortcut.TargetPath = targetPath;
                    shortcut.Arguments = "-mod";
                    shortcut.Save();
                    CS.ConsoleWriteLine($"Ярлык \"{Path.GetFileName(pathPdfMod)}\" добавлен", MesagType.Info);

                    //Delete
                    shell = new WshShell();
                    shortcut = (IWshShortcut)shell.CreateShortcut(pathPdfDel);
                    shortcut.Description = $"Ярлык {DataSetWpfOpt.Product} удалить VP";
                    shortcut.TargetPath = targetPath;
                    shortcut.Arguments = "-del";
                    shortcut.Save();
                    CS.ConsoleWriteLine($"Ярлык \"{Path.GetFileName(pathPdfDel)}\" добавлен", MesagType.Info);

                    Console.WriteLine("");
                    Console.WriteLine($"Ярлыки для {DataSetWpfOpt.Product} созданы\nДля выхода нажмите любую клавишу...");
                    Console.ReadKey();

                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

    }
}