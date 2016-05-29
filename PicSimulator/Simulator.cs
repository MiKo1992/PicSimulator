using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    /// <summary>
    /// The main class for the PicSimulator
    /// </summary>
    class Simulator : INotifyPropertyChanged
    {
        private Form1 userInterface;
        private List<String> fileContent = new List<String>();
        private RegisterControl registerCon = new RegisterControl();
        private InstructionSet instructionSet = new InstructionSet();
        private Stack<int> stack = new Stack<int>();

        private int startLine;
        private int endLine;
        private int programCounter;
        private int timer;
        private int timerdelay;

        private int wRegister;
        public event PropertyChangedEventHandler PropertyChanged;

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
            instructionSet.setInstructionSet(new CodeInterpreter().getInstructions(fileContent));
        }

        public void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void NextStep()
        {
            instructionSet.GetInstruction(programCounter).Execute(this);
            ProgramCounter = (programCounter + 1).ToString();
            Program.mainForm.HighlightLine(instructionSet.GetInstruction(programCounter).getLine());
        }

        public int Timer
        {
            get
            {
                return timer;
            }
            set
            {
                if (timerdelay <= 0)
                {
                    // external clock
                    if (Int32.Parse(T0CSBit) == 0)
                    {
                        timer = value;
                    }
                    else
                    {
                        if (Int32.Parse(T0SEBit) == 0)
                        {
                            // -4 => von 0 auf 1, low-to-high
                            if (value == -4)
                            {
                                timer += 1;
                            }
                        }
                        else
                        {
                            if (value == -5)
                            {
                                timer += 1;
                            }
                        }

                    }
                    if (timerdelay == -1)
                    {
                        Console.Write("Timerdelay war: -1 also + 1 fuer timer");
                        timer += 1;
                    }
                    int scaler = 1;
                    if (Int32.Parse(PSABit) == 0)
                    {
                        int bitvalue = (Int32.Parse(PS2Bit) << 2) + (Int32.Parse(PS1Bit) << 1) + (Int32.Parse(PS0Bit));
                        switch (bitvalue)
                        {
                            case 0:
                                scaler = 2;
                                break;
                            case 1:
                                scaler = 4;
                                break;
                            case 2:
                                scaler = 8;
                                break;
                            case 3:
                                scaler = 16;
                                break;
                            case 4:
                                scaler = 32;
                                break;
                            case 5:
                                scaler = 64;
                                break;
                            case 6:
                                scaler = 128;
                                break;
                            case 7:
                                scaler = 256;
                                break;
                            default:
                                scaler = 1;
                                break;
                        }
                    }
                    if ((timer / scaler) == 1)
                    {
                        if (((getRegister((int)RegisterType.TMR0) + 1) & 0x100) > 0)
                        {
                            T0IFBit = "1";
                            // Setze TMR0IF in INTCON
                            ZBit = "1";
                        }
                        else
                        {
                            //T0IFBit = "0";
                        }
                        getRegisterControl().SetRegisterAtAddress((int)RegisterType.TMR0, getRegister((int)RegisterType.TMR0) + 1);
                        timer = timer % scaler;
                        Console.Write("Neuer timer = " + timer);
                    }
                    else if ((timer / scaler) == 2)
                    {
                        Console.Write("Timer + Scaler war 2");
                        getRegisterControl().SetRegisterAtAddress((int)RegisterType.TMR0, getRegister((int)RegisterType.TMR0) + 2);
                        timer = 0;
                    }
                    NotifyPropertyChanged("Timer");
                }
                else
                {
                    Console.Write("1 Delay: " + timerdelay);
                    Console.Write("TMR0: " + getRegister((int)RegisterType.TMR0));
                    timerdelay -= value;
                }

                Program.mainForm.UpdateStorageSet();
            }
        }

        public void SetDelay(int delayBy)
        {
            timerdelay = delayBy;
        }

        public string ProgramCounter
        {
            get
            {
                return this.programCounter.ToString("0000");
            }
            set
            {
                if (value != programCounter.ToString("0000"))
                {
                    programCounter = Int32.Parse(value);
                    //Console.Write("Programmcounter: " + programCounter);
                    NotifyPropertyChanged("ProgramCounter");
                }
            }
        }

        public int Stack
        {
            get
            {
                int value = stack.Pop();
                Program.mainForm.UpdateStack(stack);
                return value;
            }
            set
            {
                if (stack.Count() == 8)
                {
                    // case muss nicht impl. werden (wir zeigen das 9. element einfach nicht an :))
                }
                stack.Push(value);

                Program.mainForm.UpdateStack(stack);
            }
        }

        public string WRegister
        {
            get
            {
                return this.wRegister.ToString("X2");
            }
            set
            {
                if (value != this.wRegister.ToString("X2"))
                {
                    this.wRegister = Int32.Parse(value, System.Globalization.NumberStyles.HexNumber);
                    NotifyPropertyChanged("wRegister");
                }
            }
        }

        public string CBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 0) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 0) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 0) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.STATUS, 0);
                    }
                    NotifyPropertyChanged("CBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.STATUS, 0);
                    NotifyPropertyChanged("CBit");
                }
            }
        }

        public string DCBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 1) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.STATUS] & (1 << 1)) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 1) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.STATUS, 1);
                    }
                    NotifyPropertyChanged("DCBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.STATUS, 1);
                    NotifyPropertyChanged("DCBit");
                }

            }
        }

        public string ZBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 2) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 2) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 2) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.STATUS, 2);
                    }
                    NotifyPropertyChanged("ZBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.STATUS, 2);
                    NotifyPropertyChanged("ZBit");
                }
            }
        }

        public string PDBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 3) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 3) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 3) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.STATUS, 3);
                    }
                    NotifyPropertyChanged("PDBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.STATUS, 3);
                    NotifyPropertyChanged("PDBit");
                }
            }
        }

        public string TOBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 4) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 4) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 4) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.STATUS, 4);
                    }
                    NotifyPropertyChanged("TOBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.STATUS, 4);
                    NotifyPropertyChanged("TOBit");
                }
            }
        }

        public string RP0Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 5) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 5) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 5) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.STATUS, 5);
                    }
                    NotifyPropertyChanged("RP0Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.STATUS, 5);
                    NotifyPropertyChanged("RP0Bit");
                }
            }
        }

        public string GIEBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 7) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 7) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 7) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.INTCON, 7);
                    }
                    NotifyPropertyChanged("GIEBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.INTCON, 7);
                    NotifyPropertyChanged("GIEBit");
                }
            }
        }

        public string EEIEBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 6) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 6) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 6) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.INTCON, 6);
                    }
                    NotifyPropertyChanged("EEIEBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.INTCON, 6);
                    NotifyPropertyChanged("EEIEBit");
                }
            }
        }

        public string T0IEBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 5) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 5) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 5) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.INTCON, 5);
                    }
                    NotifyPropertyChanged("T0IEBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.INTCON, 5);
                    NotifyPropertyChanged("T0IEBit");
                }
            }
        }

        public string INTEBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 4) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 4) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 4) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.INTCON, 4);
                    }
                    NotifyPropertyChanged("INTEBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.INTCON, 4);
                    NotifyPropertyChanged("INTEBit");
                }
            }
        }

        public string RBIEBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 3) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 3) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 3) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.INTCON, 3);
                    }
                    NotifyPropertyChanged("RBIEBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.INTCON, 3);
                    NotifyPropertyChanged("RBIEBit");
                }
            }
        }

        public string T0IFBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 2) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 2) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 2) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.INTCON, 2);
                    }
                    NotifyPropertyChanged("T0IFBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.INTCON, 2);
                    NotifyPropertyChanged("T0IFBit");
                }
            }
        }

        public string INTFBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 1) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 1) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 1) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.INTCON, 1);
                    }
                    NotifyPropertyChanged("INTFBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.INTCON, 1);
                    NotifyPropertyChanged("INTFBit");
                }
            }
        }

        public string RBIFBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 0) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 0) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.INTCON] & 1 << 0) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.INTCON, 0);
                    }
                    NotifyPropertyChanged("RBIFBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.INTCON, 0);
                    NotifyPropertyChanged("RBIFBit");
                }
            }
        }

        public string RA0Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 0) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 0) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTA, 0);
                    }
                    NotifyPropertyChanged("RA0Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTA, 0);
                    NotifyPropertyChanged("RA0Bit");
                }
            }
        }

        public string RA1Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 1) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 1) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTA, 1);
                    }
                    NotifyPropertyChanged("RA1Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTA, 1);
                    NotifyPropertyChanged("RA1Bit");
                }
            }
        }

        public string RA2Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 2) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 2) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTA, 2);
                    }
                    NotifyPropertyChanged("RA2Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTA, 2);
                    NotifyPropertyChanged("RA2Bit");
                }
            }
        }

        public string RA3Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 3) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 3) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTA, 3);
                    }
                    NotifyPropertyChanged("RA3Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTA, 3);
                    NotifyPropertyChanged("RA3Bit");
                }
            }
        }

        public string RA4Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 4) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 4) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTA, 4);
                    }
                    NotifyPropertyChanged("RA4Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTA, 4);
                    NotifyPropertyChanged("RA4Bit");
                }
            }
        }

        public string RA5Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 5) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 5) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTA, 5);
                    }
                    NotifyPropertyChanged("RA5Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTA, 5);
                    NotifyPropertyChanged("RA5Bit");
                }
            }
        }

        public string RA6Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 6) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 6) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTA, 6);
                    }
                    NotifyPropertyChanged("RA6Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTA, 6);
                    NotifyPropertyChanged("RA6Bit");
                }
            }
        }

        public string RA7Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 7) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTA] & 1 << 7) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTA, 7);
                    }
                    NotifyPropertyChanged("RA7Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTA, 7);
                    NotifyPropertyChanged("RA7Bit");
                }
            }
        }

        public string RB0Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 0) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 0) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTB, 0);
                    }
                    NotifyPropertyChanged("RB0Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTB, 0);
                    NotifyPropertyChanged("RB0Bit");
                }
            }
        }

        public string RB1Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 1) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 1) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTB, 1);
                    }
                    NotifyPropertyChanged("RB1Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTB, 1);
                    NotifyPropertyChanged("RB1Bit");
                }
            }
        }

        public string RB2Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 2) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 2) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTB, 2);
                    }
                    NotifyPropertyChanged("RB2Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTB, 2);
                    NotifyPropertyChanged("RB2Bit");
                }
            }
        }

        public string RB3Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 3) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 3) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTB, 3);
                    }
                    NotifyPropertyChanged("RB3Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTB, 3);
                    NotifyPropertyChanged("RB3Bit");
                }
            }
        }

        public string RB4Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 4) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 4) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTB, 4);
                    }
                    NotifyPropertyChanged("RB4Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTB, 4);
                    NotifyPropertyChanged("RB4Bit");
                }
            }
        }

        public string RB5Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 5) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 5) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTB, 5);
                    }
                    NotifyPropertyChanged("RB5Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTB, 5);
                    NotifyPropertyChanged("RB5Bit");
                }
            }
        }

        public string RB6Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 6) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 6) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTB, 6);
                    }
                    NotifyPropertyChanged("RB6Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTB, 6);
                    NotifyPropertyChanged("RB6Bit");
                }
            }
        }

        public string RB7Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 7) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                bool actualValue = ((registerCon.getRegister()[(int)RegisterType.PORTB] & 1 << 7) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.PORTB, 7);
                    }
                    NotifyPropertyChanged("RB7Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.PORTB, 7);
                    NotifyPropertyChanged("RB7Bit");
                }
            }
        }

        public string RP1Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 6) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 6) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 6) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.STATUS, 6);
                    }
                    NotifyPropertyChanged("RP1Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.STATUS, 6);
                    NotifyPropertyChanged("RP1Bit");
                }
            }
        }

        public string IRPBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 7) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 7) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.STATUS] & 1 << 7) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.STATUS, 7);
                    }
                    NotifyPropertyChanged("IRPBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.STATUS, 7);
                    NotifyPropertyChanged("IRPBit");
                }
            }
        }

        public string PS0Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 0) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 0) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 0) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.OPTION_REG, 0);
                    }
                    NotifyPropertyChanged("PS0Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.OPTION_REG, 0);
                    NotifyPropertyChanged("PS0Bit");
                }
            }
        }

        public string PS1Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 1) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 1) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 1) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.OPTION_REG, 1);
                    }
                    NotifyPropertyChanged("PS1Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.OPTION_REG, 1);
                    NotifyPropertyChanged("PS1Bit");
                }
            }
        }

        public string PS2Bit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 2) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 2) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 2) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.OPTION_REG, 2);
                    }
                    NotifyPropertyChanged("PS2Bit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.OPTION_REG, 2);
                    NotifyPropertyChanged("PS2Bit");
                }
            }
        }

        public string PSABit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 3) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 3) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 3) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.OPTION_REG, 3);
                    }
                    NotifyPropertyChanged("PSABit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.OPTION_REG, 3);
                    NotifyPropertyChanged("PSABit");
                }
            }
        }

        public string T0SEBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 4) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 4) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 4) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.OPTION_REG, 4);
                    }
                    NotifyPropertyChanged("T0SEBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.OPTION_REG, 4);
                    NotifyPropertyChanged("T0SEBit");
                }
            }
        }

        public string T0CSBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 5) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 5) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 5) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.OPTION_REG, 5);
                    }
                    NotifyPropertyChanged("T0CSBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.OPTION_REG, 5);
                    NotifyPropertyChanged("T0CSBit");
                }
            }
        }

        public string INTEDGBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 6) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 6) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 6) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.OPTION_REG, 6);
                    }
                    NotifyPropertyChanged("INTEDGBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.OPTION_REG, 6);
                    NotifyPropertyChanged("INTEDGBit");
                }
            }
        }

        public string RBPUBit
        {
            get
            {
                if ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 7) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 7) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerCon.getRegister()[(int)RegisterType.OPTION_REG] & 1 << 7) == 0)))
                    {

                    }
                    else
                    {
                        registerCon.ToggleBit((int)RegisterType.OPTION_REG, 7);
                    }
                    NotifyPropertyChanged("RBPUBit");
                }
                catch (FormatException)
                {
                    registerCon.ToggleBit((int)RegisterType.OPTION_REG, 7);
                    NotifyPropertyChanged("RBPUBit");
                }
            }
        }

        public int getRegister(int address)
        {
            return registerCon.getRegister()[address];
        }

        public RegisterControl getRegisterControl()
        {
            return registerCon;
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
