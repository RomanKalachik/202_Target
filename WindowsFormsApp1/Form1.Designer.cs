using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraLayout;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace WindowsFormsApp1 {
    partial class Form1 {
        ChartControl chartControl1;
        LayoutControlItem chartControl1item;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        IContainer components = null;
        LayoutControl Form1layoutControl1ConvertedLayout;
        LayoutControlGroup layoutControlGroup1;
        PointSeriesView pointSeriesView1;
        Series series11;
        XYDiagram xyDiagram1;
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            xyDiagram1 = new XYDiagram();
            series11 = new Series();
            pointSeriesView1 = new PointSeriesView();
            chartControl1 = new ChartControl();
            Form1layoutControl1ConvertedLayout = new LayoutControl();
            layoutControlGroup1 = new LayoutControlGroup();
            chartControl1item = new LayoutControlItem();
            ((ISupportInitialize)(chartControl1)).BeginInit();
            ((ISupportInitialize)xyDiagram1).BeginInit();
            ((ISupportInitialize)(series11)).BeginInit();
            ((ISupportInitialize)pointSeriesView1).BeginInit();
            ((ISupportInitialize)(Form1layoutControl1ConvertedLayout)).BeginInit();
            Form1layoutControl1ConvertedLayout.SuspendLayout();
            ((ISupportInitialize)(layoutControlGroup1)).BeginInit();
            ((ISupportInitialize)(chartControl1item)).BeginInit();
            SuspendLayout();
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisXZooming = true;
            xyDiagram1.EnableAxisYScrolling = true;
            xyDiagram1.EnableAxisYZooming = true;
            chartControl1.Diagram = xyDiagram1;
            chartControl1.Legend.Name = "Default Legend";
            chartControl1.Location = new Point(12, 12);
            chartControl1.Name = "chartControl1";
            series11.Name = "Series 1";
            pointSeriesView1.PointMarkerOptions.Size = 4;
            pointSeriesView1.PointMarkerOptions.Kind = MarkerKind.Square;
            pointSeriesView1.Color = Color.FromArgb(100, 0, 200, 10);
            series11.View = pointSeriesView1;
            chartControl1.SeriesSerializable = new Series[] {
        series11};
            chartControl1.Size = new Size(776, 440);
            chartControl1.TabIndex = 0;
            Form1layoutControl1ConvertedLayout.Controls.Add(chartControl1);
            Form1layoutControl1ConvertedLayout.Dock = DockStyle.Fill;
            Form1layoutControl1ConvertedLayout.Location = new Point(0, 0);
            Form1layoutControl1ConvertedLayout.Name = "Form1layoutControl1ConvertedLayout";
            Form1layoutControl1ConvertedLayout.Root = layoutControlGroup1;
            Form1layoutControl1ConvertedLayout.Size = new Size(800, 464);
            Form1layoutControl1ConvertedLayout.TabIndex = 4;
            layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] {
            chartControl1item});
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new Size(800, 464);
            layoutControlGroup1.TextVisible = false;
            chartControl1item.Control = chartControl1;
            chartControl1item.Location = new Point(0, 0);
            chartControl1item.Name = "chartControl1item";
            chartControl1item.Size = new Size(780, 444);
            chartControl1item.TextSize = new Size(0, 0);
            chartControl1item.TextVisible = false;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 464);
            Controls.Add(Form1layoutControl1ConvertedLayout);
            Name = "Form1";
            Text = "Form1";
            ((ISupportInitialize)(xyDiagram1)).EndInit();
            ((ISupportInitialize)(pointSeriesView1)).EndInit();
            ((ISupportInitialize)(series11)).EndInit();
            ((ISupportInitialize)(chartControl1)).EndInit();
            ((ISupportInitialize)(Form1layoutControl1ConvertedLayout)).EndInit();
            Form1layoutControl1ConvertedLayout.ResumeLayout(false);
            ((ISupportInitialize)(layoutControlGroup1)).EndInit();
            ((ISupportInitialize)(chartControl1item)).EndInit();
            ResumeLayout(false);

        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

