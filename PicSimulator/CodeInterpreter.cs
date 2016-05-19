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

        public ArrayList findCommands(ArrayList fileContent)
        {

            ArrayList commands = new ArrayList();

            foreach(String content in fileContent)
            {
                if(Char.IsNumber(content[0]))
                {
                    String[] command = new String[2];
                    command[0] = content.Substring(5, 2);
                    command[1] = content.Substring(7, 2);
                    commands.Add(command);
                    //Console.WriteLine(command[0] + command[1]);
                }
            }
            return commands;
        }
    }
}
