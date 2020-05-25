using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
namespace WpfApp11 {
    public class Generator {
        public static List<DataRow> Generate2()
        {
            List<DataRow> result = new List<DataRow>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "WpfApp11.2dPointData.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string csv = reader.ReadToEnd();
                string[] lines = csv.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
               int counter =lines.Length -1;
                foreach (string line in lines)
                {
                    if (line.Length < 5)
                        continue;
                    string[] args = line.Split(new char[] { ',' });
                    result.Add(new DataRow() { Argument = double.Parse(args[0]), Value = double.Parse(args[1]) });
                    if (counter-- <= 0)
                        break;
                }
            }
            return result;
        }
    }
}