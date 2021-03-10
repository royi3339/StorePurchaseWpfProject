using BE;
using PL.Commands;
using PL.model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BL;

namespace PL.VM
{
    public class purchaseProductVM : viewModel
    {
        public ObservableCollection<ProductVM> productsList { get; set; }

        private string _StatusButton; 
        public string statusButton
        {
            get { return _StatusButton; }
            set
            {
                _StatusButton = value;
                update("statusButton");
            }
        }

        private float _TotalPrice;
        public float totalPrice
        {
            get { return _TotalPrice; }
            set
            {
                _TotalPrice = value;
                update("totalPrice");
            }
        }

        public purchaseProductModel model;
        public genericCommand confirmB { get; set; }
        public genericCommand loadFromAppB { get; set; }
        public genericCommand showProductsOnlineB { get; set; }
        public genericCommand suggestProductsB { get; set; }

        BLimp myBL = new BLimp();

        converter myConverter = new converter();

        private bool _PurchaseVisible;
        public bool purchaseVisible
        {
            get { return _PurchaseVisible; }
            set
            {
                _PurchaseVisible = value;
                update("purchaseVisible");
            }
        }
                
        public purchaseProductVM()
        {
            statusButton = "Show Products Online";
            model = new purchaseProductModel();
            productsList = new ObservableCollection<ProductVM>();
            confirmB = new genericCommand();
            confirmB.genericClickEvent += confirmFoo;
            loadFromAppB = new genericCommand();
            loadFromAppB.genericClickEvent += loadFromAppFoo;
            showProductsOnlineB = new genericCommand();
            showProductsOnlineB.genericClickEvent += showProductsOnlineFoo;
            suggestProductsB = new genericCommand();
            suggestProductsB.genericClickEvent += suggestProductsFoo;
        }

        public void totalPriceCalculator()
        {
            float price = 0;
            foreach (ProductVM p in productsList) { price += p.cost * p.amount; }
            totalPrice = price;
        }

        private void loadFromAppFoo()
        {
            model.products = myBL.GetProductsFromQRScanner();
            myConverter.convertToObservableCollection(model.products, productsList, totalPriceCalculator, 1);
            statusButton = "Show Products Online";
        }

        private void confirmFoo()
        {
            if (productsList.Count() == 0)
            {
                MessageBox.Show("please scan some product to buy first...");
                return;
            }

            double totalWeight = 0;
            foreach (ProductVM p in productsList) { totalWeight += p.weight * p.amount; }            
            
            if (totalWeight == 0)
            {
                MessageBox.Show("please selecet some product to buy first...");
                return;
            }
            MessageBox.Show("your parchase is succesfully confirmed, you weight is: " + totalWeight + "kg. \nthe cost is: " + totalPrice + "₪");

            myBL.CreatePdfForCheck(model.products);

            myBL.saveOrder(model.products, totalPrice);
        }

        private void showProductsOnlineFoo()
        {
            model.products = new List<Product>(myBL.GetProducts());
            myConverter.convertToObservableCollection(model.products, productsList, totalPriceCalculator, 0);
            statusButton = "Reset";
        }

        private void suggestProductsFoo()
        {
            List<Product> recProducts = myBL.recommendedProducts(model.products);
            myBL.CreatePdfForStoreRecomendations(recProducts);
            foreach (Product recProd in recProducts)
            {
                if (!model.products.Contains(recProd))
                {
                    recProd.amount = 0;
                    model.products.Add(recProd);
                    ProductVM temp = new ProductVM(recProd);
                    temp.amountChanged += totalPriceCalculator;
                    productsList.Add(temp);
                }
            }
            MessageBox.Show("The shopping cart have been updated");
        }
    }
}
