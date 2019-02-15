using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProjektWINFORMS
{
    public partial class wykres : Form
    {
        public wykres()
        {
            InitializeComponent();
        }
        

        private void wykres_Load_1(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Legends.Clear();

            chart1.Legends.Add("Legenda");
            chart1.Legends[0].Alignment = StringAlignment.Center;
            chart1.Legends[0].Title = "Ilosc dni: " + Globals.DTable.Rows.Count;
            chart1.Legends[0].BorderColor = Color.Black;

            string serie = "Dni: " + Globals.DTable.Rows.Count;
            chart1.Series.Add(serie);
            chart1.Series[serie].ChartType = SeriesChartType.Bar;

            int itt = 0;
            string colname = "";

            foreach (DataColumn column in Globals.DTable.Columns)
            {
                itt = 0;
                foreach (DataRow row in Globals.DTable.Rows)
                {
                    if (column.ColumnName != "Data")
                    {
                        if (!row[column].ToString().Contains('-'))
                        {
                            try
                            {
                                int p = 0;
                                int.TryParse(row[column].ToString(), out p);
                                itt += p;
                                colname = column.ColumnName;
                            }
                            catch
                            {
                                itt += 0;
                                colname = column.ColumnName;
                            }
                        }
                    }
                }
                if (itt != 0 && colname != "")
                {
                    chart1.Series[serie].Points.AddXY(colname, itt);
                }
            }
        }
    }
}
