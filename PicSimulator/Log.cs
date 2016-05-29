using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicSimulator
{
    class Log : TextWriter
    {
        TextBox output = null;

        public Log(TextBox output)
        {
            this.output = output;
        }

        public override void Write(string value)
        {
            String output = value + "\n";
            base.Write(output);
            this.output.AppendText(output);
        }

        public override void Write(char value)
        {
            base.Write(value);
        }

        public override Encoding Encoding
        {
            get
            {
                return System.Text.Encoding.UTF8;
            }
        }
    }
}
