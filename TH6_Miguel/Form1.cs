using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TH6_Miguel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static int inputan;

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_play_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out inputan))
            {
                MessageBox.Show("Pleaseee fill a number", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (Convert.ToInt32(textBox1.Text) <= 3)
            {
                MessageBox.Show("Number must be greater than 3", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                inputan = Convert.ToInt32(textBox1.Text);
                Form2 form2 = new Form2();
                form2.Show();
            }
        }
    }
}
