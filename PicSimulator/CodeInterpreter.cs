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

        private List<Instruction> Instructions = new List<Instruction>();

        public CodeInterpreter()
        {
        }

        /// <summary>
        /// Get a list with the instructions in a String list
        /// </summary>
        /// <param name="fileContent">List of Strings</param>
        /// <returns name= "Instructions">List of Instructions objects</returns>
        public List<Instruction> getInstructions(List<String> fileContent)
        {

            List<Instruction> Instructions = new List<Instruction>();

            foreach(String content in fileContent)
            {
                if(Char.IsNumber(content[0]))
                {
                    String[] splitLine = content.Split(' ');
                    //TODO Save arguments in Instructions
                    Instructions.Add(new Instruction(ParseCommand(splitLine[1]), int.Parse(splitLine[0], System.Globalization.NumberStyles.HexNumber)));

                }
            }
            return Instructions;
        }

        private InstructionType ParseCommand(String command)
        {
            int value = int.Parse(command, System.Globalization.NumberStyles.HexNumber);

            if ((value & 0x3F9F) == 0x0000)
            {
                return InstructionType.NOP;
            }
            else if ((value & 0x3FFF) == 0x0064)
            {
                return InstructionType.CLRWDT;
            }
            else if ((value & 0x3FFF) == 0x0009)
            {
                return InstructionType.RETFIE;
            }
            else if ((value & 0x3FFF) == 0x0008)
            {
                return InstructionType.RETURN;
            }
            else if ((value & 0x3FFF) == 0x0063)
            {
                return InstructionType.SLEEP;
            }
            else if ((value & 0x3000) == 0x0000)
            {
                return ByteInstruction(value);
            }
            else if ((value & 0x3000) == 0x1000)
            {
                return BitInstruction(value);
            }
            else if ((value & 0x3000) >= 0x2000)
            {
                return LcInstruction(value);
            }
            else
            {
                throw new Exception("Unknown command");
            }
        }

        private InstructionType ByteInstruction(int value)
        {
            int hexValue = value;

            InstructionType type = new InstructionType();

            if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3F80))
            {
                type = (InstructionType)(hexValue & 0x3F80);
            }

            else if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3F00))
            {
                type = (InstructionType)(hexValue & 0x3F00);
            }
            else
            {
                throw new Exception("Unknown command"); 
            }
            return type;
        }

        private InstructionType BitInstruction(int value)
        {
            InstructionType type;
            int hexValue = value;

            if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3C00))
            {
                type = (InstructionType)(hexValue & 0x3C00);
                return type;
            }
            else
            {
                throw new Exception("Unknown command");
            }
        }

        private static InstructionType LcInstruction(int value)
        {
            int hexValue = value;

            if ((hexValue & 0x3800) == 0x2000)
            {
                return InstructionType.CALL;
            }
            else if ((hexValue & 0x3800) == 0x2800)
            {
                return InstructionType.GOTO;
            }
            else if ((hexValue & 0x3C00) == 0x3400)
            {
                return InstructionType.RETLW;
            }
            else if ((hexValue & 0x3C00) == 0x3000)
            {
                return InstructionType.MOVLW;
            }
            else if ((hexValue & 0x3E00) == 0x3E00)
            {
                return InstructionType.ADDLW;
            }
            else if ((hexValue & 0x3E00) == 0x3C00)
            {
                return InstructionType.SUBLW;
            }
            else
            {
                if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3F00))
                {
                    return (InstructionType)(hexValue & 0x3F00);
                }
                else
                {
                    throw new Exception("Unknown command");
                }
            }
        }
    }
}
