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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged 
    {
        IBL myBL = oneBL.GetBL();
        ObservableCollection<Product> prodLst;

        QRScanner myQRScanner;
        public event PropertyChangedEventHandler PropertyChanged;


        private int _Count;
        public int count
        {
            get { return _Count; }
            set
            {
                _Count = value;
                update("count");
            }
        }

        private void update(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }                    

        public MainWindow()
        {
            InitializeComponent();
            prodLst = new ObservableCollection<Product>();
            myQRScanner = myBL.getQRScanner();
            DataContext = myQRScanner;
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    Product p = new Product(1, "milk", "tara", 6, new bool[4] { false, false, false, true }, 1.1, "milk.jpg");
            //    myBL.addProduct(p);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            //try
            //{
            //    Product pp = new Product(2, "bamba", "osem", 200, new bool[4] { false, false, false, false }, 0.06, "bamba.png");
            //    myBL.addProduct(pp);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            //myQRScanner.AuthenticateAndListContent();
            myQRScanner.lstRes = new ObservableCollection<int>();
            for (int i = 1; i < 7; i++) { myQRScanner.lstRes.Add(i); }
            prodLst = myBL.convertIdToProduct((myQRScanner.lstRes));
            listOfRes.ItemsSource = prodLst;
        }

        private void plusButton(object sender, RoutedEventArgs e) { ((Product)((Button)sender).DataContext).amount++; }

        private void minosButton(object sender, RoutedEventArgs e) { ((Product)((Button)sender).DataContext).amount--; }

        private void confirmButton(object sender, RoutedEventArgs e)
        {
            if (prodLst.Count() == 0)
            {
                MessageBox.Show("please scan some product to buy first...");
                return;
            }
            
            double totalWeight = 0;
            foreach (Product p in prodLst) { totalWeight += p.weight * p.amount; }
            if (totalWeight == 0)
            {
                MessageBox.Show("please selecet some product to buy first...");
                return;
            }
            MessageBox.Show("your parchase is succesfully confirmed, you weight is: " + totalWeight);
        }
    }
}


//string path = Directory.GetCurrentDirectory();

