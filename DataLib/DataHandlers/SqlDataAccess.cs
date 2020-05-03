using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper; //import reference
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLib
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName= "EtainDB")
        {
            //go and grab the connection string from web.config based on the name above
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString; 
        }

        public static List<T> LoadSalt<T>(string sql, T data)
        {
            // returns a generic list of one item if the salt exists for an email address
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql, data).ToList();
            }
        }
        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                //saves a user to the DB
                return cnn.Execute(sql, data); //execute query, pass data and return
            }
        }

        public static int CheckUser<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                //used to check a users credentials in the db
                return cnn.Query<T>(sql, data).Count();
            }
        }
    }
}
