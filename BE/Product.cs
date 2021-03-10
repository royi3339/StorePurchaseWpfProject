using System;
using System.ComponentModel.DataAnnotations.Schema;
using static BE.myEnums;

namespace BE 
{
    /// <summary>
    /// Product.
    /// </summary>
    [Serializable]
    public class Product
    {      
        /// <summary>
        /// The id of the Product.
        /// </summary>
        public int id { get; set; }


        /// <summary>
        /// The name of the Product.
        /// </summary>      
        public string name { get; set; }


        /// <summary>
        /// The manufacturer of the Product.
        /// </summary>
        public string manufacturer { get; set; }


        /// <summary>
        /// The cost of the Product.
        /// </summary>
        public float cost { get; set; }


        /// <summary>
        /// The health recommendation of the Product.
        /// </summary>
        public bool natran { get; set; }


        /// <summary>
        /// The health recommendation of the Product.
        /// </summary>
        public bool sugar { get; set; }


        /// <summary>
        /// The health recommendation of the Product.
        /// </summary>
        public bool saturatedFat { get; set; }


        /// <summary>
        /// The health recommendation of the Product.
        /// </summary>
        public bool green { get; set; }


        /// <summary>
        /// The weight of the Product.
        /// </summary>
        public double weight { get; set; }


        /// <summary>
        /// The path of the image of the Product.
        /// </summary>
        public string imagePath { get; set; }


        /// <summary>
        /// The amount of the Product.
        /// </summary>
        [NotMapped]
        public int amount { get; set; }
                

        public categorieType categorie { get; set; }


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
        public Product(int id, string name, string manufacturer, float cost, bool natran, bool sugar, bool saturatedFat, bool green, double weight, string imagePath, categorieType categorie)
        {
            this.id = id;
            this.name = name;
            this.manufacturer = manufacturer;
            this.cost = cost;
            this.natran = natran;
            this.sugar = sugar;
            this.saturatedFat = saturatedFat;
            this.green = green;
            this.weight = weight;
            this.imagePath = imagePath;
            this.categorie = categorie;
        }

        /// <summary>
        /// A default constructor of Product class.
        /// </summary>
        public Product() { }
    }    
}
