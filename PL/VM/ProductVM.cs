using System;
using BE;
using PL.Commands;
using static BE.myEnums;

namespace PL.VM
{
    public class ProductVM : viewModel
    {
        public event Action amountChanged;
        public Product product;
                
        /// <summary>
        /// The id of the Product.
        /// </summary>
        public int id
        {
            get { return product.id; }
        }

        /// <summary>
        /// The name of the Product.
        /// </summary>   
        public string name
        {
            get { return product.name; }
            set
            {
                product.name = value;
                update("name");
            }
        }

        /// <summary>
        /// The manufacturer of the Product.
        /// </summary>
        public string manufacturer
        {
            get { return product.manufacturer; }
            set
            {
                product.manufacturer = value;
                update("manufacturer");
            }
        }

        /// <summary>
        /// The cost of the Product.
        /// </summary>
        public float cost
        {
            get { return product.cost; }
            set
            {
                product.cost = value;
                update("cost");
            }
        }

        /// <summary>
        /// The health recommendation of the Product.
        /// </summary>
        public bool natran
        {
            get { return product.natran; }
            set
            {
                product.natran = value;
                update("natran");
            }
        }

        /// <summary>
        /// The health recommendation of the Product.
        /// </summary>
        public bool sugar
        {
            get { return product.sugar; }
            set
            {
                product.sugar = value;
                update("sugar");
            }
        }

        /// <summary>
        /// The health recommendation of the Product.
        /// </summary>
        public bool saturatedFat
        {
            get { return product.saturatedFat; }
            set
            {
                product.saturatedFat = value;
                update("aturatedFat");
            }
        }

        /// <summary>
        /// The health recommendation of the Product.
        /// </summary>
        public bool green
        {
            get { return product.green; }
            set
            {
                product.green = value;
                update("green");
            }
        }

        /// <summary>
        /// The weight of the Product.
        /// </summary>
        public double weight
        {
            get { return product.weight; }
            set
            {
                product.weight = value;
                update("weight");
            }
        }

        /// <summary>
        /// The path of the image of the Product.
        /// </summary>
        public string imagePath
        {
            get { return product.imagePath; }
            set
            {
                product.imagePath = value;
                update("imagePath");
            }
        }

        /// <summary>
        /// The amount of the Product.
        /// </summary>
        public int amount
        {
            get { return product.amount; }
            set
            {
                if (value >= 0 && value <= 50)
                {
                    product.amount = value;
                    update("amount");
                    if (amountChanged != null)
                        amountChanged();
                }
            }
        }

        public categorieType categorie
        {
            get { return categorie; }
            set 
            {
                categorie = value;
                update("categorie");
            }
        }

        public genericCommand plusB { get; set; }
        public genericCommand minusB { get; set; }

        public ProductVM(Product p)
        {
            product = p;
            update("full Product");
            plusB = new genericCommand();
            plusB.genericClickEvent += PlusButtonFoo;
            minusB = new genericCommand();
            minusB.genericClickEvent += minusButtonFoo;
        }

        private void PlusButtonFoo() { amount++; }

        private void minusButtonFoo() { amount--; }
    }
}
