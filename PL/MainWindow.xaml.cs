using System.Windows;
using PL.VM;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public mainWindowVM currentVM;

        public MainWindow()
        {
            InitializeComponent();
            currentVM = new mainWindowVM();
            DataContext = currentVM;
        }
    }
}

