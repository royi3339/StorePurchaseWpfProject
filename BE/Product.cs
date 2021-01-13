using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;


namespace BE 
{
    public class Product : INotifyPropertyChanged  
    {
        private int _Id;
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
        public string imagePath
        {
            get { return _ImagePath; }
            set
            {
                _ImagePath = value;
                update("imagePath");
            }
        }
        
        private void update(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;


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

        public Product()
        {
        }
    }    
}
