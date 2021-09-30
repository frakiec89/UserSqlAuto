using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSqlAuto
{
    public class ServiceSQL
    {
        /// <summary>
        /// Передать  имя бд  и  Connection to a SQL Server instance
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cs"></param>
        internal static void CreateDateBase(string name, string cs)
        {
            SqlConnection myConn = new SqlConnection(cs);

            var str = $"CREATE DATABASE {name}";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }
        internal static void CreateUser(string name, string pasword, string cs)
        {
            SqlConnection myConn = new SqlConnection(cs);

            var str = $"CREATE LOGIN [{name}] WITH PASSWORD=N'{pasword}', " +
                $"DEFAULT_DATABASE=[{name}] , " +
                $"CHECK_EXPIRATION=OFF, " +
                $"CHECK_POLICY=OFF " +
                $"USE[{name}]  " +
                $"CREATE USER[{name}]  FOR LOGIN[{name}]  " +
                $"USE [{name}] " +
                $"ALTER ROLE[db_owner] ADD MEMBER [{name}]  ";

            SqlCommand myCommand = new SqlCommand(str, myConn);

            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery(); 
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }

            
        }
    }
}
