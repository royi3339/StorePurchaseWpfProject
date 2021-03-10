using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using BE;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Accord.MachineLearning.Rules;

namespace BL
{
    /// <summary>
    /// BLimp class implementation.
    /// </summary>
    public class BLimp
    {
        Util util;
        Dalimp myDAL;
        QRScanner myQRScanner = new QRScanner();

        List<Product> allProducts;

        public BLimp()
        {
            util = new Util();
            myDAL = new Dalimp();
            allProducts = myDAL.GetProducts();
        }

        public void saveProduct(Product product) { myDAL.saveProduct(product); }

        // return only the Products that we scanned.
        public List<Product> GetProductsFromQRScanner()
        {
            myQRScanner.AuthenticateAndListContent();
            return convertIdToProduct(myQRScanner.lstRes.ToList());
        }

        // return the entire catalog.
        public List<Product> GetProducts()
        {
            return allProducts;
        }

        public List<Order> getAllOrders()
        {
            return myDAL.getAllOrders();
        }

        public List<OrderRow> getAllOrderRows()
        {
            return myDAL.getAllOrderRows();
        }

        public void saveOrder(List<Product> products, float price)
        {
            List<OrderRow> order = new List<OrderRow>();

            foreach (Product prod in products)
            {
                if (prod.amount > 0)
                    order.Add(new OrderRow(prod.id, prod.amount));
            }
            myDAL.saveOrder(order, price);
        }


        /// <summary>
        /// A method that converting int id List of Product to a Product List type.
        /// </summary>
        /// <param name="lst"> The List of id that we want to convert. </param>
        /// <returns> The converted Product List. </returns>
        public List<Product> convertIdToProduct(List<int> lst)
        {
            List<Product> newProd = new List<Product>();
            foreach (int a in lst)
            {
                Product temp = convertIdToProduct(a);
                if (temp != null && !newProd.Contains(temp))
                {
                    newProd.Add(temp);
                }
            }
            return newProd;
        }

        public Product convertIdToProduct(int id)
        {
            return allProducts.Where(y => y.id == id).FirstOrDefault();
        }

        public AssociationRule<int>[] getRules()
        {
            SortedSet<int>[] dataSets = myDAL.loadForApiriori();

            Apriori apriori = new Apriori(1, 0.7);
            AssociationRuleMatcher<int> classsifier = apriori.Learn(dataSets);
            return classsifier.Rules;
        }

        public List<Product> recommendedProducts(List<Product> products)
        {
            AssociationRule<int>[] rules = getRules();

            // the id list that we want to convert into Product when we recommended on them.
            List<int> recommendedId = new List<int>();
            List<Product> recommendedProducts = new List<Product>();
            List<int> givenProductsIdList = (products.Where(y => y.amount > 0).Select(y => y.id)).ToList();

            givenProductsIdList.Add(util.convertDay(DateTime.Now));
            givenProductsIdList.Add(util.convertDayPeriod(DateTime.Now));

            foreach (var rule in rules)
            {
                if (rule.X.IsSubsetOf(givenProductsIdList))
                {
                    recommendedId = recommendedId.Union(rule.Y.Except(givenProductsIdList)).ToList();
                }
            }

            foreach (int id in recommendedId)
            {
                if (id < 0) { continue; }
                Product temp = convertIdToProduct(id);
                if (temp != null)
                {
                    recommendedProducts.Add(temp);
                }
            }
            return recommendedProducts;
        }

        public List<string> getRulesAsString()
        {
            AssociationRule<int>[] rules = getRules();
            List<string> rulesRes = new List<string>();
            foreach (var rule in rules)
            {
                if (rule.Y.All(a => a >= 0))
                {
                    rulesRes.Add(setToString(rule.X) + " ==> " + setToString(rule.Y));
                }
            }
            return rulesRes;
        }

        public string setToString(SortedSet<int> set)
        {
            string r = "";
            foreach (int id in set)
            {
                if (id < 0)
                {
                    r += util.convertTimeIdToString(id) + " ";
                }
                else
                {
                    r += convertIdToProduct(id).name + " ";
                }
            }
            return r;
        }

