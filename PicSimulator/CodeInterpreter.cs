using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class CodeInterpreter
    {

        ArrayList commands = new ArrayList();

        public CodeInterpreter()
        {
        }

        /// <summary>
        /// Saves the hex values and the name of the commands
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public ArrayList findCommands(ArrayList fileContent)
        {

            ArrayList commands = new ArrayList();

            foreach(String content in fileContent)
            {
                if(Char.IsNumber(content[0]))
                {
                    String[] command = new String[3];
                    command[0] = content.Substring(5, 2);
                    command[1] = content.Substring(7, 2);
                    command[2] = content.Split(' ')[23];
                    commands.Add(command);
                    Console.WriteLine(command[0] + command[1] + " " + command[2]);

                }
            }
            return commands;
        }
    }
}
