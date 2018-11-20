using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Helper
{
    public class JSONFilterConvertor
    {
        public static string ReturnSQLFilterString(string JsonFilter)
        {
            return $"SELECT * from dbo.User where Email like 'a%'";
        }
    }
}
