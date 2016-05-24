using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class Register
    {

        private RegisterTypes name;
        private int value = 0x00;

        public Register(RegisterTypes name, int value)
        {
            this.name = name;
            this.value = value;
        }

        public RegisterTypes getName()
        {
            return name;
        }

        public int getValue()
        {
            return value;
        }
    }
}
