using Microsoft.Win32;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserSqlAuto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<User> users = new List<User>();

        public MainWindow()
        {
            InitializeComponent();
            tbAdress.Text = "192.168.10.134";
            tbLogin.Text = "stud";
            tbPassword.Text = "stud1";

            cbCount.ItemsSource = RunCB(30);
        }
        private static List<int> RunCB(int  count)
        {
            List<int> vs = new List<int>();
            for (int i = 0; i < count; i++)
            {
                vs.Add(i + 1);
            }
            return vs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (users.Count >0)
            {
                foreach (var item in users)
                {
                    AddUserDB(item);
                }
                MessageBox.Show("Операция закончена");
            }
        }

        private void AddUserDB(User user)
        {
            string conectionStrint = $"Server={tbAdress.Text};Database=master;User Id={tbLogin.Text};Password={tbPassword.Text}";

            string name = user.Name;
            try
            {
                ServiceSQL.CreateDateBase(name, conectionStrint);
                ServiceSQL.CreateUser(name, user.Password, conectionStrint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btGEnerator_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbStartLogin.Text))
            {
                MessageBox.Show("Введите псевдоним для пользователей"); return;
            }

            if (cbCount.SelectedIndex<0)
            {
                MessageBox.Show("Укажите кол-во пользователей"); return;
            }
            users = USerGeneretic.GetUsers(tbStartLogin.Text , Convert.ToInt32( cbCount.SelectedItem.ToString()));
            dtUSer.ItemsSource = users;
        }

        private void btRun_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                users = USerGeneretic.GetUsers(openFileDialog.FileName);
                dtUSer.ItemsSource = users;
            }
        }
    }
}
