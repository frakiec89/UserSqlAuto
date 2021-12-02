using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSqlAuto.BL
{
    public class MySqlServer : ISQL
    {
        public void CreateDateBase(string name, string cs)
        {
            MySqlConnection conn = new MySqlConnection(cs);
            conn.Open();
            string sql = $"CREATE SCHEMA `{name}`";
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteScalar();
            conn.Close();
        }

        public void CreateUser(string name, string pasword, string cs)
        {
            MySqlConnection conn = new MySqlConnection(cs);
            conn.Open();
            string sql =$"CREATE USER '{name}'@'%' IDENTIFIED BY '{pasword}'";
            string sql2 = $"GRANT ALL PRIVILEGES ON {name}.* TO  '{name}'@'%'";
            
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlCommand command2 = new MySqlCommand(sql2, conn);
            command.ExecuteScalar();
            command2.ExecuteScalar();
            conn.Close();
           
        }

       

        public string GetSqlconectionStrint(string adress, string login, string password)
        {
            return $"server={adress};uid={login};pwd={password}";
        }

        public string[] GetUser(string adress, string login, string password)
        {
            List<string> userDef = new List<string>() {
               "mysql.infoschema", 
                "mysql.session",   
                "mysql.sys"   , "root"
            }; // todo списки исключений

            List<string> userList = new List<string>();
            MySqlConnection con = new MySqlConnection(GetSqlconectionStrint(adress, login, password)); ;
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(@"SELECT User, Host FROM mysql.user", con))
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        userList.Add(dr[0].ToString());
                    }
                }
                con.Close();
            }

            foreach (var item in userDef)
            {
                userList.Remove(item);
            }
            return userList.ToArray();

        }

        public void RemoveDataBase(string name, string adress, string login, string password)
        {
            MySqlConnection con = new MySqlConnection(GetSqlconectionStrint(adress, login, password));
            con.Open();
            try
            {
                string sql = $"DROP DATABASE `{name}`";
                MySqlCommand command = new MySqlCommand(sql, con);
                command.ExecuteScalar();
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            finally
            {
                con.Close();
            }
        }

        public string[] GetDataBases(string adress, string login, string password)
        {
            List<string> dbDef = new List<string>() {
                "mysql" 
            ,"information_schema" 
            ,"performance_schema"
            }; // todo списки исключений
           
            List<string> dbList = new List<string>();
            MySqlConnection con = new MySqlConnection(GetSqlconectionStrint(adress, login, password)); ;
            con.Open();
           
            using (MySqlCommand cmd = new MySqlCommand(@"SHOW DATABASES", con))
            {
                using (var  dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        dbList.Add(dr[0].ToString());
                    }
                }
                con.Close();
            }

            foreach (var item in dbDef)
            {
                dbList.Remove(item);
            }
            return dbList.ToArray();
        }

        public void RemoveUser(string name, string adress, string login, string password)
        {
            MySqlConnection con = new MySqlConnection(GetSqlconectionStrint(adress, login, password));
            con.Open();
            try
            {
                string sql = $"DROP USER '{name}'@'%'";
                MySqlCommand command = new MySqlCommand(sql, con);
                command.ExecuteScalar();
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            finally
            {
                con.Close();
            }
        }
    }
}
