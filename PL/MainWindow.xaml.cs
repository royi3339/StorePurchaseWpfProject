using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;//3339
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE;
using System.Collections.ObjectModel;
using ZXing;
using System.IO;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL myBL = oneBL.GetBL();
        ObservableCollection<Product> prodLst;

        QRScanner myQRScanner;
        int count;
        
        //public static ObservableCollection<Result> toDeleteIt;// { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            // DataContext = prodLst;   
            myQRScanner = myBL.getQRScanner();
            count = myQRScanner.count;
            DataContext = count;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    Product p = new Product(1, "milk", "tara", 6, new bool[4] { false, false, false, true }, 1.1, "milk.jpg");
            //    bl.addProduct(p);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            //try
            //{
            //    Product pp = new Product(2, "bamba", "osem", 200, new bool[4] { false, false, false, false }, 0.06, "bamba.png");
            //    bl.addProduct(pp);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            myQRScanner.AuthenticateAndListContent();
            prodLst = myBL.convertIdToProduct((myQRScanner.lstRes));

            listOfRes.ItemsSource = prodLst;
            string path = Directory.GetCurrentDirectory();

            count++;
        }
    }
}
