using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BicycleWorldServiceModelClient.BicycleWorldService
{
    public partial class ProductData
    {
        public override string ToString()
        {
            return String.Format("Number: {0}\nName: {1}\nCategory: {2}\nDescription: {3}\nColor: {4}\nList Price: {5:c}", this.ProductNumber, this.Name, this.CategoryName, this.ProductDescription, this.Color, this.ListPrice);
        }

        public string ProductToolTip { get { return this.ToString(); } }
    }
}
