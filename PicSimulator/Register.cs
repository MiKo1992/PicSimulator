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

        /// <summary>
        /// Creates a new register object
        /// </summary>
        /// <param name="name">Name of the new register</param>
        /// <param name="value">Value of the new register</param>
        public Register(RegisterTypes name, int value)
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// Returns the name of the register
        /// </summary>
        /// <returns>register name</returns>
        public RegisterTypes getName()
        {
            return name;
        }

        /// <summary>
        /// Returns the register value
        /// </summary>
        /// <returns>register value</returns>
        public int getValue()
        {
            return value;
        }
    }
}
