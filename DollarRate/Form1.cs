using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DollarRate
{
    public partial class Form1 : Form
    {
        String[] lines;
        BL b;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadCSVFile();
        }
        public void LoadCSVFile()
        {
            //upload csv file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "csv files (*.csv)|*.csv";
            try
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    lines = File.ReadAllLines(filePath);
                    //check that the file is not empty
                    if (Array.Exists(lines, element => element == ""))
                    {
                        throw new Exceptions("empty file");
                    }
                    b = new BL();
                    b.createList(lines, 1);
                    drawGrahp(b.currencyValuesPerDay);
                    this.comboBox1.Visible = true;
                    BindingSource bs = new BindingSource();
                    bs.DataSource = b.currencyTypes;
                    comboBox1.DataSource = bs;
                }
            }
            catch (Exceptions e)
            {
                MessageBox.Show(e.Mmessage);
            }
        }

        //draw a dynamic graph
        public void drawGrahp(List<CurrencyValuePerDay> currencyValuePerDay)
        {
            this.chart1.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("Currency Rate");
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Series.Clear();
            Series series1 = new Series("Currency Rate");
            series1.ChartArea = "Currency Rate";
            series1.XValueType = ChartValueType.Date;
            series1.IsXValueIndexed = true;
            series1.ChartType = SeriesChartType.Line;
            this.chart1.Series.Add(series1);
            ToolTip toolTip1 = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 500;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            for (int i = 0; i < currencyValuePerDay.Count; i++)
            {
                series1.Points.AddXY(currencyValuePerDay[i].Date, currencyValuePerDay[i].CurrencyValue);
                series1.ToolTip = string.Format("{0}, {1}", currencyValuePerDay[i].Date.ToString(), currencyValuePerDay[i].CurrencyValue);
            }
            this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            // set Y-Axis's position
            chart1.ChartAreas[0].AxisX.Crossing = 0;
            // set X-Axis's position
            chart1.ChartAreas[0].AxisY.Crossing = 0;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BL b = new BL();
            b.createList(lines, comboBox1.SelectedIndex);
            drawGrahp(b.currencyValuesPerDay);
        }
    }
}

