using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
namespace WpfApp11 {
    public partial class MainWindow : Window {
        double dx = 0;
        double dy = 0;
        Point mdPoint;
        double ox = 0, oy = 0;
        double prevWidth = 0;
        QuadtreeNode startNode;
        Tuple<Range, Range> visibleRange;
        Tuple<Range, Range> wholeRange;
        double zoomFactor = 1;
        public MainWindow()
        {
            InitializeComponent();
            List<DataRow> data = Generator.Generate2();
            startNode = QuardtreeBuilder.BuildTree(data, 20);
            wholeRange = QuardtreeBuilder.GetWholeRange(startNode);
            LayoutUpdated += MainWindow_LayoutUpdated;
        }
        Rect CalcScreenRect(SimpleNode node)
        {
            double x = (node.RefinedX - visibleRange.Item1.RefinedMin) / visibleRange.Item1.RefinedDiff * ActualWidth;
            double y = (node.RefinedY - visibleRange.Item2.RefinedMin) / visibleRange.Item2.RefinedDiff * ActualHeight;
            return new Rect(x, y, 4, 4);
        }
        void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mdPoint = e.GetPosition(this);
        }
        void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPoint = e.GetPosition(this);
                dx = currentPoint.X - mdPoint.X;
                dy = currentPoint.Y - mdPoint.Y;
                UpdateCanvas();
            }
        }
        void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ox -= dx;
            oy -= dy;
            dx = 0;
            dy = 0;
        }
        void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            zoomFactor += e.Delta / 200.0;
            if (zoomFactor < 1) zoomFactor = 1;
            UpdateCanvas();
        }
        void MainWindow_LayoutUpdated(object sender, EventArgs e)
        {
            if (prevWidth == ActualWidth || ActualWidth == 0) return;
            prevWidth = ActualWidth;
            UpdateCanvas();
        }
        void UpdateCanvas()
        {
            double rdx = wholeRange.Item1.RefinedMin + (ox - dx) / (ActualWidth) * wholeRange.Item1.RefinedDiff;
            double rdy = wholeRange.Item2.RefinedMin + (oy - dy) / (ActualHeight) * wholeRange.Item2.RefinedDiff;

            double wx = wholeRange.Item1.RefinedDiff / zoomFactor;
            double wy = wholeRange.Item2.RefinedDiff / zoomFactor;

            visibleRange = new Tuple<Range, Range>(new Range() { Min = rdx, Max = wx }, new Range() { Min = rdy, Max = wy });

            Path path = new Path();
            path.Stroke = null;
            path.Fill = Brushes.Blue;

            GeometryGroup gg = new GeometryGroup();
            gg.FillRule = FillRule.Nonzero;
            startNode.VisitNodes(visibleRange, zoomFactor, (node) =>
            {
                    gg.Children.Add(new RectangleGeometry(CalcScreenRect(node)));
            });
            path.Data = gg;
            Title = string.Format("Current zoomLevel:{0} , rectangles count = {1}", zoomFactor.ToString(), gg.Children.Count.ToString());
            canvas.Children.Clear();
            canvas.Children.Add(path);
        }
    }
}