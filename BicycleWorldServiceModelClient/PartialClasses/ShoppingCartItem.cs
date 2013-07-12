using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BicycleWorldServiceModelClient.BicycleWorldService
{
    public partial class ShoppingCartItem
    {
        public override string ToString()
        {
            return String.Format("Quantity: {0}\nNumber: {1}\nName: {2}\nUnit Cost: {3:c}\nSubtotal: {4:c}", this.Quantity, this.ProductNumber, this.ProductName, this.Cost, this.Cost * this.Quantity);
        }

        public string ShoppingCartToolTip { get { return this.ToString(); } }
    }
}
