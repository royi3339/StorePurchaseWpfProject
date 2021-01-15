using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using BE;
using System.ComponentModel;
using System.Threading;
using System.Net;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace DAL
{
    /// <summary>
    /// DalXMLimp.
    /// </summary>
    public class DalXMLimp : IDal
    {
        private const string productPath = @"productXml.xml";
        private XElement productRoot;

        /// <summary>
        /// DalXMLimp constructor.
        /// </summary>
        public DalXMLimp()
        {
            bool f = File.Exists(productPath);
            if (!File.Exists(productPath))
            {
                List<Product> products = new List<Product>();
                SaveToXML<List<Product>>(products, productPath);
            }
            //if (!File.Exists(productPath))
            //CreateFileProduct();
            // loadProduct();
        }

        /// <summary>
        /// Adding a new Product to the Xml Product List.
        /// </summary>
        /// <param name="p"> The new Product that we want to add. </param>
        public void addProduct(Product p)
        {
            if (IsExistProduct(p.id))
                throw new DuplicateIdException("Product ", p.id);
            //unit.id = Int32.Parse(productRoot.Element("ProductSerialNum").Value) + 1;/////////////////////////////////////////////////////////////////
            //productRoot.Element("ProductSerialNum").Value = unit.id.ToString();///////////////////////////////////////////////////////////////////////
            //productRoot.Save(productPath);
            ObservableCollection<Product> productList = LoadFromXML<ObservableCollection<Product>>(productPath);
            productList.Add(p);
            SaveToXML(productList, productPath);
        }

        /// <summary>
        /// Loading the Products from the XML file.
        /// </summary>
        private void loadProduct() { productRoot = XElement.Load(productPath); }

        /// <summary>
        /// Returning all the Products that in the XML file.
        /// </summary>
        /// <returns> IEnumerable<Product>. </returns>
        public IEnumerable<Product> getAllProducts() { return LoadFromXML<ObservableCollection<Product>>(productPath); }

        /// <summary>
        /// Removing a Product from the Products XML file.
        /// </summary>
        /// <param name="id"> The id of the Product that we want to remove from the Products XML file. </param>
        public void removeProduct(int id)
        {
            if (!IsExistProduct(id))
                throw new MissingIdException("Product ", id);
            List<Product> list = LoadFromXML<List<Product>>(productPath);
            list.Remove(list.FirstOrDefault(unit => unit.id == id));
            SaveToXML(list, productPath);
        }

        /// <summary>
        /// Checking if a Product exist.
        /// </summary>
        /// <param name="id"> The id of the Product that we want to check if it exist in the Products XML file. </param>
        /// <returns></returns>
        private bool IsExistProduct(int id)
        {
            List<Product> units = LoadFromXML<List<Product>>(productPath);
            return units.Any(unit => unit.id == id);
        }

        /// <summary>
        /// Updating a Product.
        /// </summary>
        /// <param name="unit"> The id of the Product that we want to update from the Products XML file. </param>
        public void updateProduct(Product unit)
        {
            if (!IsExistProduct(unit.id))
                throw new MissingIdException("Product ", unit.id);
            removeProduct(unit.id);
            List<Product> unitslist = LoadFromXML<List<Product>>(productPath);
            unitslist.Add(unit);
            SaveToXML(unitslist, productPath);
        }

        /// <summary>
        /// Saving to the XML file.
        /// </summary>
        /// <typeparam name="T"> The source type. </typeparam>
        /// <param name="source"> The source. </param>
        /// <param name="path"> The path. </param>
        private void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            XmlSerializer xmlSer = new XmlSerializer(source.GetType());
            xmlSer.Serialize(file, source);
            file.Close();
        }

        /// <summary>
        /// Loading a Product from the XML file.
        /// </summary>
        /// <typeparam name="T"> The returning type. </typeparam>
        /// <param name="path"> The path. </param>
        /// <returns> T </returns>
        private T LoadFromXML<T>(string path)
        {

            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            T result = (T)xmlSer.Deserialize(file);
            file.Close();
            return result;
        }
    }
}
