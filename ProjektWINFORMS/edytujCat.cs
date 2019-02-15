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
    public partial class edytujCat : Form
    {
        public edytujCat()
        {
            InitializeComponent();
            foreach (var sublist in Globals.Excercises)
                comboBox1.Items.Add(sublist[0]);
            foreach (DataColumn column in Globals.DTable.Columns)
                if (column.ColumnName != "Data")
                    comboBox1.Items.Add(column.ColumnName);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selCat = comboBox1.Text;
            string newName = textBox1.Text;

            if (selCat.Length == 0||newName.Length==0)
            {
                MessageBox.Show("Musisz wybrać kategorię i podać jej nową nazwę");
            }
   
            if (selCat.Length > 0 && newName.Length > 0)
            {
                for (var i = 0; i < Globals.Excercises.Count; i++)
                    for (var j = 0; j < Globals.Excercises[i].Count; j++)
                        if (Globals.Excercises[i][j] == selCat)
                            Globals.Excercises[i][j] = newName;

                foreach (DataColumn column in Globals.DTable.Columns)
                    if (column.ColumnName.Contains(selCat))
                    {
                        var newColName = column.ColumnName.Replace(selCat, newName);
                        Globals.DTable.Columns[column.ColumnName].ColumnName = newColName;
                    }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedCategory = comboBox1.Text;

            if (selectedCategory.Length == 0)
                MessageBox.Show("Wybierz co chcesz usunąć");
            if (selectedCategory.Length > 0)
            {
                for (var i = 0; i < Globals.Excercises.Count; i++)
                {
                    for (var j = 0; j < Globals.Excercises[i].Count; j++)
                        if (Globals.Excercises[i][j] == selectedCategory && selectedCategory.Contains(':'))
                        {
                            Globals.Excercises[i].RemoveAt(j);
                            break;
                        }
                    if (Globals.Excercises[i][0] == selectedCategory && !selectedCategory.Contains(':'))
                    {
                        Globals.Excercises.RemoveAt(i);
                        break;
                    }
                    
                }

                List<int> indexList = new List<int>();
                if (!selectedCategory.Contains(':'))
                {
                    int c = 0;
                    foreach (DataColumn column in Globals.DTable.Columns)
                    {
                        if (column.ColumnName.Contains(selectedCategory + ':'))
                            indexList.Add(c);
                        c += 1;
                    }
                }
                int first = 0;
                foreach (int index in indexList)
                {
                        if (first == 0)
                        {
                            Globals.DTable.Columns.RemoveAt(index);
                            first += 1;
                        }
                        else
                        {
                            Globals.DTable.Columns.RemoveAt(index - first);
                            first += 1;

                        }
                }
                MessageBox.Show("Kategoria usunięta");
                Globals.visibility = false;
                this.Close();

                var counter = 0;
                foreach (DataColumn column in Globals.DTable.Columns)
                {
                    if (column.ColumnName == selectedCategory)
                    {
                        if (column.ColumnName.Contains(':'))
                        {
                            Globals.DTable.Columns.Remove(column);
                            Globals.DTable.Columns.RemoveAt(counter);
                            break;
                        }

                        MessageBox.Show("Usunieto!");
                        this.Close();
                        break;

                    }
                    counter += 1;
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
