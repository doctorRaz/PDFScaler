using System;
using System.IO;


namespace drz.Servise
{
    /// <summary> Подгрузка библиотек </summary>
    partial class ReflectionLoader

    {
        public ReflectionLoader()
        {
            AsmEventAdd();//add  event Assembly resolve                   

        }

        #region System.Reflection            
        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string sPath = string.Empty;

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
            string sAsmFileFullName = DataSetWpfOpt.sAsmFulPath;//каталог DLL
            // string sAsmFileFullName = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            string sPath = Directory.GetParent(sAsmFileFullName).FullName;


            string[] asmPaths = UtilitesWorkFil.GetFilesOfDir(sPath, true, sDllName);
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
            };
        }
        #endregion

      
 
    }
}
