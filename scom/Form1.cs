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
        // constants
        enum BAUD
        {
            B1200 = 0,
            B2400,
            B4800,
            B9600,
            B19200,
            B38400,
            B54600,
            B115200,
            NUM_OF_BAUDS,
        };

        enum FRAME
        {
            D7PNS1 = 0,
            D7PNS2,
            D7POS1,
            D7POS2,
            D7PES1,
            D7PES2,
            D8PNS1,
            D8PNS2,
            D8POS1,
            D8POS2,
            D8PES1,
            D8PES2,
            NUM_OF_FRAMES,
        };

        enum TERM
        {
            NONE = 0,
            CR,
            LF,
            CRLF,
            NUM_OF_TERMS,
        };

        private readonly int MAX_COMMAND_HISTORY = 20;

        
        private readonly string[] BAUDS_DISP = new string[(int)BAUD.NUM_OF_BAUDS]{ "1200", "2400", "4800", "9600", "19200", "38400", "54600", "115200" };
        private readonly string[] FRAMES_DISP = new string[(int)FRAME.NUM_OF_FRAMES]{ "7N1", "7N2", "7O1", "7O2", "7E1", "7E2", "8N1", "8N2", "8O1", "8O2", "8E1", "8E2" };
        private readonly string[] TERMS_DISP = new string[(int)TERM.NUM_OF_TERMS]{ "NONE", "CR", "LF", "CRLF" };
        private readonly string[] TERMS_STR = new string[(int)TERM.NUM_OF_TERMS]{ "", "\r", "\n", "\r\n" };

        // variables
        private string appPath;
        private string appName;
        private string appNameWoExt;
        
        private StreamWriter streamWriterLog = null;
        
        public Form1()
        {
            InitializeComponent();
        }
                
        delegate void AppendTextCallback(string text);

        private void AppendText(string text)
        {
            if (richTextBoxSCREEN.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(AppendText);
                BeginInvoke(d, new object[] { text });
            }
            else
            {
                richTextBoxSCREEN.Focus();
                richTextBoxSCREEN.AppendText(text);
                if (streamWriterLog != null)
                {
                    streamWriterLog.Write(text);
                }
                comboBoxCOMMAND.Focus();
            }
        }

        private void SetBaud(int baud)
        {
            serialPort1.BaudRate = baud;
        }

        private void SetFrame(int frame)
        {
            switch ((FRAME)frame)
            {
                case FRAME.D7PNS1:
                    serialPort1.DataBits = 7;
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.One;
                    break;
                case FRAME.D7PNS2:
                    serialPort1.DataBits = 7;
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.Two;
                    break;
                case FRAME.D7POS1:
                    serialPort1.DataBits = 7;
                    serialPort1.Parity = Parity.Odd;
                    serialPort1.StopBits = StopBits.One;
                    break;
                case FRAME.D7POS2:
                    serialPort1.DataBits = 7;
                    serialPort1.Parity = Parity.Odd;
                    serialPort1.StopBits = StopBits.Two;
                    break;
                case FRAME.D7PES1:
                    serialPort1.DataBits = 7;
                    serialPort1.Parity = Parity.Even;
                    serialPort1.StopBits = StopBits.One;
                    break;
                case FRAME.D7PES2:
                    serialPort1.DataBits = 7;
                    serialPort1.Parity = Parity.Even;
                    serialPort1.StopBits = StopBits.Two;
                    break;
                case FRAME.D8PNS1:
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.One;
                    break;
                case FRAME.D8PNS2:
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.Two;
                    break;
                case FRAME.D8POS1:
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.Odd;
                    serialPort1.StopBits = StopBits.One;
                    break;
                case FRAME.D8POS2:
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.Odd;
                    serialPort1.StopBits = StopBits.Two;
                    break;
                case FRAME.D8PES1:
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.Even;
                    serialPort1.StopBits = StopBits.One;
                    break;
                case FRAME.D8PES2:
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
            appPath = Application.ExecutablePath;
            appName = Path.GetFileName(appPath);
            appNameWoExt = Path.ChangeExtension(appName, null);
            this.Text = appNameWoExt;
            comboBoxCOM_DropDown(sender, e);

            comboBoxBAUD.Items.AddRange(BAUDS_DISP);
            comboBoxBAUD.SelectedIndex = (int)BAUD.B9600;

            comboBoxFRAME.Items.AddRange(FRAMES_DISP);
            comboBoxFRAME.SelectedIndex = (int)FRAME.D8PNS1;

            comboBoxTERM.Items.AddRange(TERMS_DISP);
            comboBoxTERM.SelectedIndex = (int)TERM.CR;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Close();
                    streamWriterLog.Dispose();
                }
                catch
                {
                    ; // nothing to do
                }
            }
        }

        private void comboBoxCOM_DropDown(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBoxCOM.Items.Clear();

            int num = ports.Length;
            if (num != 0)
            {
                comboBoxCOM.Items.AddRange(ports);
                comboBoxCOM.SelectedIndex = 0;
            }
            else
            {
                comboBoxCOM.Text = "NONE";
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

            if (timeStampToolStripMenuItem.Checked)
            {
                DateTime dt = DateTime.Now;
                AppendText("[" + dt.ToString("yyyy/MM/dd HH:mm:ss") + "]\r\n");
            }
                        
            if (rXToolStripMenuItem.Checked)
            {
                AppendText("[RX]\r\n");
            }
            AppendText(text);
        }


        private void comboBoxBAUD_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetBaud(int.Parse(comboBoxBAUD.Text));
        }

        private void comboBoxFRAME_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFrame(comboBoxFRAME.SelectedIndex);
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
            ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logToolStripMenuItem.Checked == false)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                ofd.Filter = "any file(*.*)|*.*";
                ofd.FilterIndex = 0;
                ofd.Title = "log file";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.Text.Encoding enc;
                        if (aSCIIToolStripMenuItem.Checked)
                        {
                            enc = new System.Text.ASCIIEncoding();
                        }
                        else if (shiftJISToolStripMenuItem.Checked)
                        {
                            enc = Encoding.GetEncoding("Shift_JIS");
                        }
                        else
                        {
                            enc = new System.Text.UTF8Encoding(false); // without BOM
                        }
                        
                        streamWriterLog = new StreamWriter(ofd.FileName, true, enc);
                        logToolStripMenuItem.Checked = true;
                        this.Text += ":" + ((FileStream)(streamWriterLog.BaseStream)).Name;
                    }
                    catch (Exception excpt)
                    {
                        MessageBox.Show(excpt.Message, "error");
                    }
                }
            }
            else
            {
                streamWriterLog.Dispose();
                streamWriterLog = null;
                logToolStripMenuItem.Checked = false;
                this.Text = appNameWoExt;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxSCREEN.Clear();
        }

        private void tXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tXToolStripMenuItem.Checked = !tXToolStripMenuItem.Checked;
        }

        private void rXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rXToolStripMenuItem.Checked = !rXToolStripMenuItem.Checked;
        }

        private void timeStampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeStampToolStripMenuItem.Checked = !timeStampToolStripMenuItem.Checked;
        }

        private void comboBoxCOMMAND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (serialPort1.IsOpen)
                {
                    string term = TERMS_STR[comboBoxTERM.SelectedIndex];
                    try
                    {
                        serialPort1.Write(comboBoxCOMMAND.Text + term);
                    }
                    catch (Exception excpt)
                    {
                        MessageBox.Show(excpt.Message, "error");
                    }
                    finally
                    {
                        if (timeStampToolStripMenuItem.Checked)
                        {
                            DateTime dt = DateTime.Now;
                            AppendText("[" + dt.ToString("yyyy/MM/dd HH:mm:ss") + "]\r\n");
                        }
                        if (tXToolStripMenuItem.Checked)
                        {
                            AppendText("[TX]\r\n");
                        }
                        AppendText(comboBoxCOMMAND.Text + term);
                        comboBoxCOMMAND.Items.Insert(0, comboBoxCOMMAND.Text);
                        // do not check the duplicated command
                        if (comboBoxCOMMAND.Items.Count > MAX_COMMAND_HISTORY)
                        {
                            // remove the oldest histroy
                            comboBoxCOMMAND.Items.RemoveAt(comboBoxCOMMAND.Items.Count - 1);
                        }
                    }
                }
            }
        }

        private void checkBoxCONNECT_Click(object sender, EventArgs e)
        {
            if (checkBoxCONNECT.Checked == false)
            {
                try
                {
                    serialPort1.PortName = comboBoxCOM.Text;
                    SetBaud(int.Parse(comboBoxBAUD.Text));
                    SetFrame(comboBoxFRAME.SelectedIndex);

                    serialPort1.Open();
                    if (serialPort1.IsOpen)
                    {
                        checkBoxCONNECT.Text = "disconnect";
                        comboBoxCOM.Enabled = false;
                        checkBoxCONNECT.Checked = true;
                    }
                    else
                    {
                        checkBoxCONNECT.Checked = false;
                    }
                }
                catch (Exception excpt)
                {
                    MessageBox.Show(excpt.Message, "error");
                }

            }
            else
            {
                try
                {
                    serialPort1.Close();
                }
                catch (Exception excpt)
                {
                    MessageBox.Show(excpt.Message, "error");
                }
                finally
                {
                    checkBoxCONNECT.Text = "connect";
                    comboBoxCOM.Enabled = true;
                    checkBoxCONNECT.Checked = false;
                }
            }
        }

    }
}

