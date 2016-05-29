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
                    Instructions.Add(ParseCommand(splitLine[1], int.Parse(splitLine[0], System.Globalization.NumberStyles.HexNumber)));

                }
            }
            return Instructions;
        }

        private Instruction ParseCommand(String command, int codeLine)
        {
            int value = int.Parse(command, System.Globalization.NumberStyles.HexNumber);

            if ((value & 0x3F9F) == 0x0000)
            {
                return new Instruction(InstructionType.NOP, codeLine);
            }
            else if ((value & 0x3FFF) == 0x0064)
            {
                return new Instruction(InstructionType.CLRWDT, codeLine);
            }
            else if ((value & 0x3FFF) == 0x0009)
            {
                return new Instruction(InstructionType.RETFIE, codeLine);
            }
            else if ((value & 0x3FFF) == 0x0008)
            {
                return new Instruction(InstructionType.RETURN, codeLine);
            }
            else if ((value & 0x3FFF) == 0x0063)
            {
                return new Instruction(InstructionType.SLEEP, codeLine);
            }
            else if ((value & 0x3000) == 0x0000)
            {
                return ByteInstruction(value, codeLine);
            }
            else if ((value & 0x3000) == 0x1000)
            {
                return BitInstruction(value, codeLine);
            }
            else if ((value & 0x3000) >= 0x2000)
            {
                return LcInstruction(value, codeLine);
            }
            else
            {
                throw new Exception("Unknown command");
            }
        }

        private Instruction ByteInstruction(int hexValue, int codeLine)
        {
            InstructionType type = new InstructionType();

            if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3F80))
            {
                type = (InstructionType)(hexValue & 0x3F80);
                if (type == InstructionType.CLRF)
                {
                    return new Instruction(type, hexValue & 0x007F);
                }
            }

            // Check for byte-oriented operations
            else if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3F00))
            {
                type = (InstructionType)(hexValue & 0x3F00);

                if ((hexValue & 0x0080) > 0)
                {
                    return new Instruction(type, codeLine, 1, hexValue & 0x007F);
                }
                else
                {
                    return new Instruction(type, codeLine, 0, hexValue & 0x007F);
                }

            }
            else
            {
                throw new Exception("Unknown command");
            }

            if (type == InstructionType.MOVWF)
            {
                return new Instruction(type, codeLine, hexValue & 0x007F);
            }
            return new Instruction(type, codeLine);
        }

        private Instruction BitInstruction(int hexValue, int codeLine)
        {
            InstructionType type = new InstructionType();
            // Check for bit-oriented operations
            if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3C00))
            {
                type = (InstructionType)(hexValue & 0x3C00);
            }
            else
            {
                throw new Exception("Unknown command");
            }

            int argument1 = Convert.ToInt32(Convert.ToString(hexValue, 2).Substring(6, 3), 2);
            return new Instruction(type, codeLine, argument1, hexValue & 0x007F);
        }

        private Instruction LcInstruction(int hexValue, int codeLine)
        {
            InstructionType type = new InstructionType();

            // Check for CALL
            if ((hexValue & 0x3800) == 0x2000)
            {
                type = InstructionType.CALL;
                return new Instruction(type, hexValue & 0x07FF);
            }
            else if ((hexValue & 0x3800) == 0x2800)
            {
                type = InstructionType.GOTO;
                return new Instruction(type, hexValue & 0x07FF);
            }
            else if ((hexValue & 0x3C00) == 0x3400)
            {
                type = InstructionType.RETLW;

            }
            else if ((hexValue & 0x3C00) == 0x3000)
            {
                type = InstructionType.MOVLW;

            }
            else if ((hexValue & 0x3E00) == 0x3E00)
            {
                type = InstructionType.ADDLW;

            }
            else if ((hexValue & 0x3E00) == 0x3C00)
            {
                type = InstructionType.SUBLW;

            }
            else
            {
                if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3F00))
                {
                    type = (InstructionType)(hexValue & 0x3F00);
                }
                else
                {
                    throw new Exception("Unknown command");
                }
            }

            return new Instruction(type, hexValue & 0x00FF);
        }
    }
}
