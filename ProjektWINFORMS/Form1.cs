using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektWINFORMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Globals.SBind.DataSource = Globals.DTable;
            Globals.dataGridView1.DataSource = Globals.SBind;

            if (!Globals.DTable.Columns.Contains("Data"))
                Globals.DTable.Columns.Add("Data", typeof(string));
            
            if (!Globals.Excercises.Any())
            {
                
                Globals.Excercises.Sort((a, b) => a[1].CompareTo(b[1]));
                for (var i = 0; i < Globals.Excercises.Count; i++)
                    for (var j = 0; j < Globals.Excercises[i].Count; j++)
                        if (j > 0 && !Globals.DTable.Columns.Contains(Globals.Excercises[i][0] + ": " + Globals.Excercises[i][j]))
                            Globals.DTable.Columns.Add(Globals.Excercises[i][0] + ": " + Globals.Excercises[i][j], typeof(string));
            }
            
            dataGridView1.DataSource = Globals.DTable;
           
        }

   

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void stworzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var stworz = new stworz();
            stworz.FormClosed += stworzFormClosed;
            stworz.Show(); 
        }

        private void edytujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.Excercises.Count != 0)
            {
                var edytujCat = new edytujCat();
                edytujCat.FormClosed += edytujCatFromClosed;
                edytujCat.Show();
            }
            else
                MessageBox.Show("Nie mam czego edytować :(");
        }

        private void wczytajToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.Excercises.Count != 0)
            {

            }
            else
            {
                MessageBox.Show("Brak kategorii");
            }
        }

        private void wykresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void stworzFormClosed(object sender, FormClosedEventArgs e)
        {
            if (Globals.visibility)
                this.dataGridView1.Visible = true;
        }
        private void edytujCatFromClosed(object sender, FormClosedEventArgs e)
        {
            this.dataGridView1.Update();
            if (!Globals.visibility)
                this.dataGridView1.Visible = false;
        }
    }
}
public static class Globals
{
    public static List<List<string>> Excercises = new List<List<string>>();
    public static List<List<string>> doneExcercises = new List<List<string>>();
    public static DataTable DTable = new DataTable();
    public static BindingSource SBind = new BindingSource();
    public static DataGridView dataGridView1 = new DataGridView();
    public static bool visibility=false;
}