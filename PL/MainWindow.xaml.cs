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
        
        //public static ObservableCollection<Result> toDeleteIt;// { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            // DataContext = prodLst;   
            myQRScanner = myBL.getQRScanner();
            DataContext = myQRScanner;


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

            //myQRScanner.AuthenticateAndListContent();
            myQRScanner.lstRes = new ObservableCollection<int>();
            for (int i = 1; i < 7; i++)
            {
                myQRScanner.lstRes.Add(i);
            }

            prodLst = myBL.convertIdToProduct((myQRScanner.lstRes));

            listOfRes.ItemsSource = prodLst;
            //string path = Directory.GetCurrentDirectory();

        }
    }
}
