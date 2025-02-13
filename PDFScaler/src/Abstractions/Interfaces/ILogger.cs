using System;

using drz.Abstractions.Interfaces;

namespace drz.Servise
{
    internal interface ILogger
    {
        DateTime DateTimeStamp { get; }
        MesagType MesagType { get; }
        string Messag { get; }
    }
}