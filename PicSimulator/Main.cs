using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    /// <summary>
    /// The main class for the PicSimulator
    /// </summary>
    class Main
    {
        private Form1 userInterface;
        private ArrayList fileContent = new ArrayList();
        private ArrayList commands;

        /// <summary>
        /// Creates a main object with reference to the Windows Form
        /// </summary>
        /// <param name="userInterface"></param>
        public Main(Form1 userInterface)
        {
            this.userInterface = userInterface;
        }

        /// <summary>
        /// Opens a file reader and starts the PicSimulator
        /// </summary>
        public void start()
        {
            fileContent = new FileReader().readFile();
            userInterface.printFile(fileContent);
            commands = new CodeInterpreter().findCommands(fileContent);
            printLog(commands);

        }

        private void printLog(ArrayList commands)
        {
            foreach(String[] command in commands)
            {
                userInterface.printLog(command[2]);
            }
        }

        /// <summary>
        /// Return's the fileContent List
        /// </summary>
        /// <returns></returns>
        public ArrayList getFileContent()
        {
            return fileContent;
        }
    }
}
