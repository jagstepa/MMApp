using MMApp.Domain.Repositories;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Helpers
{
    public static class ErrorMessages
    {
        public static string GetErrorMessage<T>(string value, ErrorMessageType errorMessageType) where T : IModelInterface
        {
            string errorMsg = string.Empty;
            var type = typeof(T).Name;
            string msgText = string.Empty;

            switch (errorMessageType)
            {
                case ErrorMessageType.Duplicate:
                    msgText = " ) already exists!";
                    break;
                case ErrorMessageType.Delete:
                    msgText = " ) can't be deleted!";
                    break;
                case ErrorMessageType.Changes:
                    msgText = " ) didn't change!";
                    break;
            }

            errorMsg = type + " ( " + value + msgText;
            return errorMsg;
        }
    }
}