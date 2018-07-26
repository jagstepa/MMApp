using MMApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMApp.Web.Helpers
{
    public static class ErrorMessages
    {
        public static string GetDuplicateErrorMessage<T>(string value) where T : IModelInterface
        {
            string errorMsg = string.Empty;
            var type = typeof(T).Name;
            errorMsg = type + " ( " + value + " ) already exists!";
            return errorMsg;
        }

        public static string GetDeleteErrorMessage<T>(string value) where T : IModelInterface
        {
            string errorMsg = string.Empty;
            var type = typeof(T).Name;
            errorMsg = type + " ( " + value + " ) can't be deleted!";
            return errorMsg;
        }
    }
}