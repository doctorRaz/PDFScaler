using System;
using System.IO;

using drz.PDFScaler.Infrastructure;


namespace drz.PDFScaler.Servise
{
    /// <summary> Подгрузка библиотек </summary>
    partial class ReflectionLoader

    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionLoader"/> class.
        /// </summary>
        internal ReflectionLoader()
        {
            AsmEventAdd();//add  event Assembly resolve                   

        }

        #region System.Reflection            
        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string sPath;

            if (args.Name.IndexOf(",") > -1)
            {
                sPath = AssemblFulNameDll(args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");
            }
            else
            {
                sPath = AssemblFulNameDll(args.Name + ".dll");
            }

            if (sPath != string.Empty)
            {
                return System.Reflection.Assembly.LoadFile(sPath);
            }
            return null;
        }

        /// <summary>
        /// Получить полный путь к файлу загружаемой dll
        /// </summary>
        /// <param name="sDllName">имя dll</param>
        /// <returns>Путь и имя к библиотеке</returns>
        /*static*/
        string AssemblFulNameDll(string sDllName)
        {
            string asmPath = String.Empty;
            string sAsmFileFullName = DataSetWpfOpt.AsmFulPath;//каталог DLL
            // string sAsmFileFullName = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            string sPath = Directory.GetParent(sAsmFileFullName).FullName;


            string[] asmPaths = GetFilesOfDir(sPath, true, sDllName);
            if (asmPaths.Length > 0)
            {
                string asmPathTmp = asmPaths[0];//хватаем первую в списке

                if (File.Exists(asmPathTmp)) asmPath = asmPathTmp;//присваиваем ее значение
            }
            return asmPath;
        }

        #endregion

        #region Asm Event ADD

        /// <summary>
        /// add  event Assembly resolve    
        /// </summary>
        bool AsmEventAdd()
        {

            //https://adn-cis.org/forum/index.php?topic=10332.msg47741#msg47741
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                return true;
            }
            catch
            {
                return false;
            }
            ;
        }
        #endregion

        #region Utilities Files
        /// <summary>Получить список путей фалов в директории</summary>
        /// <param name="sPath">Директория с файлами</param>
        /// <param name="WithSubfolders">Учитывать поддиректории</param>
        /// <param name="sSerchPatern">Маска поиска</param>
        /// <returns>Пути к файлам</returns>
        static string[] GetFilesOfDir(string sPath, bool WithSubfolders, string sSerchPatern = "*.pdf")
        {
            try
            {
                return Directory.GetFiles(sPath,
                                            sSerchPatern,
                                            (WithSubfolders
                                            ? SearchOption.AllDirectories
                                            : SearchOption.TopDirectoryOnly));
            }
            catch
            {
                //Console.WriteLine(ex.Message);
                return new string[0];
            }
        }
        #endregion

    }
}
