using System;
using System.Collections.Generic;
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
            return $"Server={adress};Database=master;User Id={login};Password={password};Connection Timeout=2";
        }

        public string[] GetDataBases(string adress, string login, string password)
        { 
            // стандартные базы данных
            List<string> defDb = new List<string> { "master" , "tempdb" , "model" , "msdb" , "Gogs"
            }; // todo списки исключений

            List<string> list = new List<string>();
            // Open connection to the database
            string cs = GetSqlconectionStrint(adress, login, password);
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                    {
                       
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(dr[0].ToString());
                            }
                        }
                    }
                }
                foreach (var item in defDb)
                {
                    list.Remove(item);
                }
                return list.ToArray();

            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void RemoveDataBase(string name, string adress, string login, string password)
        {
            SqlConnection myConn = new SqlConnection( GetSqlconectionStrint(adress , login , password) );

            var str = $@"ALTER DATABASE  [{name}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        DROP DATABASE [{name}]";

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

        public string[] GetUser(string adress, string login, string password)
        {
            // стандартные пользователи базы данных
            List<string> deUser = new List<string> { "Sa" ,
            "##MS_PolicyEventProcessingLogin##",
            "##MS_PolicyTsqlExecutionLogin##",
            "sa",
            "stud"
            }; // todo списки исключений
            List<string> list = new List<string>();

            // Open connection to the database
            string cs = GetSqlconectionStrint(adress, login, password);
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(@"select * from sys.sql_logins", con))
                    {

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(dr[0].ToString());
                            }
                        }
                    }
                }
                foreach (var item in deUser)
                {
                    list.Remove(item);
                }
                return list.ToArray();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveUser(string name, string adress, string login, string password)
        {
            SqlConnection myConn = new SqlConnection(GetSqlconectionStrint(adress, login, password));
            var str = $@"DROP LOGIN [{name}]";
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
