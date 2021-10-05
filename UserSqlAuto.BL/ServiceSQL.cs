using System;
using System.Data;
using System.Data.SqlClient;

namespace UserSqlAuto.BL
{
    public class ServiceSQL : ISQL
    {
        /// <summary>
        /// Передать  имя бд  и  Connection to a SQL Server instance
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cs"></param>
        public  void CreateDateBase(string name, string cs)
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
        public  void CreateUser(string name, string pasword, string cs)
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

        public string GetSqlconectionStrint(string adress, string login, string password)
        {
            return $"Server={adress};Database=master;User Id={login};Password={password}";
        }
    }
}
