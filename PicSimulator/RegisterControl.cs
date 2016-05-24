using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class RegisterControl
    {
        ArrayList register = new ArrayList();

        public RegisterControl()
        {
            init();
        }

        private void init()
        {
            register.Add(new Register(RegisterTypes.INDF, 0x00));
            register.Add(new Register(RegisterTypes.TMR0, 0x00));
            register.Add(new Register(RegisterTypes.PCL, 0x00));
            register.Add(new Register(RegisterTypes.STATUS, 0x18));
            register.Add(new Register(RegisterTypes.FSR, 0x00));
            register.Add(new Register(RegisterTypes.PORTA, 0x00));
            register.Add(new Register(RegisterTypes.PORTB, 0x00));
            register.Add(new Register(RegisterTypes.EEDATA, 0x00));
            register.Add(new Register(RegisterTypes.EEADR, 0x00));
            register.Add(new Register(RegisterTypes.PCLATH, 0x00));
            register.Add(new Register(RegisterTypes.INTCON, 0x00));
            register.Add(new Register(RegisterTypes.OPTION_REG, 0xFF));
            register.Add(new Register(RegisterTypes.TRISA, 0x1F));
            register.Add(new Register(RegisterTypes.TRISB, 0xFF));
            register.Add(new Register(RegisterTypes.EECON1, 0x00));
            register.Add(new Register(RegisterTypes.EECON2, 0x00));
        }

        public ArrayList getRegister()
        {
            return register;
        }
    }
}
