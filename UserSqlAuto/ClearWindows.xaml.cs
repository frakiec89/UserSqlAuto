using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UserSqlAuto.BL;

namespace UserSqlAuto
{
    /// <summary>
    /// Логика взаимодействия для ClearWindows.xaml
    /// </summary>
    public partial class ClearWindows : Window
    {
        private string _adress;
        private string _login;
        private string _password;
        private ISQL _sql;

        public ClearWindows(string adress , string login , string password, ISQL iSQL)
        {
            InitializeComponent();
            _adress = adress;
            _login = login;
            _password = password;
            this.Loaded += ClearWindows_Loaded;
            _sql = iSQL;
        }

        private void ClearWindows_Loaded(object sender, RoutedEventArgs e)
        {
            dtContent.ItemsSource = GetContentDB();
            dtContentUser.ItemsSource = GetContentUser();
        }

        private List<UserModel> GetContentUser()
        {
            try
            {
                ISQL sQL = _sql; 
                string[] databaseUser = sQL.GetUser(_adress, _login, _password);
                List<UserModel> userModel = new List<UserModel>();
                foreach (var item in databaseUser)
                {
                    userModel.Add(new UserModel { Name = item, IsClear = "true" });
                }
                return userModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return new List<UserModel>();
        }

        private List<DataBaseModel> GetContentDB()
        {
            try
            {
                ISQL sQL = _sql; 
                string[] database = sQL.GetDataBases(_adress, _login, _password);
                List<DataBaseModel> dataBaseModel = new List<DataBaseModel>();
                foreach (var item in database)
                {
                    dataBaseModel.Add(new DataBaseModel { Name = item, IsClear = "true" });
                }
                return dataBaseModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return new List<DataBaseModel>();
        }

        private void removeAllDB_Click(object sender, RoutedEventArgs e)
        {
            List<DataBaseModel> dataBaseModels = dtContent.ItemsSource as List<DataBaseModel>;
            dataBaseModels = dataBaseModels.Where(x => x.IsClear.ToLower() == "true").ToList();
            ISQL sQL = _sql; 
            try
            {
                foreach (var item in dataBaseModels)
                {
                    sQL.RemoveDataBase(item.Name, _adress, _login, _password);
                }
                MessageBox.Show("Базы удалены");
                dtContent.ItemsSource = GetContentDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void removeAllUSer_Click(object sender, RoutedEventArgs e)
        {
            List<UserModel> userModels = dtContentUser.ItemsSource as List<UserModel>;
            userModels = userModels.Where(x => x.IsClear.ToLower() == "true").ToList();
            ISQL sQL = _sql;
            try
            {
                foreach (var item in userModels)
                {
                    try
                    {
                        sQL.RemoveUser(item.Name, _adress, _login, _password);
                    }
                    catch(Exception ex)
                    {
                       var r = MessageBox.Show($"Пользователя {item.Name} Удалить не  удалось \n" +
                            ex.Message, "Ошибка" +"\n продолжить  удаление  дальше?", MessageBoxButton.YesNo
                            );

                        if (r == MessageBoxResult.Yes)
                            continue;
                        else
                            dtContentUser.ItemsSource = GetContentUser();
                        return;
                    }
                }
                MessageBox.Show("Пользователи  удалены");
                dtContentUser.ItemsSource = GetContentUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    internal class UserModel
    {
        /// <summary>
        /// Название базы данных
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Удалить да  или нет 
        /// </summary>
        public string IsClear { get; set; }
    }

    public class DataBaseModel
    {
        /// <summary>
        /// Название базы данных
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Удалить да  или нет 
        /// </summary>
        public string IsClear { get; set; }
    }
}
