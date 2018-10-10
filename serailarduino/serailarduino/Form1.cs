using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports; 

namespace serailarduino
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mydelegate = new Adddatadelegate(Adddata);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(ports);
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(ports);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen == true)
            {
                return;
            }
            else
            {
                serialPort1.Open();
                button2.Enabled = false;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                return;
            }
            else
            {
                serialPort1.Close();
                button2.Enabled = true;
                button3.Enabled = false;
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
           // serialPort1.PortName = comboBox1.Text;
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
           // serialPort1.BaudRate = Int32.Parse(comboBox2.Text); //เปลี่ยนสตริง
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.BaudRate = Int32.Parse(comboBox2.Text);
        }


        public void Adddata(string str)
        {
            textBox1.AppendText(str);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string str = sp.ReadExisting();
            Console.WriteLine(str);
            //textBox1.AppendText(str);//cross thread problem 
           // Adddata(str);
            textBox1.Invoke(mydelegate, str);
        }

        //declare delegate
        public delegate void Adddatadelegate(string str);
        //define delegatest
        public Adddatadelegate mydelegate;

      
    }
}
