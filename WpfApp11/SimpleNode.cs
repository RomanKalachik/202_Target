using System;
using System.Linq;
namespace WpfApp11 {
    public class SimpleNode {
        public static IComparable GetMax(IComparable item1, IComparable item2)
        {
            if (item1.CompareTo(item2) > 0) return item1;
            else return item2;
        }
        public static IComparable GetMin(IComparable item1, IComparable item2)
        {
            if (item1.CompareTo(item2) > 0) return item2;
            else return item1;
        }
        public bool IsEmpty { get { return X == null || Y == null; } }
        public double RefinedX { get { return (double)X; } }
        public double RefinedY { get { return (double)Y; } }
        public int SourceIndex { get; set; }
        public IComparable X { get; set; }
        public IComparable Y { get; set; }
    }
}