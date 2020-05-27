using System;
using System.Collections.Generic;
using System.Linq;
namespace WpfApp11 {
    public class QuadtreeNode : SimpleNode {
        public const int MaxIterations = 250000;
        QuadtreeNodeTypes CalculateOrientation(IComparable x, IComparable y)
        {
            QuadtreeNodeTypes whereToAdd;
            int we = X.CompareTo(x);
            int ns = Y.CompareTo(y);
            if (ns == 0 && we == 0)
                whereToAdd = QuadtreeNodeTypes.Self;
            else
            {
                if (ns > 0)
                {
                    if (we > 0)
                        whereToAdd = QuadtreeNodeTypes.SW;
                    else
                        whereToAdd = QuadtreeNodeTypes.SE;
                }
                else
                {
                    if (we > 0)
                        whereToAdd = QuadtreeNodeTypes.NW;
                    else
                        whereToAdd = QuadtreeNodeTypes.NE;
                }
            }
            return whereToAdd;
        }
       
        bool CanSimplify(SimpleNode newNode, int zoomLevel, int zoomLevelsCount)
        {
            if (IsEmpty) return false;
            double minDistance = (double)zoomLevel /(2 * zoomLevelsCount);
            if (zoomLevel == 1) return false;
            return Math.Abs(RefinedX - newNode.RefinedX) < minDistance && Math.Abs(RefinedY - newNode.RefinedY) < minDistance;
        }
        void CheckNodes(QuadtreeNodeTypes hint)
        {
            if (NW == null && hint == QuadtreeNodeTypes.NW)
                NW = new QuadtreeNode();
            if (NE == null && hint == QuadtreeNodeTypes.NE)
                NE = new QuadtreeNode();
            if (SW == null && hint == QuadtreeNodeTypes.SW)
                SW = new QuadtreeNode();
            if (SE == null && hint == QuadtreeNodeTypes.SE)
                SE = new QuadtreeNode();
        }
        public QuadtreeNode Connect(SimpleNode newNode, byte zoomLevel, int zoomLevelsCount)
        {
            QuadtreeNode whereToAdd = null;

            if (IsEmpty)
                whereToAdd = this;
            else
            {
                if (CanSimplify(newNode, zoomLevel, zoomLevelsCount))
                {
                    whereToAdd = this;
                }
                else
                    whereToAdd = GetChildNode(CalculateOrientation(newNode.X, newNode.Y), true);
            }
            if (whereToAdd != null)
            {
                if (whereToAdd.IsEmpty)
                {
                    whereToAdd.X = newNode.X;
                    whereToAdd.Y = newNode.Y;
                    whereToAdd.SourceIndex = newNode.SourceIndex;
                    whereToAdd.ZoomLevel = zoomLevel;
                }
                else
                {
                    if (whereToAdd != this)
                        return whereToAdd;
                }
            }
            return null;
        }
        public QuadtreeNode GetChildNode(QuadtreeNodeTypes type, bool forceCreate = false)
        {
            if (forceCreate)
                CheckNodes(type);
            switch (type)
            {
                case QuadtreeNodeTypes.NW:
                    return NW;
                case QuadtreeNodeTypes.NE:
                    return NE;
                case QuadtreeNodeTypes.SW:
                    return SW;
                case QuadtreeNodeTypes.SE:
                    return SE;
                case QuadtreeNodeTypes.Self:
                default:
                    return this;
            }
        }
        public void VisitNodes(Tuple<Range, Range> visibleRange, double zoomLevel, Action<SimpleNode> callback)
        {
            QuadtreeNode currentNode = this;
            int watchdog = MaxIterations;
            Stack<QuadtreeNode> nodes = new Stack<QuadtreeNode>();
            do
            {
                QuadtreeNodeTypes orientationMin = currentNode.CalculateOrientation(visibleRange.Item1.Min, visibleRange.Item2.Min);
                QuadtreeNodeTypes orientationMax = currentNode.CalculateOrientation(visibleRange.Item1.Max, visibleRange.Item2.Max);
                foreach (QuadtreeNodeTypes nodeType in Enum.GetValues(typeof(QuadtreeNodeTypes)))
                {
                    QuadtreeNode node = currentNode.GetChildNode(nodeType);
                    if (node == null || node.IsEmpty) continue;
                    if (node.ZoomLevel < zoomLevel) continue;
                    if (node == currentNode)
                    {
                        callback(node);
                        continue;
                    }
                    if (orientationMax == orientationMin && orientationMin != nodeType)
                        continue;
                    if (orientationMin == QuadtreeNodeTypes.SW && orientationMax == QuadtreeNodeTypes.SE && (nodeType == QuadtreeNodeTypes.NE || nodeType == QuadtreeNodeTypes.NW))
                        continue;
                    if (orientationMin == QuadtreeNodeTypes.SW && orientationMax == QuadtreeNodeTypes.NW && (nodeType == QuadtreeNodeTypes.NE || nodeType == QuadtreeNodeTypes.SE))
                        continue;
                    if (orientationMin == QuadtreeNodeTypes.NW && orientationMax == QuadtreeNodeTypes.NE && (nodeType == QuadtreeNodeTypes.SE || nodeType == QuadtreeNodeTypes.SW))
                        continue;
                    nodes.Push(node);
                }
                if (nodes.Count == 0) break;
                currentNode = nodes.Pop();
            }
            while (nodes.Count >= 0 && watchdog-- > 0);
        }
        public QuadtreeNode NE { get; set; }
        public QuadtreeNode NW { get; set; }
        public QuadtreeNode SE { get; set; }
        public QuadtreeNode SW { get; set; }
        public byte ZoomLevel { get; set; }
    }
    public enum QuadtreeNodeTypes : byte {
        NW = 0,
        NE = 1,
        SW = 2,
        SE = 3,
        Self = 4
    }
}