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
    public partial class stworz : Form
    {
        public stworz()
        {
            InitializeComponent();
        }

        private void stworz_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newCat = textBox1.Text;
            string excName = richTextBox1.Text;
            
            if (newCat.Length==0 || excName.Length==0)
            {
                MessageBox.Show("Nazwa kategorii i/lub rodzaje ćwiczeń nie mogą być puste");
            }
            else
            {
                if (excName.Contains(';'))
                {
                    List<string> exclList = excName.Split(";".ToCharArray()).ToList();
                    exclList.Insert(0, newCat);
                    Globals.Excercises.Add(exclList);
                    if (!Globals.DTable.Columns.Contains("Data"))
                    {
                        Globals.DTable.Columns.Add("Data", typeof(string));
                    };
                    int count = 0;
                    var categ = string.Empty;
                    foreach (string ex in exclList)
                    {
                        if (count == 0)
                        {
                            count += 1;
                        }
                        else
                        {
                            if (!Globals.DTable.Columns.Contains(newCat + ": " + ex))
                            {
                                Globals.DTable.Columns.Add(newCat + ": " + ex, typeof(string));
                            }
                        }
                    }
                }
                else
                {
                    if (!Globals.DTable.Columns.Contains("Data"))
                    {
                        Globals.DTable.Columns.Add("Data", typeof(string));
                    }
                    if (!Globals.DTable.Columns.Contains(newCat + ": " + excName))
                    {
                        Globals.DTable.Columns.Add(newCat + ": " + excName, typeof(string));
                    }
                    Globals.Excercises.Add(new List<string> {newCat, excName});
                }
                if (Globals.Excercises.Count > 0)
                    Globals.visibility = true;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
    }
}
