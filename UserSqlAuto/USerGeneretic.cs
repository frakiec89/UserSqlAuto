using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSqlAuto
{
    public  class USerGeneretic
    {
        public static List<User> GetUsers (string name , int  count , int  countSimbolPassword)
        {
            List<User> users = new List<User>();

            for (int i = 0; i < count; i++)
            {
                users.Add(new User() { Name = name + (i + 1), Password =Random( countSimbolPassword) });
            }
            return users;
        }
        public static List<User> GetUsers(string filePath , int countSimbolPassword)
        {
            List<User> users = new List<User>();

            List<String> lineList = new List<string>();

            try
            {
                StreamReader sr = new StreamReader(filePath);
                string line = sr.ReadLine();
                while ( string.IsNullOrEmpty(line)==false)
                {
                     lineList.Add(line);
                     line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Exception: " + e.Message);
            }

            foreach (var item  in  lineList )
            {
                users.Add(new User() { Name = item , Password = Random(countSimbolPassword) });
            }
            return users;
        }
        public static string Random(int length)
            {
                try
                {
                    byte[] result = new byte[length];
                    for (int index = 0; index < length; index++)
                    {
                        result[index] = (byte)new Random().Next(100, 115);
                    }
                    return System.Text.Encoding.ASCII.GetString(result);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }
}
