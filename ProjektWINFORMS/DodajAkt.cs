using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektWINFORMS
{
    public partial class DodajAkt : Form
    {
        public DodajAkt()
        {
            InitializeComponent();
            for (var i = 0; i < Globals.Excercises.Count; i++)
                for (var j = 0; j < Globals.Excercises[i].Count; j++)
                    if (j > 0)
                        comboBox1.Items.Add(Globals.Excercises[i][0] + ": " + Globals.Excercises[i][j]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ex = comboBox1.Text;
            string count = textBox1.Text;
            string sDate = monthCalendar1.SelectionRange.Start.ToShortDateString();
            int intCount;
            var regexItem = new Regex("^[0-9]*$");
            bool conversionResult = Int32.TryParse(count, out intCount);
            if (!conversionResult && (regexItem.IsMatch(count)))
                MessageBox.Show("Czy ty aby nie przesadzasz?!");
            else if (conversionResult == false || intCount <= 0 || ex.Length < 0 || sDate.Length < 0)
                MessageBox.Show("Wybierz datę, ćwiczenie oraz ilość powtórzeń większą od 0");


            if (conversionResult == true && intCount > 0 && ex.Length > 0 && sDate.Length > 0)
            {
                Globals.doneExcercises.Add(new List<string> {sDate, ex, count});
                if (!Globals.DTable.Columns.Contains("Data"))
                    Globals.DTable.Columns.Add("Data", typeof(string));
                
                Globals.Excercises.Sort((a, b) => a[1].CompareTo(b[1]));

                for (var i = 0; i < Globals.Excercises.Count; i++)
                    for (var j = 0; j < Globals.Excercises[i].Count; j++)
                        if (j > 0 && !Globals.DTable.Columns.Contains(Globals.Excercises[i][0] + ": " + Globals.Excercises[i][j]))
                            Globals.DTable.Columns.Add(Globals.Excercises[i][0] + ": " + Globals.Excercises[i][j], typeof(string));

                var dateAlreadyExist = Globals.DTable.Select("Data = '" + sDate + "'");
                if (dateAlreadyExist.Length > 0)
                {
                    DataRow dr = Globals.DTable.Select("Data= '" + sDate + "'").FirstOrDefault();
                    if (dr != null)
                    {
                        dr[ex] = count;
                    }
                }
                else
                {

                    DataRow row = Globals.DTable.NewRow();
                    row["Data"] = sDate;
                    row[ex] = count;
                    Globals.DTable.Rows.Add(row);
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
