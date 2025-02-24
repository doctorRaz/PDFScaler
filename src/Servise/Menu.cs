/*
*        Copyright doctorRAZ 2014-2025 by ��������� ������
*
*        Licensed under the Apache License, Version 2.0 (the "License");
*        you may not use this file except in compliance with the License.
*        You may obtain a copy of the License at
*
*            http://www.apache.org/licenses/LICENSE-2.0
*
*        Unless required by applicable law or agreed to in writing, software
*        distributed under the License is distributed on an "AS IS" BASIS,
*        WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*        See the License for the specific language governing permissions and
*        limitations under the License.
*/

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

            #region License

            CS.ConsoleWriteLine("\t\tApache License\n" +
                "\tVersion 2.0, January 2004\n", MesagType.Ok);
            CS.ConsoleWriteLine($"   Copyright(C) {DataSetWpfOpt.Trademark} by {DataSetWpfOpt.Copyright}\n", MesagType.Ok);

            CS.ConsoleWrite("   ������������� � ������������ � Apache License, Version 2.0 (\"��������\");\n" +
                "   �� �� ������ ������������ ���� ����, ����� ��� � ������������ � ���������.\n" +
                "   �� ������ �������� ����� �������� �� ������: ", MesagType.None);
            CS.ConsoleWriteLine("http://www.apache.org/licenses/LICENSE-2.0", MesagType.Ok);

            CS.ConsoleWriteLine("\n   ���� ���������� ���������������� �� ������� ����� ��� ��� �� ����������� � ���������� �����,\n" +
                "   ����������� �����������, ���������������� �� ��������, ���������������� �� �������� ", MesagType.None);
            CS.ConsoleWrite("   \"��� ����\", ��� �������� ��� ������� ������ ����, ", MesagType.Warn);
            CS.ConsoleWriteLine("��� �����, ��� � ���������������.\n" +
                "   ��.�������� ��� ��������� ���������� � ���������� ������, ������������ ����������\n" +
                "   � ����������� � ������������ � ���������.", MesagType.None);
            CS.ConsoleWriteLine("   ========================================================================================", MesagType.None);
            #endregion
            #region About
            CS.ConsoleWriteLine("\t���������� ���������:", MesagType.Ok);
            CS.ConsoleWriteLine($"   {DataSetWpfOpt.Product} �������� ���������� ������ PDF, �������� � ������ �������� PDF �����,\n" +
                $"������� ����� � �������� ��������� ���������.");
            CS.ConsoleWriteLine("   � ���������� �������� PDF ����������� �������� �������� ����� ����� ���������� �������\n" +
                "� AutoCAD � nanoCAD.");
            CS.ConsoleWriteLine("   ����� ��������� ���� ������ ����� ������� ��������� ����� ������������ ������ *.bak\n" +
                "(��������� �� ���������)");
            CS.ConsoleWriteLine("!!!!�������������� ����� �� ������ ���� ������� ��� ��������� � �� ��������� � ������,\n" +
                "��� ������� ������", MesagType.Warn);
            CS.ConsoleWrite("������ ����������: ", MesagType.None);
            CS.ConsoleWriteLine("https://doctorraz.blogspot.com/2025/02/pdfscaler-autocad-nanocad.html", MesagType.Ok);
            CS.ConsoleWriteLine("   ========================================================================================", MesagType.None);
            #endregion

            #region Config XML
            CS.ConsoleWriteLine("\t���������:", MesagType.Ok);
            CS.ConsoleWriteLine($"��� ������ ������� {DataSetWpfOpt.Product} ��������� ���������������� ����:");
            CS.ConsoleWriteLine($"<{Config.XMLpatch}>", MesagType.Ok);
            CS.ConsoleWriteLine($"���� ������� {DataSetWpfOpt.Product} ����� �������� � ��������� ���������.\n");            
            #endregion

            #region Using
            CS.ConsoleWriteLine("\t!!!����� ��������� ������ ������ �������������� ���������������� ����!!!",MesagType.Warn);
            CS.ConsoleWriteLine($"����� ��������� ������: {DataSetWpfOpt.Product} [options] filenames", MesagType.Ok);
            Console.WriteLine("�����:");

            Console.WriteLine("\t��������� �������� �������� ������ (VP) ��������:");
            Console.WriteLine("\t\t-mm\t���������� (�������� �� ���������)");
            Console.WriteLine("\t\t-in\t�����");
            Console.WriteLine("\t\t-pt\t������-�����");
            Console.WriteLine("\t\t-cm\t����������");
            Console.WriteLine("\t\t-pr\t������� ����������� (1/96 �����)");
            Console.WriteLine("");

            Console.WriteLine("\t����������/��������/��������� VP ��������:");
            Console.WriteLine("\t\t-add\t��������� VP (�������� �� ���������)");
            Console.WriteLine("\t\t-del\t������� VP");
            Console.WriteLine("\t\t-mod\t��������� VP ��� �������� ������� �������������");
            Console.WriteLine("");

            Console.WriteLine("\t���������� PDF:");
            Console.WriteLine("\t\t-bakon\t��������� ����� ������������� PDF *.bak (�������� �� ���������)");
            Console.WriteLine("\t\t-bakoff\t������������ ���� ����������������");
            Console.WriteLine("");

            Console.WriteLine("\t������������� �������� ���� �������:");
            Console.WriteLine("\t\t-exon\t��������� ������������� (�������� �� ���������)");
            Console.WriteLine($"\t\t-exoff\t������������� �� ���������, ���� �� ���� ������)");

            CS.ConsoleWriteLine("   ========================================================================================", MesagType.None);
            #endregion

            string sendto = Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
            string pathPdfAdd = Path.Combine(sendto, $"{DataSetWpfOpt.Product} ADD.lnk");
            string pathPdfMod = Path.Combine(sendto, $"{DataSetWpfOpt.Product} ��DIFY.lnk");
            string pathPdfDel = Path.Combine(sendto, $"{DataSetWpfOpt.Product} DELETE.lnk");


            if (System.IO.File.Exists(pathPdfAdd))
            {
                CS.ConsoleWrite($"�� ������ ������� ������ {DataSetWpfOpt.Product} �� 'SEND TO'?\n" +
                    $"\t[Y]-��, ������ ������� ����������: ", MesagType.Warn);

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
                CS.ConsoleWrite($"�� ������] �������� ������ {DataSetWpfOpt.Product} � 'SEND TO'?\n" +
                    $"\t[Y]-��, ������ ������� ����������: ", MesagType.Ok);
                ConsoleKey response = Console.ReadKey().Key;
                Console.WriteLine("");

                if (response == ConsoleKey.Y)
                {
                    string targetPath = Path.Combine(Path.GetDirectoryName(DataSetWpfOpt.AsmFulPath),
                                                     $"{DataSetWpfOpt.AsmFileNameWithoutExtension}.exe");

                    //Add
                    WshShell shell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(pathPdfAdd);
                    shortcut.Description = $"����� {DataSetWpfOpt.Product} �������� VP";
                    shortcut.TargetPath = targetPath;
                    shortcut.Arguments = "-add";
                    shortcut.Save();
                    CS.ConsoleWriteLine($"����� \"{Path.GetFileName(pathPdfAdd)}\" ��������", MesagType.Info);

                    //Mod
                    //shell = new WshShell();
                    shortcut = (IWshShortcut)shell.CreateShortcut(pathPdfMod);
                    shortcut.Description = $"����� {DataSetWpfOpt.Product} �������� VP";
                    shortcut.TargetPath = targetPath;
                    shortcut.Arguments = "-mod";
                    shortcut.Save();
                    CS.ConsoleWriteLine($"����� \"{Path.GetFileName(pathPdfMod)}\" ��������", MesagType.Info);

                    //Delete
                    //shell = new WshShell();
                    shortcut = (IWshShortcut)shell.CreateShortcut(pathPdfDel);
                    shortcut.Description = $"����� {DataSetWpfOpt.Product} ������� VP";
                    shortcut.TargetPath = targetPath;
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