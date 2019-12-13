﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FrbaOfertas
{
    // Parametriza construccion de IN clause  https://nodogmablog.bryanhogan.net/2016/01/parameterize-sql-where-in-clause-c/
    public static class SqlWhereInParamBuilder
    {
        public static string BuildWhereInClause<T>(string partialClause, string paramPrefix, IEnumerable<T> parameters)
        {
            string[] parameterNames = parameters.Select(
                (paramText, paramNumber) => "@" + paramPrefix + paramNumber.ToString())
                .ToArray();

            string inClause = string.Join(",", parameterNames);
            string whereInClause = string.Format(partialClause.Trim(), inClause);

            return whereInClause;
        }

        public static void AddParamsToCommand<T>(this SqlCommand cmd, string paramPrefix, IEnumerable<T> parameters)
        {
            string[] parameterValues = parameters.Select((paramText) => paramText.ToString()).ToArray();

            string[] parameterNames = parameterValues.Select(
                (paramText, paramNumber) => "@" + paramPrefix + paramNumber.ToString()
                ).ToArray();

            for (int i = 0; i < parameterNames.Length; i++)
            {
                cmd.Parameters.AddWithValue(parameterNames[i], parameterValues[i]);
            }
        }
    }
}