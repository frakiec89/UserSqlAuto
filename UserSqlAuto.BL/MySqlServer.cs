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
    }
}
