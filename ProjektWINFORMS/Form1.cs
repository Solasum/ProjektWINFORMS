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
using Microsoft.VisualBasic.FileIO;

public static class Globals
{
    public static List<List<string>> Excercises = new List<List<string>>();
    public static List<List<string>> doneExcercises = new List<List<string>>();
    public static DataTable DTable = new DataTable();
    public static BindingSource SBind = new BindingSource();
    public static DataGridView dataGridView1 = new DataGridView();
    public static bool visibility = false;
}
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
            if (Globals.Excercises.Count != 0)
            {
                var DodajAkt = new DodajAkt();
                DodajAkt.FormClosed += DodajAktFormClosed;
                DodajAkt.Show();
            }
            else
                MessageBox.Show("Brak kategorii");
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
            Read();
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.Excercises.Count != 0)
                Save();
            else
                MessageBox.Show("Brak elementów do zapisania");
        }

        private void wykresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.Excercises.Count != 0)
            {
                var wykres = new wykres();
                wykres.Show();
            }
            else
                MessageBox.Show("Brak kategorii");
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
        private void DodajAktFormClosed(object sender, FormClosedEventArgs e)
        {
            this.dataGridView1.Update();
        }


        private void Save()
        {


            var fileContent = string.Empty;
            var filePath = string.Empty;
            var fileName = string.Empty;
            var dir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            var ext = string.Empty;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = dir;
                saveFileDialog.Filter = "xml files (*.xml)|*.xml|csv files (*.csv)|*.csv";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.DefaultExt = "xml";
                saveFileDialog.AddExtension = true;


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                    fileName = saveFileDialog.FileName;
                    ext = Path.GetExtension(saveFileDialog.FileName);

                    if (ext.ToLower() == ".csv")
                    {
                        using (StreamWriter writer = File.CreateText(fileName))
                        {
                            int numberOfColumns = Globals.DTable.Columns.Count;
                            foreach (DataColumn column in Globals.DTable.Columns)
                            {
                                writer.Write(column.ColumnName);
                                writer.Write(";");
                            }
                            foreach (DataRow row in Globals.DTable.Rows)
                            {
                                writer.WriteLine();
                                for (int i = 0; i < numberOfColumns; i++)
                                {
                                    writer.Write(Convert.ToString(row[i]));
                                    if (i != numberOfColumns - 1)
                                        writer.Write(";");
                                }
                            }
                        }
                    }
                    else if (ext.ToLower() == ".xml")
                    {
                        DataSet dataSet = new DataSet();
                        dataSet.Tables.Add(Globals.DTable);
                        dataSet.WriteXml(fileName);
                      
                    }
                }
            }
        }

        private void Read()
        {
            if (Globals.Excercises.Count != 0)
            {
                MessageBox.Show("Baza cwiczen musi byc pusta!");
            }
            else
            {
                var fileContent = string.Empty;
                var filePath = string.Empty;
                var dir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                var extension = string.Empty;
                var file = string.Empty;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = dir;
                    openFileDialog.Filter = "xml files (*.xml)|*.xml"; //"xml files (*.xml)|*.xml|csv files (*.csv)|*.csv //xml files (*.xml)|*.xml
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;
                        filePath = openFileDialog.FileName;
                        file = openFileDialog.FileName;
                        extension = Path.GetExtension(openFileDialog.FileName);

                        if (extension.ToLower() == ".xml")
                        {
                            DataSet dataSet = new DataSet();
                           //dataSet.Tables.Add(Globals.DTable);
                            dataSet.ReadXml(file);
                            DataTable table = dataSet.Tables[0];
                            foreach (DataRow row in table.Rows)
                            {
                                foreach (DataColumn column in table.Columns)
                                {
                                    Console.WriteLine(row[column]);
                                }
                            }
                                this.dataGridView1.Columns.Clear();
                            Globals.DTable = table;
                            this.dataGridView1.DataSource = Globals.DTable;
                            this.dataGridView1.Update();
                            this.dataGridView1.Visible = true;

                            //foreach (DataGridViewRow row in dataGridView1.Rows)
                            String cat, excName;
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                String header = dataGridView1.Columns[i].HeaderText;
                                //Console.WriteLine(header);
                                
                                if (header == "Data")
                                    continue;
                                int index = header.IndexOf(":");
                                cat = header.Substring(0, index);
                                excName = header.Substring(index+2);
                                Globals.Excercises.Add(new List<string> { cat, excName });
                                //Console.WriteLine(excName);
                                
                            }
                            string sDate="";
                            string count = "";
                            for (int j =0; j< dataGridView1.RowCount; j++ )
                            {
                                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                {
                                    
                                    String header = dataGridView1.Columns[i].HeaderText;
                                    if(header == "Data")
                                        if (dataGridView1.Rows[j].Cells[i].Value != null)
                                            sDate = dataGridView1.Rows[j].Cells[i].Value.ToString();
                                    if (header != "Data")
                                    {
                                        int index = header.IndexOf(":");
                                        cat = header.Substring(0, index);
                                        excName = header.Substring(index);
                                        if (dataGridView1.Rows[j].Cells[i].Value!=null)
                                           count= dataGridView1.Rows[j].Cells[i].Value.ToString();
                                        
                                        Globals.doneExcercises.Add(new List<string> { sDate, excName, count });
                                    }
                                   

                                }
                            }
                            Globals.Excercises.Sort((a, b) => a[1].CompareTo(b[1]));
                        }
                        if(extension.ToLower() == ".csv")
                        {
                            DataSet dataSet = new DataSet();
                            //dataSet.Tables.Add(Globals.DTable);
                            DataTable table = new DataTable();




                            using (TextFieldParser csvParser = new TextFieldParser(file))
                            {
                                //csvParser.CommentTokens = new string[] { "#" };
                                csvParser.SetDelimiters(new string[] { ";" });
                                //csvParser.HasFieldsEnclosedInQuotes = true;
                                



                                string[] fields1 = csvParser.ReadFields();
                                foreach(string z in fields1)
                                    table.Columns.Add(new DataColumn(z, typeof(string)));


                                while (!csvParser.EndOfData)
                                {
                                    string[] fields = csvParser.ReadFields();
                                    DataRow dr = table.NewRow();
                                    int indx = 0;
                                    foreach (string z in fields)
                                    {
                                       
                                        dr[fields1[indx]] = z;

                                        indx++;
                                    }
                                    table.Rows.Add(dr);
                                    
                                    


                                }
                                dataSet.Tables.Add(table);
                            }





                            
                            foreach (DataRow row in table.Rows)
                            {
                                foreach (DataColumn column in table.Columns)
                                {
                                    Console.WriteLine(row[column]);
                                }
                            }
                            this.dataGridView1.Columns.Clear();
                            Globals.DTable = table;
                            this.dataGridView1.DataSource = Globals.DTable;
                            this.dataGridView1.Update();
                            this.dataGridView1.Visible = true;

                            //foreach (DataGridViewRow row in dataGridView1.Rows)
                            String cat, excName;
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                String header = dataGridView1.Columns[i].HeaderText;
                                //Console.WriteLine(header);

                                if (header == "Data")
                                    continue;
                                int index = header.IndexOf(":");
                                cat = header.Substring(0, index);
                                excName = header.Substring(index + 2);
                                Globals.Excercises.Add(new List<string> { cat, excName });
                                //Console.WriteLine(excName);

                            }
                            string sDate = "";
                            string count = "";
                            for (int j = 0; j < dataGridView1.RowCount; j++)
                            {
                                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                {

                                    String header = dataGridView1.Columns[i].HeaderText;
                                    if (header == "Data")
                                        if (dataGridView1.Rows[j].Cells[i].Value != null)
                                            sDate = dataGridView1.Rows[j].Cells[i].Value.ToString();
                                    if (header != "Data")
                                    {
                                        int index = header.IndexOf(":");
                                        cat = header.Substring(0, index);
                                        excName = header.Substring(index);
                                        if (dataGridView1.Rows[j].Cells[i].Value != null)
                                            count = dataGridView1.Rows[j].Cells[i].Value.ToString();

                                        Globals.doneExcercises.Add(new List<string> { sDate, excName, count });
                                    }


                                }
                            }
                            Globals.Excercises.Sort((a, b) => a[1].CompareTo(b[1]));
                        }

                    }
                }
                
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
