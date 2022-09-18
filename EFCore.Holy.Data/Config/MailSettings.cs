namespace EFCore.Holy.Data.Config
{
    public static class MailSettings
    {
        public static string DisplayName = "Holy Development";
        public static string From = "holy.development.no@gmail.com";
        public static string Username = "holy.development.no@gmail.com";
        public static string Password = "";
        public static string Host = "smtp.gmail.com";
        public static string SmtpServer = "smtp.gmail.com";
        public static int Port = 587;
        public static bool UseSSL = false;
        public static bool UseStartTls = true;
    }
}