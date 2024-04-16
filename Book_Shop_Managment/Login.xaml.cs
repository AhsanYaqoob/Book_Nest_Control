using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SqlConnection conn = new SqlConnection("Data Source=AHSAN\\SQLEXPRESS;Initial Catalog=Book_Shop_Management_System;Integrated Security=True;TrustServerCertificate=true");
        public static string username="";
        public Login()
        {
            InitializeComponent();
        }
       
        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM User_Table WHERE UName=@UName AND UPass=@UPass";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UName", UnameTb.Text);
                cmd.Parameters.AddWithValue("@UPass", UPassTb.Password);
                username = UnameTb.Text;
                int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                


                if (userCount > 0)
                {
                    // User credentials are valid
                   
                    conn.Close();
                    UserScreen books = new UserScreen();
                   // MessageBox.Show(username);

                    books.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                conn.Close();
            }

            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
            this.Close();
        }

        private void UnameTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
