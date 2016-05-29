using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicSimulator
{
    public partial class Form1 : Form
    {

        private Simulator simu;
        private Log log;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            log = new Log(consoleLog);
            Console.SetOut(log);
        }

        /// <summary>
        /// Print a list in dataGridView1
        /// </summary>
        /// <param name="file"></param>
        public void printFile(List<String> file)
        {
            foreach (String row in file) {
            dataGridView1.Rows.Add(row);
            }

            dataGridView1.AutoResizeColumn(0);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reset();
            simu.start();
            setDataBindings();
        }

        private void setDataBindings()
        {
            wLabel.DataBindings.Clear();
            pcLabel.DataBindings.Clear();
            cLabel.DataBindings.Clear();
            dcLabel.DataBindings.Clear();
            zLabel.DataBindings.Clear();
            pdLabel.DataBindings.Clear();
            toLabel.DataBindings.Clear();
            rp0Label.DataBindings.Clear();

            wLabel.DataBindings.Add("Text", simu, "wRegister");
            pcLabel.DataBindings.Add("Text", simu, "ProgramCounter");
            cLabel.DataBindings.Add("Text", simu, "CBit");
            dcLabel.DataBindings.Add("Text", simu, "DCBit");
            zLabel.DataBindings.Add("Text", simu, "ZBit");
            pdLabel.DataBindings.Add("Text", simu, "PDBit");
            toLabel.DataBindings.Add("Text", simu, "TOBit");
            rp0Label.DataBindings.Add("Text", simu, "RP0Bit");
        }

        public void UpdateStorageSet()
        {
            int[] storage = simu.getRegisterControl().getRegister();
            Console.Write("[+] Updating Storage\n");
            for (int i = 0; i < storage.Length / 8; i++)
            {
                storageGridView.Rows[i].Cells[0].Value = storage[8 * i + 0].ToString("X2");
                storageGridView.Rows[i].Cells[1].Value = storage[8 * i + 1].ToString("X2");
                storageGridView.Rows[i].Cells[2].Value = storage[8 * i + 2].ToString("X2");
                storageGridView.Rows[i].Cells[3].Value = storage[8 * i + 3].ToString("X2");
                storageGridView.Rows[i].Cells[4].Value = storage[8 * i + 4].ToString("X2");
                storageGridView.Rows[i].Cells[5].Value = storage[8 * i + 5].ToString("X2");
                storageGridView.Rows[i].Cells[6].Value = storage[8 * i + 6].ToString("X2");
                storageGridView.Rows[i].Cells[7].Value = storage[8 * i + 7].ToString("X2");
            }
        }

        public void UpdateStack(Stack<int> stack)
        {
            // Clear stack
            stackGridView.Rows.Clear();
            int[] stackarr = stack.ToArray();
            for (int i = stackarr.Length; i > 0; i--)
            {
                int n = stackGridView.Rows.Add();
                stackGridView.Rows[n].Cells[0].Value = stackarr[i - 1].ToString("0000");
            }
        }

        public void HighlightLine(int lineNumber)
        {
            //dataGridView1.Rows[lineNumber].Selected = true;
        }

        public void AddStorageSet(int[] storage)
        {
            //storageGridView.RowHeadersWidth = 30;
            Console.Write("[+] Initializing Storage\n");
            for (int i = 0; i < (storage.Length) / 8; i++)
            {

                int n = storageGridView.Rows.Add();

                storageGridView.Rows[n].Cells[0].Value = storage[8 * i + 0].ToString("X2");
                storageGridView.Rows[n].Cells[1].Value = storage[8 * i + 1].ToString("X2");
                storageGridView.Rows[n].Cells[2].Value = storage[8 * i + 2].ToString("X2");
                storageGridView.Rows[n].Cells[3].Value = storage[8 * i + 3].ToString("X2");
                storageGridView.Rows[n].Cells[4].Value = storage[8 * i + 4].ToString("X2");
                storageGridView.Rows[n].Cells[5].Value = storage[8 * i + 5].ToString("X2");
                storageGridView.Rows[n].Cells[6].Value = storage[8 * i + 6].ToString("X2");
                storageGridView.Rows[n].Cells[7].Value = storage[8 * i + 7].ToString("X2");
            }
            int counter = 0;
            for (int i = 0; i <= 31; i++)
            {
                storageGridView.Rows[i].HeaderCell.Value = counter.ToString("X2");
                counter += 8;
            }
        }

        private void reset()
        {
            dataGridView1.Rows.Clear();
            consoleLog.Clear();
            simu = new Simulator(this);
        }

        public void ResetStorageSet()
        {
            storageGridView.Rows.Clear();
            storageGridView.Refresh();
        }

        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\hilfe.pdf");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cLabel_Click(object sender, EventArgs e)
        {

        }

        private void dcLabel_Click(object sender, EventArgs e)
        {

        }

        private void zLabel_Click(object sender, EventArgs e)
        {

        }

        private void pdLabel_Click(object sender, EventArgs e)
        {

        }

        private void toLabel_Click(object sender, EventArgs e)
        {

        }

        private void rp0Label_Click(object sender, EventArgs e)
        {

        }

        private void pcLabel_Click(object sender, EventArgs e)
        {

        }

        private void wLabel_Click(object sender, EventArgs e)
        {

        }

        private void btn_nextStep_Click(object sender, EventArgs e)
        {
            simu.NextStep();
        }
    }
}
