﻿using AdventureWorksCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BicycleWorldEntityModel;
using System.Collections.Generic;

namespace ProductCore.Tests
{
    
    
    /// <summary>
    ///This is a test class for CategoryCoreTest and is intended
    ///to contain all CategoryCoreTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CategoryCoreTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetCategories
        ///</summary>
        [TestMethod()]
        public void GetCategoriesTest()
        {
            int expected = 54;
             int actual;
             List<ProductCategory>  categories = CategoryCore.GetCategories();
             actual = categories.Count;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CategoryCore Constructor
        ///</summary>
        [TestMethod()]
        public void CategoryCoreConstructorTest()
        {
            CategoryCore target = new CategoryCore();
        }
    }
}
