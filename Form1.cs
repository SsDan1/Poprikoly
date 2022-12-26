using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NX10_Open_CS_Library
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.D = Convert.ToDouble(comboBox1.Text);
            if (Program.D == 1)
            {
                Program.Postr1();
            }

            if (Program.D == 2)
            {
                Program.Postr2();
            }

            if (Program.D == 3)
            {
                Program.Postr3();
            }

            if (Program.D == 4)
            {
                Program.Postr4();
            }
            if (Program.D == 5)
            {
                Program.Postr5();
            }

            if (Program.D == 6)
            {
                Program.Postr6();
            }

            if (Program.D == 8)
            {
                Program.Postr8();
            }

            if (Program.D == 10)
            {
                Program.Postr10();
            }

            if (Program.D == 12)
            {
                Program.Postr12();
            }

            if (Program.D == 14)
            {
                Program.Postr14();
            }

            if (Program.D == 16)
            {
                Program.Postr16();
            }

            if (Program.D == 18)
            {
                Program.Postr18();
            }

            if (Program.D == 20)
            {
                Program.Postr20();
            }

            if (Program.D == 22)
            {
                Program.Postr22();
            }

            if (Program.D == 24)
            {
                Program.Postr24();
            }

            if (Program.D == 27)
            {
                Program.Postr27();
            }

            if (Program.D == 30)
            {
                Program.Postr30();
            }

            if (Program.D == 36)
            {
                Program.Postr36();
            }

            if (Program.D == 42)
            {
                Program.Postr42();
            }

            if (Program.D == 48)
            {
                Program.Postr48();
            }

            

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.A = Convert.ToDouble(comboBox2.Text);
            if (Program.A == 1)
            {
                Program.Vint();
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
