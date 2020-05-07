using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfApp9;

namespace WindowsFormsApp1 {
    public partial class Form1 : Form {
        public Form1()
        {
            InitializeComponent();
            chartControl1.UseDirectXPaint = true;
            FPSCalculator.Enabled = true;
            series11.ArgumentDataMember = "Argument";
            series11.ValueDataMembers.AddRange(new string[] { "Value" });

            series11.DataSource = DataGenerator.Generate2();
        }
    }
}
