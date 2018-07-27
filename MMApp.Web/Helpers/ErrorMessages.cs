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
        //public static string GetDuplicateErrorMessage<T>(string value) where T : IModelInterface
        //{
        //    string errorMsg = string.Empty;
        //    var type = typeof(T).Name;
        //    errorMsg = type + " ( " + value + " ) already exists!";
        //    return errorMsg;
        //}

        //public static string GetDeleteErrorMessage<T>(string value) where T : IModelInterface
        //{
        //    string errorMsg = string.Empty;
        //    var type = typeof(T).Name;
        //    errorMsg = type + " ( " + value + " ) can't be deleted!";
        //    return errorMsg;
        //}

        //public static string GetChangesErrorMessage<T>(string value) where T : IModelInterface
        //{
        //    string errorMsg = string.Empty;
        //    var type = typeof(T).Name;
        //    errorMsg = type + " ( " + value + " ) didn't change!";
        //    return errorMsg;
        //}
    }
}