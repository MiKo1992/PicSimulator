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

        private Main main;

        public Form1()
        {
            InitializeComponent();
            main = new Main(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void printFile(ArrayList file)
        {
            dataGridView1.Columns.Add("Spalte1", "Code");

            foreach (String row in file) {
            dataGridView1.Rows.Add(row);
            }

            dataGridView1.AutoResizeColumn(0);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            main.readFile();
        }
    }
}
