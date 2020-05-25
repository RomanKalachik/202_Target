using System;
using System.Linq;
namespace WpfApp11 {
    public class Range {
        public IComparable Max { get; set; }
        public IComparable Min { get; set; }
        public double RefinedMax { get { return (double)Max; } }
        public double RefinedMin { get { return (double)Min; } }
        public double RefinedDiff { get { return RefinedMax - RefinedMin; } }
    }
}