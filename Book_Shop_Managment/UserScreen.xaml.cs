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

namespace Book_Shop_Managment
{
    /// <summary>
    /// Interaction logic for UserScreen.xaml
    /// </summary>
    public partial class UserScreen : Window
    {
        public UserScreen()
        {
            InitializeComponent();
            contentControl.Content = new UserBilling();
        }

        private void Logoutbtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
