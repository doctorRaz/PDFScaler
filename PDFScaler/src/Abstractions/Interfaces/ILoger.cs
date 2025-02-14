using System;

using drz.Enum;



namespace drz.Abstractions.Interfaces

{
    /// <summary>
    /// Loger
    /// </summary>
    internal interface ILoger
    {
        /// <summary>
        /// Gets the date time stamp.
        /// </summary>
        /// <value>
        /// The date time stamp.
        /// </value>
        DateTime DateTimeStamp { get; }

    /// <summary>
    /// Gets the name of the caller.
    /// </summary>
    /// <value>
    /// The name of the caller.
    /// </value>
    string CallerName { get; }

    /// <summary>
    /// Gets the type of the mesag.
    /// </summary>
    /// <value>
    /// The type of the mesag.
    /// </value>
    MesagType MesagType { get; }

    /// <summary>
    /// Gets the messag.
    /// </summary>
    /// <value>
    /// The messag.
    /// </value>
    string Messag { get; }

}
}