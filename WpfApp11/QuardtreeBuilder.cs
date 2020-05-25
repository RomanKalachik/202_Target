using System;
using System.Collections.Generic;
using System.Linq;
namespace WpfApp11 {
    public class QuardtreeBuilder {
        public static QuadtreeNode BuildTree(List<DataRow> data)
        {
            QuadtreeNode node = new QuadtreeNode();
            QuadtreeNode startNode = node;
            for (int zoomLevel = 10; zoomLevel > 1; zoomLevel -= 2)
            {
                QuadtreeNode newParent = null;
                int counter = 0;

                foreach (DataRow row in data)
                {
                    SimpleNode newNode = new SimpleNode() { X = row.Argument, Y = row.Value, SourceIndex = counter };
                    node = startNode;
                    int maxDepth = QuadtreeNode.MaxIterations;
                    do
                    {
                        newParent = node.Connect(newNode, zoomLevel / 20.0);
                        if (newParent != null)
                            node = newParent;
                        if (maxDepth < 1)
                        {
                            throw new Exception("too large data");
                        }
                    }
                    while (newParent != null && maxDepth-- > 0);
                    counter++;
                }
            }
            return startNode;
        }
        public static QuadtreeNode GetEdgeNode(QuadtreeNode startNode, QuadtreeNodeTypes corner)
        {
            QuadtreeNode result = startNode;
            QuadtreeNode newParent = null;
            int maxDepth = QuadtreeNode.MaxIterations;
            do
            {
                newParent = result.GetChildNode(corner);
                if (newParent != null)
                    result = newParent;
                if (maxDepth < 1)
                {
                    throw new Exception("too large data");
                }
            }
            while (newParent != null && maxDepth-- > 0);
            return result;
        }
        public static Tuple<Range, Range> GetWholeRange(QuadtreeNode startNode)
        {
            QuadtreeNode ne = GetEdgeNode(startNode, QuadtreeNodeTypes.NE);
            QuadtreeNode nw = GetEdgeNode(startNode, QuadtreeNodeTypes.NW);
            QuadtreeNode se = GetEdgeNode(startNode, QuadtreeNodeTypes.SE);
            QuadtreeNode sw = GetEdgeNode(startNode, QuadtreeNodeTypes.SW);
            return new Tuple<Range, Range>(
                new Range() { Min = SimpleNode.GetMin(nw.X, sw.X), Max = SimpleNode.GetMax(ne.X, se.X) },
                new Range() { Min = SimpleNode.GetMin(sw.Y, se.Y), Max = SimpleNode.GetMax(ne.Y, nw.Y) });
        }
    }
}