using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
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

namespace Book_Shop_Managment
{
    /// <summary>
    /// Interaction logic for DashboardControl2.xaml
    /// </summary>
    public partial class DashboardControl2 : UserControl
    {
        SqlConnection conn = new SqlConnection("Data Source=AHSAN\\SQLEXPRESS;Initial Catalog=Book_Shop_Management_System;Integrated Security=True");


        public DashboardControl2()
        {
           
            InitializeComponent();
            show();
            SqlDataAdapter adp = new SqlDataAdapter("Select sum(BQty) from BookTbl", conn);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            bookstocklb.Content = dt.Rows[0][0].ToString();

            try
            {

                {
                    conn.Open();

                    SqlDataAdapter adp1 = new SqlDataAdapter("SELECT SUM(Amount) FROM BillTbl", conn);
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);

                    if (dt1.Rows.Count > 0 && dt1.Rows[0][0] != DBNull.Value)
                    {
                        Amountlbl.Content = dt1.Rows[0][0].ToString();
                    }
                    else
                    {
                        // Handle the case where there is no data or the value is DBNull
                        Amountlbl.Content = "No data available";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., show an error message
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            SqlDataAdapter adp2 = new SqlDataAdapter("Select Count(*) from User_Table", conn);
            DataTable dt2 = new DataTable();
            adp2.Fill(dt2);
            Userlbl.Content = dt2.Rows[0][0].ToString();

            conn.Close();
        }

        public void show()
        {
            try
            {
                // Check if datagridview is not null before setting ItemsSource
                if (datagridview != null)
                {
                    conn.Open();
                    string query = "Select * from BillTbl";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Set ItemsSource only if datagridview is not null
                    datagridview.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it or show an error message to the user
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Ensure the connection is closed even if an exception occurs
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

    }
}
