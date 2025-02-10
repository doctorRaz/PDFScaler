namespace drz.Servise
{
    internal static class MessagWelcom
    {
        public static string Header = "ВНИМАНИЕ ВАЖНО!";

        public static string[] Welcom = new string[] {
            "\tПрограмма изменяет содержимое файлов PDF, добавляя к каждой странице scale factor.",
            "\t\tТаким образом внешние ссылки PDF будут масштабироваться одинаково в AuroCAD и nanoCAD",
            "\t\tПосле обработки будут созданы резервные копии существующих файлов *.BAK",
            "\t\tВы используете программу на свой страх и риск!",
             "\t\tОбрабатываемые файлы не должны бить открыткрыты в просмотрщике и не загружены в чертеж, как внешние ссылки",
            "",

        };

        public static string MesagStart = "\tПродолжить \"Y\", выход по любой клавише ";

        public static string MesagReplase = "\tХотите повторить \"Y\", выход по любой клавише ";
    }
}
