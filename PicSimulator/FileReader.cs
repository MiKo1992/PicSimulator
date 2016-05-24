using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicSimulator
{
    class FileReader
    {

        public FileReader() { }

        public List<String> readFile()
        {
            String sLine = "";
            List<String> fileContent = new List<String>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ".lst files|*.lst";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                System.IO.StreamReader(openFileDialog.FileName);

                while (sLine != null)
                {
                    sLine = sr.ReadLine();
                    if (sLine != null)
                        fileContent.Add(sLine);
                }
                sr.Close();
            }

            return fileContent;
        }

    }
}
