using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class Instruction
    {

        InstructionType command;
        int line;
        String argument1;
        String argument2;

        /// <summary>
        /// Creates a new Instruction object with no arguments
        /// </summary>
        /// <param name="command">The InstructionType</param>
        /// <param name="line">The line of code the commnand appeared</param>
        public Instruction(InstructionType command, int line)
        {
            this.command = command;
            this.line = line;
        }

        /// <summary>
        /// Creates a new Instruction with one arguments
        /// </summary>
        /// <param name="command">The InstructionType</param>
        /// <param name="line">The line of code the commnand appeared</param>
        /// <param name="argument1">First argument</param>
        public Instruction(InstructionType command, int line, String argument1)
        {
            this.command = command;
            this.line = line;
            this.argument1 = argument1;
        }

        /// <summary>
        /// Creates a new instruction object with two arguments
        /// </summary>
        /// <param name="command">The InstructionType</param>
        /// <param name="line">The line of code the commnand appeared</param>
        /// <param name="argument1">First argument</param>
        /// <param name="argument2">Second argument</param>
        public Instruction(InstructionType command, int line,String argument1, String argument2)
        {
            this.command = command;
            this.line = line;
            this.argument1 = argument1;
            this.argument2 = argument2;
        }

        public int getLine()
        {
            return line;
        }

        /// <summary>
        /// Returns the command as a String
        /// </summary>
        /// <returns>The command as String</returns>
        public override String ToString()
        {
            return command.ToString();
        }

        /// <summary>
        /// Returns the command
        /// </summary>
        /// <returns>The command as InstructionType</returns>
        public InstructionType getCommand()
        {
            return command;
        }
    }
}
