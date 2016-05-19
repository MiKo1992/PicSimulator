using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicSimulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            printFile(readFile());
        }

        private void printFile(ArrayList file)
        {
            dataGridView1.Columns.Add("Spalte1", "Inhalt");

            foreach (String row in file) {
            dataGridView1.Rows.Add(row);
            }
        }

        private ArrayList readFile()
        {
            String sLine = "";
            ArrayList arrText = new ArrayList();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                System.IO.StreamReader(openFileDialog1.FileName);

                while (sLine != null)
                {
                    sLine = sr.ReadLine();
                    if (sLine != null)
                        arrText.Add(sLine);
                }
                sr.Close();
            }

            return arrText;
        }
    }
}
