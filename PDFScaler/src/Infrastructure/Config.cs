using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

using drz.PDFScaler.Servise;
using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Interfaces;
using drz.PdfVpMod.Servise;

using LogManager;

namespace drz.PDFScaler
{
    class Config
    {

        internal Config(List<ILogger> logger)
        {
            Logger = logger;
            Set = new Setting();
            if (Deserialize())
            {
                Logger logItem = new Logger($"Настройки прочитаны {XMLpatch}", MesagType.Info);
                Logger.Add(logItem);
            }
            else
            {
                Logger logItem = new Logger($"Не удалось прочитать и сохранить настройки {XMLpatch}", MesagType.Error);
                Logger.Add(logItem);
            }

        }

        /// <summary> путь к XML </summary>
        static string XMLpatch => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{DataSetWpfOpt.sAppProductName}.config");

        static XmlSerializer FormatterXML => new XmlSerializer(typeof(Setting));

        public Setting Set;

        List<ILogger> Logger;

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns></returns>
        internal bool Serialize()
        {
            try
            {
                if (File.Exists(XMLpatch))
                {
                    File.SetAttributes(XMLpatch, FileAttributes.Normal);//прибить во избежание
                }
                using (FileStream fs = new FileStream(XMLpatch, FileMode.Create))
                {
                    FormatterXML.Serialize(fs, Set);
                }
                Logger logItem = new Logger($"Настройки сохранены {XMLpatch}", MesagType.Info);
                Logger.Add(logItem);
                return true;
            }
            catch (Exception e)
            {
                Logger logItem = new Logger($"Невозможно сохранить настройки {XMLpatch}\n{e.Message}", MesagType.Error);
                Logger.Add(logItem);
                return false;
            }
        }

        /// <summary>
        /// Deserializes this instance.
        /// </summary>
        /// <returns></returns>
        internal bool Deserialize()
        {
            if (File.Exists(XMLpatch))
            {
                try
                {
                    using (FileStream fs = new FileStream(XMLpatch, FileMode.Open, FileAccess.Read))
                    {
                        //_lFieldsFrmts = new List<FieldsFrmt>();
                       Setting Set0 = FormatterXML.Deserialize(fs) as Setting;
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Logger logItem = new Logger($"Не удалось прочитать {XMLpatch}\n{e.Message}", MesagType.Info);
                    Logger.Add(logItem);
                    return Serialize();
                }
            }
            else
            {
                //сохраняем настройки по дефолту
                return Serialize();
            }
        }








        //static XDocument config = null;

        static int _ExitConfirmation = 0;
        public static int ExitConfirmation
        {
            get
            {
                if (LogHelper.ErrorLog.Count() > 0) return 1;
                return _ExitConfirmation;
            }

            set
            {
                if (value >= 1) _ExitConfirmation = 1;
                else _ExitConfirmation = 0;
            }
        }

        //public static void ReadXML()
        //{
        //    try
        //    {
        //        config = XDocument.Load(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\config.xml");
        //        System.Console.WriteLine("The configuration file has been read correctly");
        //    }
        //    catch (System.IO.FileNotFoundException)
        //    {
        /*
        config = new XDocument(
            new XComment("PDFUnisci config"),
            new XElement("config",
                new XComment("It indicates the number of digits when you divide a PDF"),
                new XElement($"{nameof(PDFInterface.DefaultDigit)}", PDFInterface.DefaultDigit),
                new XComment("The program will remain open until you press any key"),
                new XElement($"{nameof(ExitConfirmation)}", ExitConfirmation),
                new XComment("If set to zero disables the function to replace the cover"),
                new XElement($"{nameof(PDFInterface.CoverFunction)}", PDFInterface.CoverFunction),
                new XComment("If set to zero disables the function to add Bookmarks when merge PDF"),
                new XElement($"{nameof(PDFInterface.Bookmarks)}", PDFInterface.Bookmarks),
                new XComment("If set to one flat only first page of the PDF"),
                new XElement($"{nameof(PDFInterface.FlatOnlyFirstPage)}", PDFInterface.FlatOnlyFirstPage)));
        config.Declaration = new XDeclaration("1.0", "utf-8", "true");
        config.Save(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\config.xml");

        System.Console.WriteLine("The config.xml file is not found, it will create");
        */
        //        }

        //        /*
        //        //Leggo il file xml
        //        PDFInterface.DefaultDigit = ReadConfig(PDFInterface.DefaultDigit, nameof(PDFInterface.DefaultDigit));
        //        ExitConfirmation = ReadConfig(ExitConfirmation, nameof(ExitConfirmation));
        //        PDFInterface.CoverFunction = ReadConfig(PDFInterface.CoverFunction, nameof(PDFInterface.CoverFunction));
        //        PDFInterface.Bookmarks = ReadConfig(PDFInterface.Bookmarks, nameof(PDFInterface.Bookmarks));
        //        PDFInterface.FlatOnlyFirstPage = ReadConfig(PDFInterface.FlatOnlyFirstPage, nameof(PDFInterface.FlatOnlyFirstPage));
        //        */
        //    }

        //    static int ReadConfig(int option, string nameof)
        //    {
        //        int i;
        //        if (int.TryParse(config.Elements("config").Select(o => o.Element($"{nameof}").Value).First(), out i)) return i;
        //        else return option;
        //    }
    }
}
