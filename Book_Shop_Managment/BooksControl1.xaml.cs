using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for BooksControl1.xaml
    /// </summary>
    public partial class BooksControl1 : UserControl
    {
        
        SqlConnection conn=new SqlConnection("Data Source=AHSAN\\SQLEXPRESS;Initial Catalog=Book_Shop_Management_System;Integrated Security=True");
      /*  private ObservableCollection<YourDataItems> yourDataItems;
        private ICollectionView collectionView;*/


        int key = 0;
        public BooksControl1()
        {
            InitializeComponent();
            show();
         /*   yourDataItems = new ObservableCollection<YourDataItems>();
            collectionView = CollectionViewSource.GetDefaultView(yourDataItems);
           datagridview.ItemsSource = collectionView;*/

            BCatCb.Items.Add("Programming");
            BCatCb.Items.Add("Networking");


            CatCbSearchCb.Items.Add("Programming");
            CatCbSearchCb.Items.Add("Networking");
            CatCbSearchCb.Items.Add("Art Books");
            CatCbSearchCb.Items.Add("Poetry Books");
            CatCbSearchCb.Items.Add("Biography");
            CatCbSearchCb.Items.Add("Self-Help Books");



        }

        private void show()
        {
            conn.Open();
            string query = "Select * from BookTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query,conn);
            DataTable dataTable = new DataTable();
           adapter.Fill(dataTable);
            datagridview.ItemsSource = dataTable.DefaultView;
            conn.Close();

        }

        private void Filter()
        {
            conn.Open();
            if (CatCbSearchCb.SelectedItem != null)
            {
                string query = "Select * from BookTbl where BCat='" + CatCbSearchCb.SelectedItem.ToString() + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                datagridview.ItemsSource = dataTable.DefaultView;
            }
            conn.Close();


        }

        private void reset()
        {
            BTitleTb.Text= string.Empty;
            BautTb.Text= string.Empty;
            BCatCb.SelectedIndex = -1;
            CatCbSearchCb.SelectedIndex = -1;
            QtyTb.Text= string.Empty;
            PriceTb.Text= string.Empty;
        }
        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(BTitleTb.Text)|| String.IsNullOrEmpty(BautTb.Text)|| String.IsNullOrEmpty(QtyTb.Text)|| String.IsNullOrEmpty(PriceTb.Text)|| String.IsNullOrEmpty(BCatCb.Text)) {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();

                    string query="Insert into BookTbl Values('"+BTitleTb.Text+"' , '"+BautTb.Text+"','"+BCatCb.Text+"','"+QtyTb.Text+"','"+PriceTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Saved SuccessFully");

                    conn.Close();
                        show();
                    Filter();
                    reset();
                }
                catch(Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                

                
            }
        }

        private void datagridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagridview.SelectedItem != null)
            {
                DataRowView row = (DataRowView)datagridview.SelectedItem;

                BTitleTb.Text = row["BTitle"].ToString();
                BautTb.Text = row["BAuthor"].ToString();
                BCatCb.SelectedItem = row["BCat"].ToString(); // Assuming ComboBox
                QtyTb.Text = row["BQty"].ToString();
                PriceTb.Text = row["BPrice"].ToString();

                if (String.IsNullOrEmpty(BTitleTb.Text))
                {
                    key = 0;
                }
                else
                {
                    key = Int32.Parse(row["BId"].ToString());
                }
            }
        }
            private void CatCbSearchCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

       

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            show();
            CatCbSearchCb.SelectedIndex=-1;
           
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
           reset();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (key==0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();

                    string query = "Delete from BookTbl where BId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Deleted SuccessFully");

                    conn.Close();
                    show();
                    Filter();
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



            }
        }

        private void Editbtn_Click(object sender, RoutedEventArgs e)
        {
             if(String.IsNullOrEmpty(BTitleTb.Text)|| String.IsNullOrEmpty(BautTb.Text)|| String.IsNullOrEmpty(QtyTb.Text)|| String.IsNullOrEmpty(PriceTb.Text)|| String.IsNullOrEmpty(BCatCb.Text)) {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();

                    string query="update BookTbl set BTitle='"+BTitleTb.Text+"' ,BAuthor ='"+BautTb.Text+"',BCat='"+BCatCb.Text+"',BQty='"+QtyTb.Text+"',BPrice='"+PriceTb.Text+"' where BId='"+key+"';";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Saved SuccessFully");

                    conn.Close();
                        show();
                    Filter();
                    reset();
                }
                catch(Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                

                
            }
        }
      /*  public void loadEmployer()
        {
            try
            {
                SqlCommand cm = new SqlCommand("SELECT * FROM BookTbl", conn);
                conn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    yourDataItems.Add(new YourDataItems
                    {
                        BId = Convert.ToInt32(dr["BId"]),
                        BTitle = dr["BTitle"].ToString(),
                        BAuthor = dr["BAuthor"].ToString(),
                        BCat = dr["BCat"].ToString(),
                        BQty = Convert.ToInt32(dr["BQty"]),
                        BPrice = Convert.ToDecimal(dr["BPrice"]),
                        // Add other properties as needed
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }*/
      /*  private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = search.Text.ToLowerInvariant();

            try
            {
                // Cast the ItemsSource of the datagridview to a DataTable
                DataTable dataTable = ((DataView)datagridview.ItemsSource).Table;

                // Add a computed column with lowercase values
                if (!dataTable.Columns.Contains("LowercaseBTitle"))
                {
                    DataColumn lowerBTitleColumn = new DataColumn("LowercaseBTitle", typeof(string), "LOWER(BTitle)");
                    dataTable.Columns.Add(lowerBTitleColumn);
                }

                // Apply a RowFilter to the DataView based on the computed column
                ((DataView)datagridview.ItemsSource).RowFilter = $"LowercaseBTitle LIKE '%{searchText}%' OR LOWER(BAuthor) LIKE '%{searchText}%' OR LOWER(BCat) LIKE '%{searchText}%'";

                // Clear any existing selection in the datagridview
                datagridview.UnselectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/
    }
}
