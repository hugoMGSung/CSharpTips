using MetroFramework.Forms;
using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartTestApp
{
    public partial class MainForm : MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnYValues_Click(object sender, EventArgs e)
        {
            ChtMain.Series["Score"].Points.Clear();
            ChtMain.Series["Score"].Points.Add(98);
            ChtMain.Series["Score"].Points.Add(72);
            ChtMain.Series["Score"].Points.Add(50);
            ChtMain.Series["Score"].Points.Add(100);
        }

        private void BtnXyValues_Click(object sender, EventArgs e)
        {
            ChtMain.Series["Score"].Points.Clear();
            ChtMain.Series["Score"].Points.AddXY(10, 98);
            ChtMain.Series["Score"].Points.AddXY(20, 72);
            ChtMain.Series["Score"].Points.AddXY(40, 50);
            ChtMain.Series["Score"].Points.AddXY(30, 100);

            //ChtMain.Series["Score"].ChartType = SeriesChartType.Line;
            //ChtMain.Series["Score"].ChartType = SeriesChartType.FastLine;
            //ChtMain.Series["Score"].ChartType = SeriesChartType.Spline;
            ChtMain.Series["Score"].ChartType = SeriesChartType.Area;
            //ChtMain.Series["Score"].ChartType = SeriesChartType.Pie;
        }
    }
}
