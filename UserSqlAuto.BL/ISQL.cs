namespace UserSqlAuto.BL
{
    public interface ISQL
    {
        void CreateDateBase(string name, string cs);
        void CreateUser(string name, string pasword, string cs);
        string GetSqlconectionStrint(string adress, string login, string password);
        string [] GetDataBases(string adress, string login, string password);
        void RemoveDataBase(string name, string adress, string login, string password);
        string[] GetUser(string adress, string login, string password);
        void RemoveUser(string name, string adress, string login, string password);
    }
}