using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace WpfApp9 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();
            pointSeries.ArgumentDataMember = "Argument";
            pointSeries.ValueDataMember = "Value";

            pointSeries.DataSource = DataGenerator.Generate2();

        }
    }

    public class DataRow {
        public double Argument { get; set; }
        public double Value { get; set; }
    }
    public class DataGenerator {
        public static List<DataRow> Generate()
        {
            List<DataRow> result = new List<DataRow>();
            for (double i = 0; i < 15000; i++)
            {
                double circleArg = Math.PI * 2 / 1000 * i;
                double circleAmp = 100 + i / 100;
                double x = circleAmp * Math.Cos(circleArg);
                double y = circleAmp *Math.Sin(circleArg);
                result.Add(new DataRow() { Argument = x, Value = y });
            }
            return result;
        }
        public static List<DataRow> Generate2()
        {
            List<DataRow> result = new List<DataRow>();
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "WpfApp9.2dPointData.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string csv = reader.ReadToEnd();
              var lines =  csv.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                int counter = lines.Length;
                foreach(var line in lines)
                {
                    if (line.Length < 5) continue;
                    var args = line.Split(new char[] { ',' });
                    result.Add(new DataRow() { Argument = double.Parse(args[0]), Value = double.Parse(args[1]) });
                    if (counter-- <= 0) break;

                }
            }
            return result;
        }

    }
}
