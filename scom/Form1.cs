using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace scom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        delegate void AppendTextCallback(string text);

        private void AppendText(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(AppendText);
                BeginInvoke(d, new object[] { text });
            }
            else
            {
                richTextBox1.Focus();
                richTextBox1.AppendText(text);
                textBox2.Focus();

            }
        }

        private void SetBaud(int baud)
        {
            serialPort1.BaudRate = baud;
        }

        private void SetFrame(string frame)
        {
            switch (frame)
            {
                case "8N1":
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.One;
                    break;
                case "8N2":
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.Two;
                    break;
                case "8O1":
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.Odd;
                    serialPort1.StopBits = StopBits.One;
                    break;
                case "8O2":
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.Odd;
                    serialPort1.StopBits = StopBits.Two;
                    break;
                case "8E1":
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.Even;
                    serialPort1.StopBits = StopBits.One;
                    break;
                case "8E2":
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.Even;
                    serialPort1.StopBits = StopBits.Two;
                    break;

            }
        }

        private void SelectCheckedItem_Encode(object sender)
        {
            foreach (ToolStripMenuItem item in encodeToolStripMenuItem.DropDownItems)
            {
                if (item.Equals(sender))
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1_DropDown(sender, e);

            string[] bauds = { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" };
            comboBox2.Items.AddRange(bauds);
            comboBox2.SelectedIndex = 3;

            string[] frames = { "8N1", "8N2", "8E1", "8E2", "8O1", "8O2" };
            comboBox3.Items.AddRange(frames);
            comboBox3.SelectedIndex = 0;

            string[] terms = { "NONE", "CR", "LF", "CRLF" };
            comboBox4.Items.AddRange(terms);
            comboBox4.SelectedIndex = 1;

        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.Clear();

            int num = ports.Length;
            if (num != 0)
            {
                comboBox1.Items.AddRange(ports);
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Text = "NONE";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                try {
                    serialPort1.PortName = comboBox1.Text;
                    SetBaud(int.Parse(comboBox2.Text));
                    SetFrame(comboBox3.Text);

                    serialPort1.Open();
                    button1.Text = "disconnect"; 
                    comboBox1.Enabled = false;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString(), "error");
                }
                
            }
            else
            {
                try
                {
                    serialPort1.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString(), "error");
                }
                finally
                {
                    button1.Text = "connect";
                    comboBox1.Enabled = true;
                }
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int nums = serialPort1.BytesToRead;
            byte[] buffer = new byte[nums];
            serialPort1.Read(buffer, 0, nums);
            string text;

            if (aSCIIToolStripMenuItem.Checked)
            {
                text = Encoding.ASCII.GetString(buffer);
            }
            else if (shiftJISToolStripMenuItem.Checked)
            {
                text = Encoding.GetEncoding("shift_jis").GetString(buffer);
            }
            else
            {
                text = Encoding.UTF8.GetString(buffer);
            }
            AppendText(text);
        }

        

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (serialPort1.IsOpen)
                {
                    string term = "";
                    try
                    {
                        serialPort1.Write(textBox2.Text);
                        switch (comboBox4.Text)
                        {
                            case "CR":
                                term = "\r";
                                break;
                            case "LF":
                                term = "\n";
                                break;
                            case "CRLF":
                                term = "\r\n";
                                break;
                        }
                        serialPort1.Write(term);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString(), "error");
                    }
                    finally
                    {
                        AppendText(textBox2.Text + term);
                        textBox2.Clear();
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Close();
                }
                catch
                {
                    ; // nothing to do
                }
            }
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetBaud(int.Parse(comboBox2.Text));
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFrame(comboBox3.Text);
        }

        private void aSCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectCheckedItem_Encode(sender);
        }

        private void shiftJISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectCheckedItem_Encode(sender);
        }

        private void uTF8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectCheckedItem_Encode(sender);
        }

        private void transferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "text file(*.txt)|*.txt";
            ofd.FilterIndex = 0;
            ofd.Title = "select file";
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(ofd.FileName))
                {
                    string text = sr.ReadToEnd();
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write(text);
                    }
                    AppendText(text);
                }
            }
        }
    }
}

