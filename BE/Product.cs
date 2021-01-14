using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Xml.Serialization;


namespace BE 
{
    /// <summary>
    /// Product.
    /// </summary>
    [Serializable]
    public class Product : INotifyPropertyChanged  
    {
        private int _Id;
        /// <summary>
        /// The id of the Product.
        /// </summary>
        public int id 
        {
            get { return _Id; }
            set
            {
                _Id = value;
                update("id");
            }
        }
                
        private string _Name;
        /// <summary>
        /// The name of the Product.
        /// </summary>   
        public string name 
        {
            get { return _Name; }
            set 
            {
                _Name = value;
                update("name");
            }
        }
        
        private string _Manufacturer;
        /// <summary>
        /// The manufacturer of the Product.
        /// </summary>
        public string manufacturer
        {
            get { return _Manufacturer; }
            set
            {
                _Manufacturer = value;
                update("manufacturer");
            }
        }

        private float _Cost;
        /// <summary>
        /// The cost of the Product.
        /// </summary>
        public float cost
        {
            get { return _Cost; }
            set
            {
                _Cost = value;
                update("cost");
            }
        }

        private bool[] _HealthRecom;
        /// <summary>
        /// The health recommendation of the Product.
        /// </summary>
        public bool[] healthRecom //= new bool[4]    // natran,sugar,shuman ravuy,green
        {
            get { return _HealthRecom; }
            set
            {
                _HealthRecom = value;
                update("healthRecom");
            }
        }

        private double _Weight;
        /// <summary>
        /// The weight of the Product.
        /// </summary>
        public double weight
        {
            get { return _Weight; }
            set
            {
                _Weight = value;
                update("weight");
            }
        }

        private string _ImagePath;
        /// <summary>
        /// The path of the image of the Product.
        /// </summary>
        public string imagePath
        {
            get { return _ImagePath; }
            set
            {
                _ImagePath = value;
                update("imagePath");
            }
        }

        [XmlIgnore]
        private int _Amount = 1;
        /// <summary>
        /// The amount of the Product.
        /// </summary>
        [XmlIgnore]
        public int amount
        {
            get { return _Amount; }
            set
            {
                if (value >= 0)
                {
                    _Amount = value;
                    update("amount");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Updating the value with the "PropertyChanged".
        /// </summary>
        /// <param name="propName"> The name of the property that we change. </param>
        private void update(string propName)
        {
            if (PropertyChanged != null)                
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// the fuul constructor of Product class.
        /// </summary>
        /// <param name="id"> The id of the Product. </param>
        /// <param name="name"> The name of the Product. </param>
        /// <param name="manufacturer"> The manufacturer of the Product. </param>
        /// <param name="cost"> The cost of the Product. </param>
        /// <param name="healthRecom"> The health recommendation of the Product. </param>
        /// <param name="weight"> The weight of the Product. </param>
        /// <param name="imagePath"> The path of the image of the Product. </param>
        public Product(int id, string name, string manufacturer, float cost, bool[] healthRecom, double weight, string imagePath)
        {                     
            healthRecom = new bool[4];
            this.id = id;
            this.name = name;
            this.manufacturer = manufacturer;
            this.cost = cost;
            this.healthRecom = healthRecom;
            this.weight = weight;
            this.imagePath = imagePath;
        }

        /// <summary>
        /// A default constructor of Product class.
        /// </summary>
        public Product() { }
    }    
}
