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

namespace SL_Project_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                FSXConnectionManager.TestSimconnect();
            }
            catch (System.IO.FileLoadException e)
            {
                MessageBox.Show("Please install SimConnect SDK");
                Environment.Exit(-1);
            }

            InitializeComponent();
            DataContext = new MasterViewModel(myMap);
        }
    }
}
