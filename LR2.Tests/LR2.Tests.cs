using NUnit.Framework;
using System;

namespace LR2.Tests
{
    [TestFixture]

    public class Tests
    {
        private Table table1;
        private Table table2;
        private Table table3;
        [SetUp]
        public void Setup()
        {
            table1 = new Table();
            table2 = new Table();
            table3 = new Table();
        }

        [TestCase(0, 4, 2)]
        public void CreationTest(int line, int column, double value)
        {

            Cell cell = new Cell(line, column, value);
            Assert.AreEqual(cell.line, line);
            Assert.AreEqual(cell.column, column);
            Assert.AreEqual(cell.value, value);
        }
      
        [Test]
        public void GetAddTest()
        {
            table1.Add(1, 3, 1.1);
            Assert.IsTrue(table1.Get(1, 3) == 1.1);
        }
        [Test]
        public void GetEmptyCellTest()
        {
            table1.Add(0, 6, 2.8);
            table1.Add(7, 4, 0.2);
            Assert.Throws<NullReferenceException>(() => table1.Get(2, 3));
        }
        [Test]
        public void CopyValueTest()
        {
            table1.Add(1, 5, 6.3);
            table1.CopyValue(1, 5, 2, 2);
            Assert.IsTrue(table1.Get(2, 2) == 6.3);
            Assert.Throws<NullReferenceException>(() => table1.CopyValue(7, 4, 3, 2));
        }

        [Test]
        public void ModifyTest()
        {
            table1.Add(1, 5, 6.3);
            table1.Modify(1, 5, 1.1);
            Assert.IsTrue(table1.Get(1, 5) == 1.1);
            Assert.Throws<NullReferenceException>(() => table1.Modify(2, 4, 2.2));
        }

       [TestCase(1, 5, 6.3)]
       [TestCase(4, 3, 0.5)]
       [TestCase(0, 6, 2.8)]
       [TestCase(7, 4, 0.2)]
        public void CopyCellTest(int line, int column, double value)
        {
            Cell cell = new Cell(line, column, value);
            table1.Add(1, 5, 6.3);
            table1.Add(4, 3, 0.5);
            table1.Add(0, 6, 2.8);
            table1.Add(7, 4, 0.2);
            table1.GetCellCopy(line, column);
            Assert.AreEqual(cell.line, line);
            Assert.AreEqual(cell.column, column);
            Assert.AreEqual(cell.value, value);
        }

        [TestCase(1, 5, 6.3)]
        [TestCase(4, 3, 0.5)]
        [TestCase(0, 6, 2.8)]
        [TestCase(7, 4, 0.2)]
        public void CopyEmptyCellTest(int line, int column, double value)
        {
            Cell cell = new Cell(line, column, value);
            table1.Add(1, 5, 6.3);
            table1.Add(4, 3, 0.5);
            table1.Add(0, 6, 2.8);
            table1.Add(7, 4, 0.2);
            table1.GetCellCopy(line, column);
            Assert.Throws<NullReferenceException>(() => table1.GetCellCopy(0, 0));
        }

        [Test]
        public void CopyTableTest()
        {
            table1.Add(1, 5, 6.3);
            table1.Add(4, 3, 0.5);
            table1.Add(0, 6, 2.8);
            table1.Add(7, 4, 0.2);
            table2 = table1.CopyTable();
            Assert.IsTrue(table2.Get(1, 5) == 6.3);
            Assert.IsTrue(table2.Get(4, 3) == 0.5);
            Assert.IsTrue(table2.Get(0, 6) == 2.8);
            Assert.IsTrue(table2.Get(7, 4) == 0.2);
        }
        [Test]
        public void MergeTablesTest()
         {
             table1.Add(1, 1, 4.6);
             table1.Add(9, 3, 0.4);
             table1.Add(8, 2, 6.1);
             table1.Add(3, 5, 11.4);
             table2.Add(1, 1, 2.1);
             table2.Add(8, 2, 1.1);
             table2.Add(4, 2, 3.8);
             table2.Add(5, 4, 7.9);
             table3 = Table.Merge(table1, table2);
             Assert.IsTrue(Math.Abs(table3.Get(1, 1) - 6.7) < 0.00001);
             Assert.IsTrue(Math.Abs(table3.Get(9, 3) - 0.4) < 0.00001);
             Assert.IsTrue(Math.Abs(table3.Get(8, 2) - 7.2) < 0.00001);
             Assert.IsTrue(Math.Abs(table3.Get(3, 5) - 11.4) < 0.00001);
             Assert.IsTrue(Math.Abs(table3.Get(4, 2) - 3.8) < 0.00001);
             Assert.IsTrue(Math.Abs(table3.Get(5, 4) - 7.9) < 0.00001);
        }
    }

    public class TestWithEx
        {
            [TestCase(-1, 2, 3.4)]
            [TestCase(1, -2, 3.4)]
        public void CreationWithExceptionsTest(int line, int column, double value)
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new Cell(line, column, value));

            }

           
    }
}