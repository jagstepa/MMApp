using Dapper;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MMApp.Data
{
    public static class DBHelpers
    {
        public static IDbConnection GetDbConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public static string GetParameters(Dictionary<string, string> pars)
        {
            StringBuilder strBuilder = new StringBuilder();
            var rowDelimiter = '@';
            var detailDelimiter = '#';

            foreach (KeyValuePair<string, string> pair in pars)
            {
                if (strBuilder.Length == 0)
                    strBuilder.Append(pair.Key + detailDelimiter + pair.Value);
                else
                    strBuilder.Append(rowDelimiter + pair.Key + detailDelimiter + pair.Value);
            }

            return strBuilder.ToString();
        }
        public static void AddEntity<T>(IDbConnection _db, string spName, string type, T value) where T : IModelInterface
        {
            T obj = (T)value;
        }

        public static DataTable GetTableParameters<T>(string searchText) where T : IModelInterface
        {
            string type = typeof(T).Name;

            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("ParamType", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("ParamValue", typeof(string));
            dt.Columns.Add(dc);

            DataRow dr = dt.NewRow();
            dr[0] = "Type";
            dr[1] = type;
            dt.Rows.Add(dr);

            DataRow dr2 = dt.NewRow();
            dr2[0] = "SearchText";
            dr2[1] = searchText;
            dt.Rows.Add(dr2);

            return dt;
        }

        public static DataTable GetTableParameters<T>(IModelInterface entity) where T : IModelInterface
        {
            string type = typeof(T).Name;
            var parameters = GetEntityProperties<T>(entity);

            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("ParamType", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("ParamValue", typeof(string));
            dt.Columns.Add(dc);

            DataRow dr = dt.NewRow();
            dr[0] = "Type";
            dr[1] = type;
            dt.Rows.Add(dr);

            foreach (KeyValuePair<string, string> pair in parameters)
            {
                dr = dt.NewRow();
                dr[0] = pair.Key;
                dr[1] = pair.Value;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private static Dictionary<string, string> GetEntityProperties<T>(IModelInterface value) where T : IModelInterface
        {
            Dictionary<string, string> paramDict = new Dictionary<string, string>();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var dbFields = property.GetCustomAttributes(typeof(DBFieldAttribute), false);

                if (dbFields.Length > 0)
                {
                    var pName = property.Name;
                    var pValue = property.GetValue(value, null);

                    if (pValue == null)
                    {
                        paramDict.Add(pName, string.Empty);
                    }
                    else
                    {
                        paramDict.Add(pName, pValue.ToString());
                    }
                }
            }

            return paramDict;
        }
    }
}
