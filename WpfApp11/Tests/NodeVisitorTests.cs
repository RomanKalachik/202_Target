using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp11.Tests {
    [TestFixture]
    public class NodeVisitorTests {
        [Test]
        public void Visitor_Simple()
        {
            List<DataRow> data = new List<DataRow>();
            data.Add(new DataRow() { Argument = 0.5, Value = 0.5, });
            data.Add(new DataRow() { Argument = 0, Value = 0, });
            data.Add(new DataRow() { Argument = 1, Value = 0, });
            data.Add(new DataRow() { Argument = 0, Value = 1, });
            data.Add(new DataRow() { Argument = 1, Value = 1, });

            QuadtreeNode startNode = QuardtreeBuilder.BuildTree(data);
            List<SimpleNode> result = new List<SimpleNode>();
            startNode.VisitNodes(new Tuple<Range, Range>(new Range() { Min = -2.0, Max = 2.0 }, new Range() { Min = -2.0, Max = 2.0 }), 1, node => { result.Add(node); });

            Assert.AreEqual(5, result.Count);
        }
    }
}