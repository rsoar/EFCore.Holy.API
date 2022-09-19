namespace EFCore.Holy.Business.Handling
{
    public static class Error
    {
        public const string InvalidMail = "INVALID_EMAIL";
        public const string FailedSendEmail = "FAILED_SEND_EMAIL";
        public const string ManagerAlreadyExists = "MANAGER_ALREADY_EXISTS";
        public const string InvalidRole = "INVALID_MANAGER_ROLE";
        public const string ChurchNotFound = "CHURCH_NOT_FOUND";
        public const string InvalidCredentials = "INVALID_CREDENTIALS";
        public const string ManagerNotFound = "MANAGER_NOT_FOUND";
        public const string DoesHavePermission = "USER_DOES_NOT_HAVE_PERMISSION";
    }
}
