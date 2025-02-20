/*
*        Copyright doctorRAZ 2014-2025 by Разыграев Андрей
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
using System.Reflection;
using System.Xml.Serialization;

using drz.PdfVpMod.Enum;
using drz.PdfVpMod.Infrastructure;
using drz.PdfVpMod.Interfaces;

namespace drz.PDFScaler.Infrastructure
{
    class Config
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        internal Config(List<ILogger> logger)
        {
            Logger = logger;
            Set = new Setting();
            if (Deserialize())
            {
                Logger.Add(new Logger($"Настройки прочитаны {XMLpatch}", MesagType.Info));
            }
            else
            {
                Logger.Add(new Logger($"Не удалось прочитать и сохранить настройки {XMLpatch}", MesagType.Error));
            }

        }

        /// <summary> путь к XML </summary>
        internal static string XMLpatch => Path.Combine(Path.GetDirectoryName(DataSetWpfOpt.AsmFulPath), $"{DataSetWpfOpt.AppProductName}.config");

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
                    File.SetAttributes(XMLpatch, FileAttributes.Normal);
                }
                using (FileStream fs = new FileStream(XMLpatch, FileMode.Create))
                {
                    FormatterXML.Serialize(fs, Set);
                }
                Logger logItem = new Logger($"Записаны настройки по умолчанию {XMLpatch}", MesagType.Ok);
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
                    Logger logItem = new Logger($"Не удалось прочитать настройки {XMLpatch}\n{e.Message}", MesagType.Idle);
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
