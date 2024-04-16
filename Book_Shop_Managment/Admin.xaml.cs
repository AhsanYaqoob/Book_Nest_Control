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
    /// Interaction logic for books.xaml
    /// </summary>
    public partial class books : Window
    {
        public books()
        {
            InitializeComponent();
            contentCntrol.Content = new BooksControl1();
        }

     
       

        private void Booksbtn_Click(object sender, RoutedEventArgs e)
        {
           
            Booksbtn.Background=new SolidColorBrush(Colors.DarkGreen);
            contentCntrol.Content = new BooksControl1();
           
        }

       

        private void Userbtn_Click(object sender, RoutedEventArgs e)
        {
            contentCntrol.Content = new UserControl1();
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            contentCntrol.Content = new DashboardControl2();
        }

        private void Logoutbtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
