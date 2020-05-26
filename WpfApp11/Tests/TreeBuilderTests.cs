using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp11.Tests {
    [TestFixture]
    public class TreeBuilderTests {
        [Test]
        public void TectBuildTree_Simple()
        {
            List<DataRow> data = new List<DataRow>();
            data.Add(new DataRow() { Argument = 0.5, Value = 0.5, });
            data.Add(new DataRow() { Argument = 0, Value = 0, });
            data.Add(new DataRow() { Argument = 1, Value = 0, });
            data.Add(new DataRow() { Argument = 0, Value = 1, });
            data.Add(new DataRow() { Argument = 1, Value = 1, });

            QuadtreeNode startNode = QuardtreeBuilder.BuildTree(data, 20);
            Assert.AreEqual(startNode.X, 0.5);
            Assert.AreEqual(startNode.Y, 0.5);

            Assert.AreEqual(startNode.NE.X, 1);
            Assert.AreEqual(startNode.NE.Y, 1);

            Assert.AreEqual(startNode.NW.X, 0);
            Assert.AreEqual(startNode.NW.Y, 1);

            Assert.AreEqual(startNode.SW.X, 0);
            Assert.AreEqual(startNode.SW.Y, 0);

            Assert.AreEqual(startNode.SE.X, 1);
            Assert.AreEqual(startNode.SE.Y, 0);

        }
        [Test]
        public void TectBuildTree_Large()
        {
            List<DataRow> data = Generator.Generate2();
            QuadtreeNode startNode = QuardtreeBuilder.BuildTree(data);
            var wholeRange = QuardtreeBuilder.GetWholeRange(startNode);
        }
        [Test]
        public void TestMin()
        {
            Assert.IsTrue((int)SimpleNode.GetMin(1, 2) == 1);
            Assert.IsTrue((int)SimpleNode.GetMin(2, 1) == 1);

        }
        [Test]
        public void TestMax()
        {
            Assert.IsTrue((int)SimpleNode.GetMax(1, 2) == 2);
            Assert.IsTrue((int)SimpleNode.GetMax(2, 1) == 2);
        }
    }

}