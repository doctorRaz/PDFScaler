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
    /// ����
    /// </summary>
    public static class UiMenu
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        public static bool Create()
        {
            IConsoleService CS = new ConsoleService();//������� �������

            #region Title
            CS.ConsoleWriteLine($"{DataSetWpfOpt.Product} {DataSetWpfOpt.VersionFull}\nCopyright(C) {DataSetWpfOpt.Trademark} by {DataSetWpfOpt.Copyright}\n", MesagType.Warning);
            #endregion

            #region License
            CS.ConsoleWriteLine("This program is free software: you can redistribute it and / or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.\nThis program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.\nYou should have received a copy of the GNU General Public License along with this program. If not, see < http://www.gnu.org/licenses/>.\n", MesagType.Info);
            #endregion

            #region Using
            CS.ConsoleWriteLine($"����� ��������� ������: {DataSetWpfOpt.Product} [options] filenames", MesagType.Ok);
            Console.WriteLine("�����:");

            Console.WriteLine("\t��������� �������� �������� ������ (VP) ��������:");
            Console.WriteLine("\t\t-mm\t���������� (�������� �� ���������)");
            Console.WriteLine("\t\t-in\t�����");
            Console.WriteLine("\t\t-pt\t������-�����");
            Console.WriteLine("\t\t-cm\t����������");
            Console.WriteLine("\t\t-pr\t������� �����������");
            Console.WriteLine("");

            Console.WriteLine("\t����������/��������/��������� VP ��������:");
            Console.WriteLine("\t\t-add\t��������� VP (�������� �� ���������)");
            Console.WriteLine("\t\t-del\t������� VP");
            Console.WriteLine("\t\t-mod\t��������� VP ��� �������� ������� �������������");
            Console.WriteLine("");

            Console.WriteLine("\t���������� PDF:");
            Console.WriteLine("\t\t-bakon\t��������� ����� ������������� PDF (*.bak) (�������� �� ���������)");
            Console.WriteLine("\t\t-bakoff\t������������ ���� ����������������");
            Console.WriteLine("");

            Console.WriteLine("\t����� �������� ���� �������:");
            Console.WriteLine("\t\t-exon\t��������� ������������� (�������� �� ���������)");
            Console.WriteLine("\t\t-exoff\t������������� ��������� ���� ���� ������");
            Console.WriteLine("");

            #endregion

            #region About
            CS.ConsoleWriteLine("��������� �������� ���������� ������ PDF, �������� � ������ �������� PDF �����, ������� ����� � �������� ��������� ���������.", MesagType.Ok);
            CS.ConsoleWriteLine("� ���������� �������� PDF ����������� �������� �������� ����� ����� ���������� ������� � AutoCAD � nanoCAD.", MesagType.Ok);
            CS.ConsoleWriteLine("����� ��������� ���� ������ ����� ������� ��������� ����� ������������ ������ *.bak (��������� �� ���������)\n", MesagType.Ok);
            CS.ConsoleWriteLine("�������������� ����� �� ������ ���� ������� ��� ��������� � �� ��������� � ������, ��� ������� ������\n", MesagType.Warning);
            #endregion

            Console.WriteLine("������ ����������: https://doctorraz.blogspot.com/2025/02/pdfscaler-autocad-nanocad.html\n");


            CS.ConsoleWriteLine("����������:", MesagType.Ok);

            CS.ConsoleWriteLine("� ���� ���� ������������ ����� �������� ��� �������� ������ �� ������ {DataSetWpfOpt.Product} � \"Send to\".\n", MesagType.Ok);

            string sendto = Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
            string pathPdfAdd = Path.Combine(sendto, $"{DataSetWpfOpt.Product} ADD.lnk");
            string pathPdfMod = Path.Combine(sendto, $"{DataSetWpfOpt.Product} ��DIFY.lnk");
            string pathPdfDel = Path.Combine(sendto, $"{DataSetWpfOpt.Product} DELETE.lnk");


            if (System.IO.File.Exists(pathPdfAdd))
            {
                CS.ConsoleWrite($"�� ������ ������� ������ {DataSetWpfOpt.Product} �� 'SEND TO'? [Y]-��, ������ ������� ����������: ", MesagType.Warning);

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
                            CS.ConsoleWriteLine($"����� {Path.GetFileName(path)} ������", MesagType.Info);
                        }
                        catch (Exception ex)
                        {
                            CS.ConsoleWriteLine(ex.Message, MesagType.Error);
                        }
                    }
                    CS.ConsoleWriteLine("��� ������ ������� ����� �������...", MesagType.Info);
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
                CS.ConsoleWrite($"�� ������ �������� ����� {DataSetWpfOpt.Product} � 'SEND TO'? [Y]-��, ������ ������� ����������: ", MesagType.Ok);
                ConsoleKey response = Console.ReadKey().Key;
                Console.WriteLine("");

                if (response == ConsoleKey.Y)
                {

                    //Add
                    WshShell shell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(pathPdfAdd);
                    shortcut.Description = $"����� {DataSetWpfOpt.Product} �������� VP";
                    shortcut.TargetPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    shortcut.Save();
                    CS.ConsoleWriteLine($"����� \"{Path.GetFileName(pathPdfAdd)}\" ��������", MesagType.Info);

                    //Mod
                    shell = new WshShell();
                    shortcut = (IWshShortcut)shell.CreateShortcut(pathPdfMod);
                    shortcut.Description = $"����� {DataSetWpfOpt.Product} �������� VP";
                    shortcut.TargetPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    shortcut.Arguments = "-mod";
                    shortcut.Save();
                    CS.ConsoleWriteLine($"����� \"{Path.GetFileName(pathPdfMod)}\" ��������", MesagType.Info);

                    //Delete
                    shell = new WshShell();
                    shortcut = (IWshShortcut)shell.CreateShortcut(pathPdfDel);
                    shortcut.Description = $"����� {DataSetWpfOpt.Product} ������� VP";
                    shortcut.TargetPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    shortcut.Arguments = "-del";
                    shortcut.Save();
                    CS.ConsoleWriteLine($"����� \"{Path.GetFileName(pathPdfDel)}\" ��������", MesagType.Info);

                    Console.WriteLine("");
                    Console.WriteLine($"������ ��� {DataSetWpfOpt.Product} �������\n��� ������ ������� ����� �������...");
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