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
using System.Windows.Shapes;

namespace UserSqlAuto
{
    /// <summary>
    /// Логика взаимодействия для MessageWindows.xaml
    /// </summary>
    public partial class MessageWindows : Window
    {
        public MessageWindows()
        {
            InitializeComponent();
            this.Loaded += MessageWindows_Loaded;
        }

        private void MessageWindows_Loaded(object sender, RoutedEventArgs e)
        {
            btOk.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
