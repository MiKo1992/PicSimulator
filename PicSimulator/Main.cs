using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class Main
    {
        private Form1 userInterface;
        private ArrayList fileContent = new ArrayList();
        private ArrayList commands;

        public Main(Form1 userInterface)
        {
            this.userInterface = userInterface;
        }

        public void readFile()
        {
            fileContent = new FileReader().readFile();
            userInterface.printFile(fileContent);
            commands = new CodeInterpreter().findCommands(fileContent);
        }

        public ArrayList getFileContent()
        {
            return fileContent;
        }
    }
}
