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
    //   asdfghjkl
    //public class Class1
    //{
    //}

    public class Dal_XML_imp //: Idal
    {
        private const string productPath = @"productXml.xml";
        private XElement productRoot;

        public Dal_XML_imp()
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


        public void addProduct(Product unit)
        {
            if (IsExistProduct(unit.id))
                throw new DuplicateIdException("Product ", unit.id);
            //unit.id = Int32.Parse(productRoot.Element("ProductSerialNum").Value) + 1;/////////////////////////////////////////////////////////////////
            //productRoot.Element("ProductSerialNum").Value = unit.id.ToString();///////////////////////////////////////////////////////////////////////
            //productRoot.Save(productPath);
            ObservableCollection<Product> productList = LoadFromXML<ObservableCollection<Product>>(productPath);
            productList.Add(unit);
            SaveToXML(productList, productPath);
        }

        private void loadProduct() { productRoot = XElement.Load(productPath); }

        public IEnumerable<Product> getAllProducts() { return LoadFromXML<ObservableCollection<Product>>(productPath); }

        public void removeProduct(int id)
        {
            if (!IsExistProduct(id))
                throw new MissingIdException("Product ", id);
            List<Product> list = LoadFromXML<List<Product>>(productPath);
            list.Remove(list.FirstOrDefault(unit => unit.id == id));
            SaveToXML(list, productPath);
        }

        private bool IsExistProduct(int id)
        {
            List<Product> units = LoadFromXML<List<Product>>(productPath);
            return units.Any(unit => unit.id == id);
        }
        public void updateProduct(Product unit)
        {
            if (!IsExistProduct(unit.id))
                throw new MissingIdException("Product ", unit.id);
            removeProduct(unit.id);
            List<Product> unitslist = LoadFromXML<List<Product>>(productPath);
            unitslist.Add(unit);
            SaveToXML(unitslist, productPath);
        }

        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            XmlSerializer xmlSer = new XmlSerializer(source.GetType());
            xmlSer.Serialize(file, source);
            file.Close();
        }
        public static T LoadFromXML<T>(string path)
        {

            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            T result = (T)xmlSer.Deserialize(file);
            file.Close();
            return result;
        }
    }
}
