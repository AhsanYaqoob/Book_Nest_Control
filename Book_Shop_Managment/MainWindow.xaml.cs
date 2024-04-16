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
using System.Windows.Threading;

namespace Book_Shop_Managment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private int progressBarValue;
      
        public MainWindow()
        {
            // Initialize and configure the timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10); // Set the interval to 1 second
            timer.Tick += Timer_Tick;

            // Start the timer
            timer.Start();

            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Increment the progress bar value
            progressBarValue += 1;
            Percentagelbl.Content= progressBarValue+" %";

            // Update the progress bar
            progressBar.Value = progressBarValue;
            if (progressBarValue >= 70 && progressBarValue <= 90)
            {
                timer.Interval = TimeSpan.FromMilliseconds(20); 
            }
            if (progressBarValue >= 95 && progressBarValue <= 100)
            {
                timer.Interval = TimeSpan.FromMilliseconds(100); 
            }
            // Example: Close the splash screen when the progress bar reaches a certain value
            if (progressBarValue >= 101)
            {
                
                timer.Stop();
               Login log = new Login();
                log.Show();
                this.Close();
                // Stop the timer
                // Close the splash screen
            }

        }
    }
}