        public void CreatePdfForStoreRecomendations(List<Product> recommandProducts)
        {
            Font x = FontFactory.GetFont("nina fett");
            x.Size = 50;
            x.SetStyle("Italic");
            x.SetColor(255, 0, 0);

            Paragraph c2 = new Paragraph("Recommanded Products Just For You:\n  ", x);
            c2.IndentationLeft = 40;

            Document doc = new Document(PageSize.A4, 0f, 0f, 5f, 0f);
            PdfWriter.GetInstance(doc, new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Recommended Stores.pdf", FileMode.Create));
            doc.Open();
            doc.Add(c2);

            x.Size = 20;
            x.SetColor(0, 42, 255);

            PdfPTable table = new PdfPTable(2);

            PdfPCell cell = new PdfPCell(new Phrase("Product Picture"));
            cell.Colspan = 1;
            //0=Left, 1=Centre, 2=Right
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            PdfPCell cell2 = new PdfPCell(new Phrase("Product Details"));
            cell2.Colspan = 1;
            //0=Left, 1=Centre, 2=Right
            cell2.HorizontalAlignment = 1;
            table.AddCell(cell2);

            doc.Add(table);

            foreach (Product prod in recommandProducts)
            {
                Image Prodimg = Image.GetInstance("../../" + prod.imagePath);

                PdfPCell cellExample = new PdfPCell(Prodimg);
                cellExample.Colspan = 1;
                cellExample.HorizontalAlignment = 1;

                PdfPTable recTable = new PdfPTable(2);
                recTable.AddCell(Image.GetInstance(Prodimg));

                Paragraph par5 = new Paragraph("\n\t" + prod.name + "\n\n\t" + prod.manufacturer + "\n\n\t" + prod.cost + " NIS", x);
                par5.IndentationLeft = 400;

                recTable.AddCell(par5);
                doc.Add(recTable);
            }
            doc.Close();
        }

        public void CreatePdfForCheck(List<Product> purchaseProducts)
        {
            Font x = FontFactory.GetFont("nina fett");
            x.Size = 50;
            x.SetStyle("Italic");
            x.SetColor(255, 0, 0);

            Document doc = new Document(PageSize.A4, 0f, 0f, 5f, 0f);
            PdfWriter.GetInstance(doc, new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Purchase Check.pdf", FileMode.Create));
            doc.Open();

            Paragraph c2 = new Paragraph("Your Check For This Purchase:\n  ", x);
            c2.IndentationLeft = 40;
            doc.Add(c2);

            x.Size = 20;
            x.SetColor(0, 42, 255);

            PdfPTable table = new PdfPTable(2);

            PdfPCell cell2 = new PdfPCell(new Phrase("Product Details"));
            cell2.Colspan = 1;
            //0=Left, 1=Centre, 2=Right
            cell2.HorizontalAlignment = 1;
            table.AddCell(cell2);

            doc.Add(table);

            PdfPTable topTable = new PdfPTable(5);
            PdfPCell topCell1 = new PdfPCell(new Phrase("Product Name"));
            topCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell topCell2 = new PdfPCell(new Phrase("Product Manufacturer"));
            topCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell topCell3 = new PdfPCell(new Phrase("Product Amount"));
            topCell3.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell topCell4 = new PdfPCell(new Phrase("Product Cost"));
            topCell4.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell topCell5 = new PdfPCell(new Phrase("Sum Product Price"));
            topCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            topTable.AddCell(topCell1);
            topTable.AddCell(topCell2);
            topTable.AddCell(topCell3);
            topTable.AddCell(topCell4);
            topTable.AddCell(topCell5);
            doc.Add(topTable);

            float purchasePrice = 0;
            float prodPrice = 0;

            foreach (Product prod in purchaseProducts)
            {
                if (prod.amount == 0) { continue; }

                prodPrice = prod.amount * prod.cost;

                PdfPTable purTable = new PdfPTable(5);
                PdfPCell infoCell1 = new PdfPCell(new Phrase(prod.name));
                infoCell1.BorderColor = new BaseColor(255, 255, 255);
                infoCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell infoCell2 = new PdfPCell(new Phrase(prod.manufacturer));
                infoCell2.BorderColor = new BaseColor(255, 255, 255);
                infoCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell infoCell3 = new PdfPCell(new Phrase(prod.amount.ToString()));
                infoCell3.BorderColor = new BaseColor(255, 255, 255);
                infoCell3.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell infoCell4 = new PdfPCell(new Phrase(prod.cost + " NIS"));
                infoCell4.BorderColor = new BaseColor(255, 255, 255);
                infoCell4.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell infoCell5 = new PdfPCell(new Phrase(prodPrice + " NIS"));
                infoCell5.BorderColor = new BaseColor(255, 255, 255);
                infoCell5.HorizontalAlignment = Element.ALIGN_CENTER;
                purTable.AddCell(infoCell1);
                purTable.AddCell(infoCell2);
                purTable.AddCell(infoCell3);
                purTable.AddCell(infoCell4);
                purTable.AddCell(infoCell5);

                purchasePrice += prodPrice;

                doc.Add(purTable);
            }

            Paragraph c3 = new Paragraph("Your Total Purchase Cost is:\t" + purchasePrice + " NIS" + "\n  ", x);
            c3.IndentationLeft = 40;
            doc.Add(c3);

            doc.Close();
        }
    }
}
