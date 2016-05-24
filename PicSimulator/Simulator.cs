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
    class Simulator
    {
        private Form1 userInterface;
        private List<String> fileContent = new List<String>();
        private List<Instruction> Instructions;

        /// <summary>
        /// Creates a main object with reference to the Windows Form
        /// </summary>
        /// <param name="userInterface"></param>
        public Simulator(Form1 userInterface)
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
            Instructions = new CodeInterpreter().getInstructions(fileContent);
            printLog(Instructions);
        }

        private void printLog(List<Instruction> commands)
        {
            foreach(Instruction instruction in commands)
            {
                userInterface.printLog("saved " + instruction.ToString());
            }
        }

        /// <summary>
        /// Return's the fileContent List
        /// </summary>
        /// <returns></returns>
        public List<String> getFileContent()
        {
            return fileContent;
        }

        public Form1 getUserInterface()
        {
            return userInterface;
        }

    }
}
