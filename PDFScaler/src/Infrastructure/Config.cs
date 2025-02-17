using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Infrastructure;
using drz.PdfVpMod.Interfaces;

namespace drz.PDFScaler.Infrastructure
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
        static string XMLpatch => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{DataSetWpfOpt.AppProductName}.config");

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
                Logger logItem = new Logger($"Сохранены настройки по умолчанию {XMLpatch}", MesagType.Info);
                Logger.Add(logItem);
                return true;
            }
            catch (Exception e)
            {
                Logger logItem = new Logger($"Не удалось сохранить настройки {XMLpatch}\n{e.Message}", MesagType.Error);
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
                        Set = FormatterXML.Deserialize(fs) as Setting;
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Logger logItem = new Logger($"Не удалось прочитать настройки {XMLpatch}\n{e.Message}", MesagType.Info);
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
    }
}
