using Dapper;
using MMApp.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static int GetEntityId(IDbConnection _db, string spName, string entityType, string parameters)
        {
            return _db.Query<int>(spName, new { Type = entityType, Parameters = parameters }, commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        public static List<IModelInterface> GetAll<T>(IDbConnection _db, string spName, string type) where T : IModelInterface
        {
            List<IModelInterface> entityList = new List<IModelInterface>();
            var tttype = typeof(T).Name;
            var entList = _db.Query<T>(spName, new { Type = type }, commandType: CommandType.StoredProcedure).ToList();
            var eList = entList.ConvertAll(x => (IModelInterface)x);
            entityList.AddRange(eList);
            return entityList;
        }
        public static void AddEntity<T>(IDbConnection _db, string spName, string type, T value) where T : IModelInterface
        {
            T obj = (T)value;
        }
    }
}
