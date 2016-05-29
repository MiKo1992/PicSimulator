using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class Instruction
    {

        InstructionType command;
        int line;
        int argument1 = -1;
        int argument2 = -1;
        int numberOfArguments = -1;

        /// <summary>
        /// Creates a new Instruction object with no arguments
        /// </summary>
        /// <param name="command">The InstructionType</param>
        /// <param name="line">The line of code the commnand appeared</param>
        public Instruction(InstructionType command, int line)
        {
            this.command = command;
            this.line = line;
            Console.Write("saved " + command.ToString());
        }

        /// <summary>
        /// Creates a new Instruction with one arguments
        /// </summary>
        /// <param name="command">The InstructionType</param>
        /// <param name="line">The line of code the commnand appeared</param>
        /// <param name="argument1">First argument</param>
        public Instruction(InstructionType command, int line, int argument1)
        {
            this.command = command;
            this.line = line;
            this.argument1 = argument1;
            Console.Write("saved " + command.ToString() + " Argument 1: " + argument1);
        }

        /// <summary>
        /// Creates a new instruction object with two arguments
        /// </summary>
        /// <param name="command">The InstructionType</param>
        /// <param name="line">The line of code the commnand appeared</param>
        /// <param name="argument1">First argument</param>
        /// <param name="argument2">Second argument</param>
        public Instruction(InstructionType command, int line,int argument1, int argument2)
        {
            this.command = command;
            this.line = line;
            this.argument1 = argument1;
            this.argument2 = argument2;
            Console.Write("saved " + command.ToString() + " Argument 1: " + argument1 + " Argument 2: " + argument2);
        }

        public bool Execute(Simulator picSim)
        {
            // Reset Carry-Bits etc.
            ResetBits(picSim);
            // Get the type of this object (=Instruction)
            this.GetType().InvokeMember(
                command.ToString(),
                BindingFlags.InvokeMethod,
                null,
                this,
                new object[] { picSim });

            Program.mainForm.UpdateStorageSet();

            return true;
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

        // --------------- OPS FUNCTIONS ---------------

        public int ADDWF(Simulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int fRegister = picSim.getRegister(GetIndirectAddress(picSim, argument2));
            int result = (wRegister + fRegister) & 0xFF;
            CheckCBit(picSim, InstructionType.ADDWF, wRegister, fRegister);
            CheckDCBit(picSim, InstructionType.ADDWF, wRegister, fRegister);
            CheckZBit(picSim, result);
            if (argument1 == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), result);
            }
            return 1;
        }

        public int ANDWF(Simulator picSim)
        {
            int result = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber) & picSim.getRegister(GetIndirectAddress(picSim, argument2));
            CheckZBit(picSim, result);
            if (argument1 == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), result);
            }
            return 1;
        }

        public int CLRF(Simulator picSim)
        {
            picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument1), 0x00);
            if (((Int32.Parse(picSim.RP0Bit) << 8) == 0) && (argument1 == (int)RegisterType.TMR0))
            {
                // First 2 instructions don't count
                picSim.Timer = 0;
                picSim.SetDelay(3);
            }
            picSim.ZBit = "1";
            return 1;
        }

        public int CLRW(Simulator picSim)
        {
            picSim.WRegister = 0.ToString("X2");
            picSim.ZBit = "1";
            return 1;
        }

        public int COMF(Simulator picSim)
        {
            int result = ~picSim.getRegister(GetIndirectAddress(picSim, argument2));
            CheckZBit(picSim, result);
            if (argument1 == 0)
            {
                picSim.WRegister = (result & 0xFF).ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), result & 0xFF);
            }
            return 1;
        }

        public int DECF(Simulator picSim)
        {
            int result = (picSim.getRegister(GetIndirectAddress(picSim, argument2)) - 1) & 0xFF;
            CheckZBit(picSim, result);
            if (argument1 == 0)
            {
                picSim.WRegister = (result & 0xFF).ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), (result & 0xFF));
            }
            return 1;
        }

        public int DECFSZ(Simulator picSim)
        {
            int cycles = 1;
            int result = (picSim.getRegister(GetIndirectAddress(picSim, argument2)) - 1) & 0xFF;
            if (result == 0)
            {
                // skip next execution if 0
                picSim.ProgramCounter = (Int32.Parse(picSim.ProgramCounter) + 1).ToString("0000");
                cycles = 2;
            }
            if (argument1 == 0)
            {
                picSim.WRegister = (result & 0xFF).ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), (result & 0xFF));
            }
            return cycles;
        }

        public int INCF(Simulator picSim)
        {
            int result = (picSim.getRegister(GetIndirectAddress(picSim, argument2)) + 1) & 0xFF;
            CheckZBit(picSim, result);
            if (argument1 == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), result);
            }
            return 1;
        }

        public int INCFSZ(Simulator picSim)
        {
            int cycles = 1;
            int result = (picSim.getRegister(GetIndirectAddress(picSim, argument2)) + 1) & 0xFF;
            if (result == 0)
            {
                // skip next execution if 0
                picSim.ProgramCounter = (Int32.Parse(picSim.ProgramCounter) + 1).ToString("0000");
                cycles++;
            }
            if (argument1 == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), result);
            }
            return cycles;
        }

        public int IORWF(Simulator picSim)
        {
            int result = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber) | picSim.getRegister(GetIndirectAddress(picSim, argument2));
            CheckZBit(picSim, result);
            if (argument1 == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), result);
            }
            return 1;
        }

        public int MOVF(Simulator picSim)
        {
            int result = picSim.getRegister(GetIndirectAddress(picSim, argument2));
            CheckZBit(picSim, result);
            if (argument1 == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), result);
            }
            return 1;
        }

        public int MOVWF(Simulator picSim)
        {
            picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument1), Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber));
            if (((Int32.Parse(picSim.RP0Bit) << 8) == 0) && (argument1 == (int)RegisterType.TMR0))
            {
                // First 2 instructions don't count
                picSim.Timer = 0;
                picSim.SetDelay(3);
            }
            return 1;
        }

        public int NOP(Simulator picSim)
        {
            // Do Nothing
            return 1;
        }

        public int RLF(Simulator picSim)
        {
            int cFlag = Int32.Parse(picSim.CBit);
            int fRegister = picSim.getRegister(GetIndirectAddress(picSim, argument2));
            fRegister <<= 1;
            if ((fRegister & 0x100) > 0)
            {
                picSim.CBit = "1";
            }
            else if ((fRegister & 0x100) == 0)
            {
                picSim.CBit = "0";
            }
            if (cFlag == 1)
            {
                fRegister ^= 0x01;
            }
            else if (cFlag == 0)
            {
                // nothing to do here
            }
            int result = fRegister & 0xFF;
            if (argument1 == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), result);
            }
            return 1;
        }

        public int RRF(Simulator picSim)
        {
            int cFlag = Int32.Parse(picSim.CBit);
            int fRegister = picSim.getRegister(GetIndirectAddress(picSim, argument2));
            if ((fRegister & 0x1) > 0)
            {
                picSim.CBit = "1";
            }
            else if ((fRegister & 0x1) == 0)
            {
                picSim.CBit = "0";
            }
            fRegister >>= 1;
            if (cFlag == 1)
            {
                fRegister ^= 0x80;
            }
            else if (cFlag == 0)
            {
                fRegister &= 0x07F;
            }
            int result = fRegister & 0xFF;
            if (argument1 == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), result);
            }
            return 1;
        }

        public int SUBWF(Simulator picSim)
        {
            int fRegister = picSim.getRegister(GetIndirectAddress(picSim, argument2));
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (fRegister - wRegister) & 0xFF;
            CheckCBit(picSim, InstructionType.SUBWF, wRegister, fRegister);
            CheckDCBit(picSim, InstructionType.SUBWF, wRegister, fRegister);
            CheckZBit(picSim, result);
            if (argument1 == 0)
            {
                picSim.WRegister = (result & 0xFF).ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), (result & 0xFF));
            }
            return 1;
        }

        public int SWAPF(Simulator picSim)
        {
            int registerF = picSim.getRegister(GetIndirectAddress(picSim, argument2));
            int upperNipples = registerF & 0xF0;
            int lowerNipples = registerF & 0x0F;
            lowerNipples <<= 4;
            upperNipples >>= 4;
            int result = lowerNipples + upperNipples;
            if (argument1 == 0)
            {
                picSim.WRegister = (result & 0xFF).ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), (result & 0xFF));
            }
            return 1;
        }

        public int XORWF(Simulator picSim)
        {
            int result = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber) ^ picSim.getRegister(GetIndirectAddress(picSim, argument2));
            CheckZBit(picSim, result);
            if (argument1 == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (argument1 == 1)
            {
                picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), result);
            }
            return 1;
        }

        public int BCF(Simulator picSim)
        {
            int fRegister = picSim.getRegister(GetIndirectAddress(picSim, argument2));
            picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), fRegister & ~(0x01 << argument1));
            return 1;
        }

        public int BSF(Simulator picSim)
        {
            int fRegister = picSim.getRegister(GetIndirectAddress(picSim, argument2));
            picSim.getRegisterControl().SetRegisterAtAddress(GetIndirectAddress(picSim, argument2), fRegister ^ (0x01 << argument1));
            return 1;
        }

        public int BTFSC(Simulator picSim)
        {
            int fRegister = picSim.getRegister(GetIndirectAddress(picSim, argument2));
            int result = fRegister & (0x01 << argument1);
            if (result > 0)
            {
                return 1;
            }
            else if (result == 0)
            {
                // skip next execution if 0
                picSim.ProgramCounter = (Int32.Parse(picSim.ProgramCounter) + 1).ToString("0000");
                return 2;
            }
            return 1;
        }

        public int BTFSS(Simulator picSim)
        {
            int fRegister = picSim.getRegister(GetIndirectAddress(picSim, argument2));
            int result = fRegister & (0x01 << argument1);
            if (result > 0)
            {
                // skip next execution if 1
                picSim.ProgramCounter = (Int32.Parse(picSim.ProgramCounter) + 1).ToString("0000");
                return 2;
            }
            else if (result == 0)
            {
                return 1;
            }
            return 1;
        }

        public int ADDLW(Simulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (wRegister + argument1) & 0xFF;
            CheckZBit(picSim, result);
            CheckCBit(picSim, InstructionType.ADDLW, wRegister, argument1);
            CheckDCBit(picSim, InstructionType.ADDLW, wRegister, argument1);
            picSim.WRegister = result.ToString("X2");
            CheckZBit(picSim, result);
            return 1;
        }

        public int ANDLW(Simulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (wRegister & argument1);
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return 1;
        }

        public int CALL(Simulator picSim)
        {
            picSim.Stack = Int32.Parse(picSim.ProgramCounter) + 1;
            picSim.ProgramCounter = (argument1 - 1).ToString("0000");
            return 2;
        }

        public int CLRWDT(Simulator picSim)
        {
            picSim.TOBit = "1";
            picSim.PDBit = "1";
            // TODO reset Prescaler & WDT
            return 1;
        }

        public int GOTO(Simulator picSim)
        {
            //Console.Write("Goto: " + argument1.ToString("0000"));
            picSim.ProgramCounter = (argument1 - 1).ToString("0000");
            return 2;
        }

        public int IORLW(Simulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (wRegister | argument1);
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return 1;
        }

        public int MOVLW(Simulator picSim)
        {
            int result = argument1;
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            
            return 1;
        }

        // TODO: RETFIE

        public int RETLW(Simulator picSim)
        {
            picSim.WRegister = argument1.ToString("X2");
            picSim.ProgramCounter = (picSim.Stack - 1).ToString("0000");
            return 2;
        }

        public int RETURN(Simulator picSim)
        {
            picSim.ProgramCounter = (picSim.Stack - 1).ToString("0000");
            return 2;
        }

        // TODO: SLEEP

        public int SUBLW(Simulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (argument1 - wRegister) & 0xFF;
            CheckCBit(picSim, InstructionType.SUBLW, wRegister, argument1);
            CheckDCBit(picSim, InstructionType.SUBLW, wRegister, argument1);
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return 1;
        }

        public int XORLW(Simulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = wRegister ^ argument1;
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return 1;
        }


        // --------------- HELP FUNCTIONS ---------------

        private void ResetBits(Simulator picSim)
        {
            //picSim.ZBit = "0";
        }

        private void CheckZBit(Simulator picSim, int result)
        {
            if (result == 0)
            {
                // Set ZBit to 1
                picSim.ZBit = "1";
            } else
            {
                picSim.ZBit = "0";
            }
        }

        private void CheckDCBit(Simulator picSim, InstructionType type, int wRegister, int argument)
        {
            if (type == InstructionType.ADDLW)
            {
                if ((wRegister & 0x0F + argument & 0x0F) > 0xFF)
                {
                    picSim.DCBit = "1";
                }
                else
                {
                    picSim.DCBit = "0";
                }
            }
            else if (type == InstructionType.SUBLW)
            {
                if (((argument & 0x0F) + ((~wRegister + 1) & 0x0F)) > 0x0F)
                {
                    picSim.DCBit = "1";
                }
                else
                {
                    picSim.DCBit = "0";
                }
            }
            else if (type == InstructionType.SUBWF)
            {
                if ((((argument & 0x0F) + ((~wRegister + 1) & 0x0F))) > 0x0F)
                {
                    picSim.DCBit = "1";
                }
                else
                {
                    picSim.DCBit = "0";
                }
            }
        }

        private void CheckCBit(Simulator picSim, InstructionType type, int wRegister, int argument)
        {
            if (type == InstructionType.SUBLW)
            {
                if (argument + (((~wRegister + 1) & 0xFF) & 0x1FF) > 0xFF)
                {
                    picSim.CBit = "1";
                }
                else
                {
                    picSim.CBit = "0";
                }
            }
            else if (type == InstructionType.ADDLW)
            {
                if ((wRegister + argument) > 0xFF)
                {
                    picSim.CBit = "1";
                }
                else
                {
                    picSim.CBit = "0";
                }
            }
            else if (type == InstructionType.SUBWF)
            {
                if (argument + (((~wRegister + 1) & 0xFF) & 0x1FF) > 0xFF)
                {
                    picSim.CBit = "1";
                }
                else
                {
                    picSim.CBit = "0";
                }
            }
        }

        private int GetIndirectAddress(Simulator picSim, int address)
        {
            if (address == 0x00)
            {
                return picSim.getRegister(0x04);
            } else if (address == 0x03 || address == 0x02 || address == 0x04 || address == 0x0A || address == 0x0B)
            {
                return address;
            } 
            else
            {    
                int rp0 = Int32.Parse(picSim.RP0Bit) << 7;
                int rp1 = Int32.Parse(picSim.RP1Bit) << 8;
                Console.Write("Adressiere Bank " + rp0);
                return ((address | rp0) | rp1);
            }
        }
    }
}
