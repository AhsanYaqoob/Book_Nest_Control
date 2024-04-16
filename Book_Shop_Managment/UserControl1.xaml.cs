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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Book_Shop_Managment
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        SqlConnection conn = new SqlConnection("Data Source=AHSAN\\SQLEXPRESS;Initial Catalog=Book_Shop_Management_System;Integrated Security=True");
        int key = 0;
        public UserControl1()
        {
            InitializeComponent();
            show();
        }

        private void show()
        {
            conn.Open();
            string query = "Select * from User_Table";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            datagridview.ItemsSource = dataTable.DefaultView;
            conn.Close();

        }
        private void reset()
        {
            UserName.Text = string.Empty;
            PassTb.Text = string.Empty;
           
           PhoneTb.Text= string.Empty;
          AddTb.Text = string.Empty;
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(UserName.Text) || String.IsNullOrEmpty(PhoneTb.Text) || String.IsNullOrEmpty(AddTb.Text) || String.IsNullOrEmpty(PassTb.Text))
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();

                    string query = "Insert into User_Table Values('" + UserName.Text + "' , '" + PhoneTb.Text + "','" + AddTb.Text + "','" + PassTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Saved SuccessFully");

                    conn.Close();
                       show();
                   reset();
                      //  Filter();*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();

                    string query = "Delete from User_Table where UId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted SuccessFully");

                    conn.Close();
                    show();
                   
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



            }
        }

        private void datagridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagridview.SelectedItem != null)
            {
                DataRowView row = (DataRowView)datagridview.SelectedItem;

                UserName.Text = row["UName"].ToString();
                PhoneTb.Text = row["Uphone"].ToString();
                AddTb.Text= row["UAdd"].ToString(); // Assuming ComboBox
                PassTb.Text = row["UPass"].ToString();
                

                if (String.IsNullOrEmpty(UserName.Text))
                {
                    key = 0;
                }
                else
                {
                    key = Int32.Parse(row["UId"].ToString());
                }
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(UserName.Text) || String.IsNullOrEmpty(PhoneTb.Text) || String.IsNullOrEmpty(AddTb.Text) || String.IsNullOrEmpty(PassTb.Text))
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();

                    string query = "update User_Table set UName='" + UserName.Text + "', UPhone ='" + PhoneTb.Text + "',UAdd='" + AddTb.Text + "',UPass='" + PassTb.Text + "' where UId='" + key + "';";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Edit SuccessFully");

                    conn.Close();
                    show();
                  
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



            }
        }
    }
}

