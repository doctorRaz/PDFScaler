using System;

using drz.Abstractions.Interfaces;

namespace drz.Servise
{
    /// <summary>
    /// Сервис сохранения сообщений
    /// </summary>
    [Serializable]
    internal class Logger : ILogger
    {
        DateTime _dateTimeStamp;
        public DateTime DateTimeStamp => _dateTimeStamp;

        string _messag;
        public string Messag => _messag;

        MesagType _mesagType;
        public MesagType MesagType => _mesagType;

        public Logger(string messag, MesagType mesagType = MesagType.None)
        {
            _dateTimeStamp = DateTime.Now;

            _messag = messag;

            _mesagType = mesagType;
        }
    }
}
