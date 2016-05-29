using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class InstructionSet
    {
        private List<Instruction> instructions = new List<Instruction>();
        private List<String> lines = new List<String>();

        public void AddInstruction(Instruction instruction)
        {
            // TODO try-catch Exceptions
            if (instruction == null)
            {
                Console.Write("Instruction NULL!");
            }
            else
            {
                Console.Write("Saving " + instruction.ToString() + '\n');
                instructions.Add(instruction);
            }
        }

        public List<Instruction> GetList()
        {
            return instructions;
        }

        public Instruction GetFirstInstruction()
        {
            return instructions[0];
        }

        public Instruction GetInstruction(int index)
        {
            return instructions[index];
        }

        public void Execute(int programCounter, Simulator Sim)
        {
            instructions[programCounter].Execute(Sim);
        }

        public void setInstructionSet(List<Instruction> instructions)
        {
            this.instructions = instructions;
        }
    }
}
