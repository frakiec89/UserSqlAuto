using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using UserSqlAuto.BL;

namespace UserSqlAuto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ISQL _iSQL; 

        private List<User> users = new List<User>();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            rbMS.IsChecked = true;
            cbCount.ItemsSource = RunCB(30);
        }

        private ISQL GetSQL()
        {
            if (rbMS.IsChecked==true && rbMySql.IsChecked==false)
            {
                return new UserSqlAuto.BL.ServiceSQL(); // todo Зависимость
            }

            if (rbMS.IsChecked == false && rbMySql.IsChecked == true)
            {
                return new UserSqlAuto.BL.MySqlServer(); // todo Зависимость
            }
            throw new Exception("Укажите вид сервера");
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

        /// <summary>
        /// Го в  БД
        /// /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _iSQL = GetSQL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            string conectionStrint = _iSQL.GetSqlconectionStrint(tbAdress.Text, tbLogin.Text, tbPassword.Text);

            string name = user.Name;
            try
            {
                _iSQL.CreateDateBase(name, conectionStrint);
                _iSQL.CreateUser(name, user.Password, conectionStrint);
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

        private void rbMS_Checked(object sender, RoutedEventArgs e)
        {
            if (rbMS.IsChecked == true)
            {
                tbAdress.Text = "192.168.10.134";
                tbLogin.Text = "stud";
                tbPassword.Text = "stud";
            }
        }

        private void rbMySql_Checked(object sender, RoutedEventArgs e)
        {
            if(rbMySql.IsChecked==true)
            {
                tbAdress.Text = "192.168.10.134";
                tbLogin.Text = "root";
                tbPassword.Text = "Frakiec89";
            }
        }
    }
}
