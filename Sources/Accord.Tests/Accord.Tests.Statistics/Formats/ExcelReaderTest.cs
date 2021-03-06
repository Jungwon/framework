﻿// Accord Unit Tests
// The Accord.NET Framework
// http://accord.googlecode.com
//
// Copyright © César Souza, 2009-2013
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

using Accord.Statistics.Formats;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Accord.Tests.Statistics
{

    [TestClass()]
    public class ExcelReaderTest
    {


        private TestContext testContextInstance;


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



        [TestMethod()]
        public void ConstructorExcel8Test()
        {
            string path = @"..\..\..\Accord.Tests\Accord.Tests.Statistics\Resources\sample.xls";
            ExcelReader target = new ExcelReader(path);

            testWorksheets(target);

            testColumns(target, false);

            testTables(target);
        }

        [TestMethod()]
        public void ConstructorExcel10Test()
        {
            // If a 64-bit ACE is installed, this test requires a 64-bit process to run correctly.
            string path = @"..\..\..\Accord.Tests\Accord.Tests.Statistics\Resources\sample.xlsx";
            ExcelReader target = new ExcelReader(path);

            testWorksheets(target);

            testColumns(target, true);

            testTables(target);
        }

        private static void testTables(ExcelReader target)
        {
            DataSet set = target.GetWorksheet();
            Assert.AreEqual(4, set.Tables.Count);
            Assert.AreEqual("Plan1", set.Tables[0].TableName);
            Assert.AreEqual("Plan2", set.Tables[1].TableName);
            Assert.AreEqual("Plan3", set.Tables[2].TableName);
            Assert.AreEqual("Sheet1", set.Tables[3].TableName);
        }

        private static void testWorksheets(ExcelReader target)
        {
            string[] list = target.GetWorksheetList();
            Assert.AreEqual(4, list.Length);
            Assert.AreEqual("Plan1", list[0]);
            Assert.AreEqual("Plan2", list[1]);
            Assert.AreEqual("Plan3", list[2]);
            Assert.AreEqual("Sheet1", list[3]);
        }

        private static void testColumns(ExcelReader target, bool xlsx)
        {
            string[] cols = target.GetColumnsList("Plan1");
            Assert.AreEqual(3, cols.Length);
            Assert.AreEqual("Header1", cols[0]);
            Assert.AreEqual("Header2", cols[1]);
            Assert.AreEqual("Header3", cols[2]);

            cols = target.GetColumnsList("Plan2");
            Assert.AreEqual(3, cols.Length);
            if (target.Provider == "Microsoft.Jet.OLEDB.4.0")
            {
                Assert.AreEqual("F1", cols[0]);
                Assert.AreEqual("F2", cols[1]);
                Assert.AreEqual("F3", cols[2]);
            }
            else
            {
                Assert.AreEqual("1", cols[0]);
                Assert.AreEqual("2", cols[1]);
                Assert.AreEqual("3", cols[2]);
            }

            cols = target.GetColumnsList("Plan3");
            Assert.AreEqual(2, cols.Length);
            Assert.AreEqual("A", cols[0]);
            Assert.AreEqual("B", cols[1]);

            cols = target.GetColumnsList("Sheet1");
            Assert.AreEqual(1, cols.Length);
            Assert.AreEqual("F1", cols[0]);
        }



    }
}
