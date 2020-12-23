using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Product
    {
        public int id;
        public string name;
        public string manufacturer;
        public float cost;
        public bool[] healthRecom = new bool[4];
        public double weight;
        public string imagePath;

        public Product(int id, string name, string manufacturer, float cost, bool[] healthRecom, double weight, string imagePath)
        {
            this.id = id;
            this.name = name;
            this.manufacturer = manufacturer;
            this.cost = cost;
            this.healthRecom = healthRecom;
            this.weight = weight;
            this.imagePath = imagePath;
        }
    }    
}
