namespace drz.Servise
{
    internal static class MessagWelcom
    {
        public static string Header = "ВНИМАНИЕ ВАЖНО!";

        public static string[] Welcom = new string[] {
            "\tПрограмма изменяет содержимое файлов PDF",
            "\t\tБудут созданы резервные копии существующих файлов с расширением *.BAK",
            "\t\tВы используете программу на свой страх и риск!"
            
        };

        public static string MesagStart = "\t\tПродолжить \"Y\", выход по любой клавише ";

        public static string MesagReplase = "Хотите повторить \"Y\", выход по любой клавише ";
    }
}
