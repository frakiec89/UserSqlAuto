namespace UserSqlAuto.BL
{
    public interface ISQL
    {
        void CreateDateBase(string name, string cs);
        void CreateUser(string name, string pasword, string cs);
        string GetSqlconectionStrint(string adress, string login, string password);
    }
}