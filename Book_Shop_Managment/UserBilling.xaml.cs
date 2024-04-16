using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Book_Shop_Managment
{
    /// <summary>
    /// Interaction logic for UserBilling.xaml
    /// </summary>
    public partial class UserBilling : UserControl
    {
        private ObservableCollection<object> billItems = new ObservableCollection<object>();
        SqlConnection conn = new SqlConnection("Data Source=AHSAN\\SQLEXPRESS;Initial Catalog=Book_Shop_Management_System;Integrated Security=True;TrustServerCertificate=true");
        int  key=0,stock=0;
        int n = 0,GridTotal=0;
        Login login;
        

        public UserBilling()
        {
            InitializeComponent();
            Billdatagridview.ItemsSource = billItems;
            show();
            login = new Login();
            UserName.Content = Login.username;

            CatCbSearchCb.Items.Add("Programming");
            CatCbSearchCb.Items.Add("Networking");
            CatCbSearchCb.Items.Add("Art Books");
            CatCbSearchCb.Items.Add("Poetry Books");
            CatCbSearchCb.Items.Add("Biography");
           


        }
        private void show()
        {
            conn.Open();
            string query = "Select * from BookTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            BookListDGV.ItemsSource = dataTable.DefaultView;
            conn.Close();

        }

        private void reset()
        {
            BTitleTb.Text = string.Empty;
           
            ClientNameTb.Text =string.Empty;
           
            QtyTb.Text = string.Empty;
            PriceTb.Text = string.Empty;
        }

        private void UpdateBook()
        {
            int newstock=stock-Int32.Parse(QtyTb.Text);
            try
            {
                conn.Open();

                string query = "UPDATE BookTbl SET BQty=" + newstock + " WHERE BId=" + key + ";";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Saved SuccessFully");

                conn.Close();
                show();
                // Filter();
                //reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
           
            if(String.IsNullOrEmpty(QtyTb.Text)|| Int32.Parse(QtyTb.Text)> stock)
            {
                MessageBox.Show("No Enough Stock");
            }
            else
            {

                int total = Int32.Parse(QtyTb.Text) * Int32.Parse(PriceTb.Text);
                int quantity = Int32.Parse(QtyTb.Text);
                int price = Int32.Parse(PriceTb.Text);

                billItems.Add(new { Id = billItems.Count + 1, Books = BTitleTb.Text, Price = price, Quantity = quantity, Total = total });
                //Billdatagridview.Items.Refresh();
                n++;
                UpdateBook();
                GridTotal = total + GridTotal;
                TotalLbl.Content=GridTotal.ToString();

            }
        }

        private void BookListDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookListDGV.SelectedItem != null)
            {
                DataRowView row = (DataRowView)BookListDGV.SelectedItem;

                BTitleTb.Text = row["BTitle"].ToString();
               // QtyTb.Text = row["BQty"].ToString();
                PriceTb.Text = row["BPrice"].ToString();

                if (String.IsNullOrEmpty(BTitleTb.Text))
                {
                    key = 0;
                    stock = 0;
                }
                else
                {
                    key = Int32.Parse(row["BId"].ToString());
                    stock = Int32.Parse(row["BQty"].ToString());
                }
            }
        }

        private void Editbtn_Click(object sender, RoutedEventArgs e)
        {

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
                BookListDGV.ItemsSource = dataTable.DefaultView;
            }
            conn.Close();


        }

        private void Printbtn_Click(object sender, RoutedEventArgs e)
        {
            /* PrintDialog printDialog = new PrintDialog();
             if (printDialog.ShowDialog() == true)
             {
                 PrintDocument(printDialog.PrintQueue);
             }*/

            
            if (String.IsNullOrEmpty(ClientNameTb.Text) || String.IsNullOrEmpty(BTitleTb.Text))
            {
                MessageBox.Show("Set CLient Information Information");
            }
            else
            {
                try
                {
                    string receiptContent = GenerateReceiptContent(); // Customize this method to format your receipt content

                    // Display the receipt content in a MessageBox
                    MessageBox.Show(receiptContent, "Receipt Preview", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Save the receipt content to a text file
                    SaveToFile(receiptContent, "Receipt.txt");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during preview: {ex.Message}", "Preview Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                try
                {
                    conn.Open();

                    string query = "Insert into BillTbl Values('" + UserName.Content + "' , '" + ClientNameTb.Text + "','" + TotalLbl.Content+ "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved SuccessFully");

                    conn.Close();
                    Billdatagridview.ItemsSource = null;
                    reset();
                    show();
                  //  Filter();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



            }
        }


        private string GenerateReceiptContent()
        {
            StringBuilder receiptContent = new StringBuilder();

            receiptContent.AppendLine("Receipt Details:");
            receiptContent.AppendLine("-----------------");

            foreach (var item in billItems)
            {
                dynamic dynamicItem = item;
                receiptContent.AppendLine($"Book: {dynamicItem.Books}, Quantity: {dynamicItem.Quantity}, Price: {dynamicItem.Price}, Total: {dynamicItem.Total}");
            }

            receiptContent.AppendLine("-----------------");
            receiptContent.AppendLine($"Total: RS {GridTotal}");
            receiptContent.AppendLine($"Client Name: {ClientNameTb.Text}");

            // Save the content to a text file on the desktop with a unique name
            string fileName = $"Receipt_{ClientNameTb.Text}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt";
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
            System.IO.File.WriteAllText(filePath, receiptContent.ToString());

            return receiptContent.ToString();
        }







        private void SaveToFile(string content, string fileName)
        {
            // Save the content to a text file
            System.IO.File.WriteAllText(fileName, content);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            show();
            CatCbSearchCb.SelectedIndex = -1;
        }

        private void CatCbSearchCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        /* private void PrintDocument(PrintQueue printQueue)
{
    PrintDialog printDialog = new PrintDialog();
    FixedDocument fixedDocument = new FixedDocument();

    foreach (var item in billItems)
    {
        PageContent itemPageContent = CreateItemPage(item);
        fixedDocument.Pages.Add(itemPageContent);
    }

    // Create a DocumentPaginator for the FixedDocument
    DocumentPaginator documentPaginator = ((IDocumentPaginatorSource)fixedDocument).DocumentPaginator;

    // Set a default PageSize or a custom size
    documentPaginator.PageSize = new Size(700, 900); // Adjust the size as needed

    // Print the document
    printDialog.PrintDocument(documentPaginator, "Print Job Title");
}

private PageContent CreateItemPage(object item)
{
    FixedPage fixedPage = new FixedPage();

    // Add your item details to fixedPage
    TextBlock textBlock = new TextBlock();
    textBlock.Text = GetItemDetails(item); // Customize this method based on your DataGrid structure
    fixedPage.Children.Add(textBlock);

    PageContent pageContent = new PageContent();
    ((IAddChild)pageContent).AddChild(fixedPage);

    return pageContent;
}

private String GetItemDetails(object item)
{
    if (item is DataRowView row)
    {
        string bookTitle = row["BTitle"].ToString();
        int quantity = Int32.Parse(QtyTb.Text);
        int price = Int32.Parse(PriceTb.Text);
        int total = quantity * price;

        return $"Book: {bookTitle}, Quantity: {quantity}, Price: {price}, Total: {total}";
    }

    return string.Empty;
}*/

        private void Resetbtn_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        public static implicit operator UserBilling(books v)
        {
            throw new NotImplementedException();
            
        }
    }
}
