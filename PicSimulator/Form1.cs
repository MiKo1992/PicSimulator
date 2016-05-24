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

        private Simulator main;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Print a list in dataGridView1
        /// </summary>
        /// <param name="file"></param>
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
            reset();
            main.start();
        }

        /// <summary>
        /// Print a new line in the ConsoleLog
        /// </summary>
        /// <param name="log"></param>
        public void printLog(String log)
        {
            consoleLog.AppendText("saved " + log);
            consoleLog.AppendText("\n");
        }

        private void reset()
        {
            dataGridView1.Rows.Clear();
            consoleLog.Clear();
            main = new Simulator(this);
        }
    }
}
