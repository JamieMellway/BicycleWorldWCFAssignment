using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml.Serialization;
using BicycleWorldEntityModel;
using BicycleWorldObjectModel;

namespace BicycleWorldCore
{
    public sealed class ShoppingCartCore
    {
        // This is used to turn XML session saving on or off.
        private bool isXmlSessionSavingEnabled = false;
        
        private static readonly ShoppingCartCore current = new ShoppingCartCore();
        static ShoppingCartCore() { }
        private ShoppingCartCore() { }
        public static ShoppingCartCore Current { get { return current; } }

        private List<ShoppingCartItem> shoppingCart = new List<ShoppingCartItem>();

        // Save the shopping cart for the current user to a local XML  
        // file named after the user 
        private void saveShoppingCart()
        {
            if (isXmlSessionSavingEnabled)
            {
                string userName = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                foreach (char badChar in Path.GetInvalidFileNameChars())
                {
                    userName = userName.Replace(badChar, '!');
                }
                string fileName = "ShoppingCartSessions\\" + userName + ".xml";
                TextWriter writer = new StreamWriter(fileName);

                XmlSerializer ser = new XmlSerializer(typeof(List<ShoppingCartItem>));
                ser.Serialize(writer, shoppingCart);
                writer.Close();
            }
        }

        // Restore the shopping cart for the current user from the local XML  
        // file named after the user 
        private void restoreShoppingCart()
        {
            if (isXmlSessionSavingEnabled)
            {
                string userName = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                foreach (char badChar in Path.GetInvalidFileNameChars())
                {
                    userName = userName.Replace(badChar, '!');
                }
                string fileName = "ShoppingCartSessions\\" + userName + ".xml";

                if (File.Exists(fileName))
                {
                    TextReader reader = new StreamReader(fileName);

                    XmlSerializer ser = new XmlSerializer(typeof(List<ShoppingCartItem>));
                    shoppingCart = (List<ShoppingCartItem>)ser.Deserialize(reader);
                    reader.Close();
                }
            }
        }

        // Examine the shopping cart to determine whether an item with a  
        // specified product number has already been added. 
        // If so, return a reference to the item, otherwise return null 
        private ShoppingCartItem find(List<ShoppingCartItem> shoppingCart,
                                      string productNumber)
        {
            foreach (ShoppingCartItem item in shoppingCart)
            {
                if (string.Compare(item.ProductNumber, productNumber) == 0)
                {
                    return item;
                }
            }

            return null;
        }

        private bool decrementStockLevel(string productNumber)
        {
            // Decrement the current stock level of the selected product 
            // in the ProductInventory table.
            // If the update is successful then return true, otherwise return false.

            // The Product and ProductInventory tables are joined over the 
            // ProductID column.

            try
            {
                // Connect to the AdventureWorks database by using the Entity Framework
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    // Find the ProductID for the specified product
                    int productID =
                        (from p in database.Products
                         where String.Compare(p.ProductNumber, productNumber) == 0
                         select p.ProductID).First();

                    // Update the first row for this product in the ProductInventory table
                    // that has a quantity value greater than zero.
                    Product productInventory = database.Products.First(
                        pi => pi.ProductID == productID && pi.Quantity > 0);

                    // Update the stock level for the ProductInventory object
                    productInventory.Quantity--;

                    // Save the change back to the database
                    database.SaveChanges();
                }
            }
            catch
            {
                // If an exception occurs, return false to indicate failure
                return false;
            }

            // Return true to indicate success
            return true;
        }

        public bool AddItemToCart(string productNumber)
        {
            // Note: For clarity, this method performs very limited
            // security checking and exception handling
            try
            {
                // Check to see whether the user has already added this
                // product to the shopping cart
                restoreShoppingCart();
                ShoppingCartItem item = find(shoppingCart, productNumber);

                // If so, then simply increment the volume
                if (item != null)
                {
                    if (decrementStockLevel(productNumber))
                    {
                        item.Quantity++;
                        saveShoppingCart();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                // Otherwise, retrieve the details of the product from the database
                else if (decrementStockLevel(productNumber))
                {
                    // Connect to the AdventureWorks database by using the Entity Framework
                    using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                    {
                        // Retrieve the details of the selected product
                        Product product = (from p in database.Products
                                           where string.Compare(p.ProductNumber, productNumber) == 0
                                           select p).First();

                        // Create and populate a new shopping cart item
                        ShoppingCartItem newItem = new ShoppingCartItem
                        {
                            ProductNumber = product.ProductNumber,
                            ProductName = product.Name,
                            Cost = product.ListPrice,
                            OrderBy = GetNextOrderByNumber(),
                            Quantity = 1
                        };

                        // Add the new item to the shopping cart
                        shoppingCart.Add(newItem);
                        saveShoppingCart();

                        // Indicate success
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                // If an error occurs, finish and indicate failure
                return false;
            }
        }

        private int GetNextOrderByNumber()
        {
            int result = 0;
            foreach (var item in shoppingCart)
            {
                if (item.OrderBy > result)
                    result = item.OrderBy;
            }

            return result + 1;
        }

        public bool RemoveItemFromCart(string productNumber)
        {
            // Determine whether the specified product has an  
            // item in the shopping cart 
            restoreShoppingCart();
            ShoppingCartItem item = find(shoppingCart, productNumber);

            // If so, then decrement the volume 
            if (item != null)
            {
                item.Quantity--;

                // If the volume is zero, remove the item from the shopping cart 
                if (item.Quantity == 0)
                {
                    shoppingCart.Remove(item);
                }

                // Indicate success 
                saveShoppingCart();
                return true;
            }

            // No such item in the shopping cart 
            return false;
        }

        public string GetShoppingCart()
        {
            // Create a string holding a formatted representation  
            // of the shopping cart 
            string formattedContent = String.Empty;
            decimal totalCost = 0;

            restoreShoppingCart();
            foreach (ShoppingCartItem item in shoppingCart)
            {
                string itemString = String.Format(
                       "Number: {0}\tName: {1}\tCost: {2:C}\tVolume: {3}",
                       item.ProductNumber, item.ProductName, item.Cost,
                       item.Quantity);
                totalCost += (item.Cost * item.Quantity);
                formattedContent += itemString + "\n";
            }

            string totalCostString = String.Format("\nTotalCost: {0:C}", totalCost);
            formattedContent += totalCostString;
            return formattedContent;
        }

        public List<ShoppingCartItem> GetShoppingCartList()
        {
            return shoppingCart;
        }

        public bool Checkout()
        {
            string userName = ServiceSecurityContext.Current.PrimaryIdentity.Name;

            SalesOrderCore.SaveSalesOrder(shoppingCart, userName);
            OperationContext.Current.SetTransactionComplete();
            shoppingCart.Clear();
            saveShoppingCart();

            return true;
        }
    }
}
