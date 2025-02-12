using System.Runtime.CompilerServices;


namespace drz.Abstractions.Interfaces
{
    /// <summary>
    /// Интерфейс сообщений консоли
    /// </summary>
    partial interface IConsoleService
    {
        /// <summary>
        /// Вывод сообщения в консоль, принудительная раскраска 
        /// </summary>
        /// <param name="Message">Выводимое сообщение</param>
        /// <param name="FColor">Color of the foreground</param>
        /// <param name="BColor">Color of the background</param>
        /// <param name="Title">The title.</param>
        /// <param name="CallerName">Вызывающий метод. При использовании обязательно использование <code>[CallerMemberName]</code></param>
        void ConsoleWriteLineFB(string Message,
                                 WConsoleColor FColor = WConsoleColor.Default,
                                 WConsoleColor BColor = WConsoleColor.Default,
                                 string Title = null,
                                 [CallerMemberName] string CallerName = null);


        /// <summary>
        /// Consoles the write line.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="TypeMesag">The type mesag.</param>
        /// <param name="Title">The title.</param>
        /// <param name="CallerName">Name of the caller.</param>
        void ConsoleWriteLine(string Message,
                            MesagType TypeMesag = MesagType.None,
                             string Title = null,
                             [CallerMemberName] string CallerName = null);

        /// <summary>
        /// Consoles the write.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="TypeMesag">The type mesag.</param>
        /// <param name="Title">The title.</param>
        /// <param name="CallerName">Name of the caller.</param>
        void ConsoleWrite(string Message,
                    MesagType TypeMesag = MesagType.None,
                     string Title = null,
                     [CallerMemberName] string CallerName = null);
    }


    /// <summary>
    /// тип сообщения Info Warning...
    /// </summary>
    enum MesagType
    {
        /// <summary>
        /// The none
        /// </summary>
        None,

        /// <summary>
        /// The information
        /// </summary>
        Info,

        /// <summary>
        /// The warning
        /// </summary>
        Warning,

        /// <summary>
        /// The error
        /// </summary>
        Error,

        /// <summary>
        /// The ok
        /// </summary>
        Ok,

    }

    /// <summary>
    ///Цвет раскраски текста или фона
    /// </summary>
    enum WConsoleColor
    {
        //
        // Сводка:
        //     Черный цвет.
        Black,
        //
        // Сводка:
        //     Темно-синий цвет.
        DarkBlue,
        //
        // Сводка:
        //     Темно-зеленый цвет.
        DarkGreen,
        //
        // Сводка:
        //     Темно-голубой цвет (темный сине-зеленый).
        DarkCyan,
        //
        // Сводка:
        //     Темно-красный цвет.
        DarkRed,
        //
        // Сводка:
        //     Темно-пурпурный цвет (темный фиолетово-красный).
        DarkMagenta,
        //
        // Сводка:
        //     Темно-желтый цвет (коричнево-желтый).
        DarkYellow,
        //
        // Сводка:
        //     Серый цвет.
        Gray,
        //
        // Сводка:
        //     Темно-серый цвет.
        DarkGray,
        //
        // Сводка:
        //     Синий цвет.
        Blue,
        //
        // Сводка:
        //     Зеленый цвет.
        Green,
        //
        // Сводка:
        //     Голубой цвет (сине-зеленый).
        Cyan,
        //
        // Сводка:
        //     Красный цвет.
        Red,
        //
        // Сводка:
        //     Пурпурный цвет (фиолетово-красный).
        Magenta,
        //
        // Сводка:
        //     Желтый цвет.
        Yellow,
        //
        // Сводка:
        //     Белый цвет.
        White,
        //текущий цвет
        Default,
    }

    /// <summary>
    /// Признак фон или текст
    /// </summary>
    enum FB
    {
        Foreground,
        Bacground
    }
}
