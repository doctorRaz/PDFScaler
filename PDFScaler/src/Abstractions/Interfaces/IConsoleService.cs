using System;
using System.Runtime.CompilerServices;


namespace drz.Abstractions.Interfaces
{
    public interface IConsoleService
    {
        /// <summary> Вывод сообщения в консоль </summary>
        /// <param name="Message">Выводимое сообщение</param>
        /// <param name="CallerName">Вызывающий метод. При использовании обязательно использование <code>[CallerMemberName]</code></param>
        void ConsoleColorMessage(string Message,
                                 WConsoleColor FColor=WConsoleColor.Default,
                                 WConsoleColor BColor=WConsoleColor.Default,
                                 string Title = null,
                                 [CallerMemberName] string CallerName = null);

        /// <summary> Вывод сообщения в консоль </summary>
        /// <param name="Message">Выводимое сообщение</param>
        /// <param name="CallerName">Вызывающий метод. При использовании обязательно использование <code>[CallerMemberName]</code></param>
    }

    public enum WConsoleColor
    {

        Default,
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
        White
    }
}
