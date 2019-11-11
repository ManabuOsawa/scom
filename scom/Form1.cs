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
        public const byte C_NUL = 0x00;
        public const byte C_STX = 0x02;
        public const byte C_ETX = 0x03;
        public const byte C_ACK = 0x06;
        public const byte C_HT = 0x09;
        public const byte C_LF = 0x0A;
        public const byte C_CR = 0x0D;
        public const byte C_NAK = 0x15;
        public const byte C_ESC = 0x1B;

                     
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

        // e.g.)
        // input : src = "003F0D"
        // output: dst[0] = 0x00, dst[1] = 0x3F, dst[2] = 0x0D
        private void ConvertHexStringToByteArray(string src, byte[] dst)
        {
            int pos = 0;
            for (int i = 0; i < src.Length; i += 2)
            {
                string s = src.Substring(i, 2);
                dst[pos] = Convert.ToByte(s, 16);
                pos++;
            }
            return;
        }
                
        private bool IsHexString(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            foreach (char c in s)
            {
                if (!Uri.IsHexDigit(c))
                {
                    return false;
                }
            }
            return true;
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
                // HideSelection(false) + AppendText -> auto scroll
                richTextBoxSCREEN.AppendText(text);
                if (streamWriterLog != null)
                {
                    streamWriterLog.Write(text);
                }
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

            // richtextbox requires "focus" for auto scroll when text is appended.
            // however, using hideselection(false), it doesn't require.
            richTextBoxSCREEN.HideSelection = false;

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


        // マルチバイト文字を分割受信（複数回のDataRecivedで完結）する場合を考える。
        // マルチバイト文字を構成するデータを一時的に格納するバッファ
        // UTF-8の4バイトを最大としておく。
        private byte[] m_recv_multibyte_chara_data= new byte[4];
        // マルチバイト文字を構成するデータを何バイト格納したか
        private int m_recv_multibyte_chara_len = 0;
        
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes_to_read = 0;
            int buffer_len = 0;
            byte[] buffer;// = new byte[1024];
            int remove_len = 0; // 受信し切っていないマルチバイト文字の構成データは表示しないようにする
            int bytes_of_charaset = 1; // 受信した文字は何バイトから成るか（Shift_JISは1～2、UTF-8は1～4、その他は1）

            bytes_to_read = serialPort1.BytesToRead;
            if (bytes_to_read < 1)
            {
                return;
            }
            else
            {
                int offset = m_recv_multibyte_chara_len;
                buffer_len = bytes_to_read + offset;
                buffer = new byte[buffer_len];
                m_recv_multibyte_chara_len = 0;

                if (aSCIIToolStripMenuItem.Checked || binaryToolStripMenuItem.Checked)
                {
                    serialPort1.Read(buffer, 0, bytes_to_read);
                } else {
                    // マルチバイト文字の受信が完了していなければ、受信済みのデータを加える。
                    Array.Copy(m_recv_multibyte_chara_data, buffer, offset);
                    serialPort1.Read(buffer, offset, bytes_to_read);

                    Console.WriteLine("data->in({0}bytes, offset{1})", bytes_to_read, offset);

                    for (int i = 0; i < buffer_len; i++)
                    {
                        Console.WriteLine("buffer[{0}]={1}", i, buffer[i]);
                        if (shiftJISToolStripMenuItem.Checked)
                        {
                            // 2byte文字の先頭バイト受信済み
                            if (m_recv_multibyte_chara_len == 1)
                            {
                                // 2byte目のチェックなし。
                                // 仕様上は 0x40-0x7E, 0x80-0xFC
                                for (int j = 0; j < m_recv_multibyte_chara_data.Length; j++)
                                {
                                    m_recv_multibyte_chara_data[j] = 0x00;
                                }
                                m_recv_multibyte_chara_len = 0;
                                remove_len = 0;
                            }
                            else
                            {
                                // 2byte文字の先頭バイトかチェック
                                if ((buffer[i] >= 0x81 && buffer[i] <= 0x9f) ||
                                    (buffer[i] >= 0xe0 && buffer[i] <= 0xef))
                                {
                                    bytes_of_charaset = 2;
                                    m_recv_multibyte_chara_len = 1;
                                    remove_len = 1;
                                    m_recv_multibyte_chara_data[0] = buffer[i];
                                }
                                else
                                {
                                    bytes_of_charaset = 1;
                                    m_recv_multibyte_chara_len = 0;
                                    remove_len = 0;
                                    
                                }
                            }
                            
                        }
                        else
                        {
                            // 何バイトの文字かチェック
                            if (buffer[i] >= 0x00 && buffer[i] <= 0x7f)
                            {
                                bytes_of_charaset = 1;
                                m_recv_multibyte_chara_len = 0;
                                remove_len = 0;
                            }
                            else if (buffer[i] >= 0xc0 && buffer[i] <= 0xdf)
                            {
                                bytes_of_charaset = 2;
                                m_recv_multibyte_chara_len = 1;
                                remove_len = 1;
                                m_recv_multibyte_chara_data[0] = buffer[i];
                            }
                            else if (buffer[i] >= 0xe0 && buffer[i] <= 0xef)
                            {
                                bytes_of_charaset = 3;
                                m_recv_multibyte_chara_len = 1;
                                remove_len = 1;
                                m_recv_multibyte_chara_data[0] = buffer[i];
                            }
                            else if (buffer[i] >= 0xf0 && buffer[i] <= 0xff)
                            {
                                bytes_of_charaset = 4;
                                m_recv_multibyte_chara_len = 1;
                                remove_len = 1;
                                m_recv_multibyte_chara_data[0] = buffer[i];
                            }
                            else
                            {
                                m_recv_multibyte_chara_data[m_recv_multibyte_chara_len] = buffer[i];
                                m_recv_multibyte_chara_len++;
                                remove_len++;
                                if (m_recv_multibyte_chara_len == bytes_of_charaset)
                                {
                                    Console.WriteLine("utf-8->done:" + System.Text.Encoding.UTF8.GetString(m_recv_multibyte_chara_data, 0, 3));
                                    for (int j = 0; j < m_recv_multibyte_chara_data.Length; j++)
                                    {
                                        m_recv_multibyte_chara_data[j] = 0x00;
                                    }
                                    m_recv_multibyte_chara_len = 0;
                                    remove_len = 0;
                                }
                            }
                        }
                    }
                }
            }

            if (remove_len > 0)
            {
                Console.WriteLine("remove->{0}byte", remove_len);
                if (buffer_len - remove_len < 1)
                {
                    Console.WriteLine("multibytes charaset->not completed");
                    return;
                }
                buffer_len -= remove_len;
            }

            // convert CR/LF
            // CRLFが一度に受信と1行改行だが、CR、LFと別々に受信すると2行改行してしまうので、
            // 見た目を揃えるためにLFをCRに置換する。CRLFはCRCRとなり2行改行する。
            // ただし、バイナリモードは置換しない。
            if (binaryToolStripMenuItem.Checked == false) { 
                for (int i=0; i<buffer_len; i++)
                {
                    if (buffer[i] == C_LF)
                    {
                        buffer[i] = C_CR;
                    }
                }
            }

            string text;

            if (aSCIIToolStripMenuItem.Checked)
            {
                text = Encoding.ASCII.GetString(buffer);
            }
            else if (shiftJISToolStripMenuItem.Checked)
            {
                text = Encoding.GetEncoding("shift_jis").GetString(buffer, 0, buffer_len);
            }
            else if (uTF8ToolStripMenuItem.Checked)
            {
                text = Encoding.UTF8.GetString(buffer, 0, buffer_len);
                Console.WriteLine("src bytes");
                for (int i = 0; i < buffer_len; i++) { 
                        Console.WriteLine(" buffer[{0}]={1}", i, buffer[i]);
                }
                Console.WriteLine("dst string");
                Console.WriteLine(" " + text);
            } 
            else 
            {
                text = "";
                for (int i=0; i< buffer_len; i++)
                {
                    // 例) 1 2 3 CR LF -> <31> <32> <33> <0D> <0A> 
                    text += "<";
                    text += buffer[i].ToString("X2");
                    text += ">";
                    text += " ";
                }
            }

            if (timeStampToolStripMenuItem.Checked)
            {
                DateTime dt = DateTime.Now;
                if (rXToolStripMenuItem.Checked)
                {
                    AppendText("\r\n[" + dt.ToString("yyyy/MM/dd HH:mm:ss") + "][RX]\r\n");
                }
                else
                {
                    AppendText("\r\n[" + dt.ToString("yyyy/MM/dd HH:mm:ss") + "]\r\n");
                }
            }
            else
            {
                if (rXToolStripMenuItem.Checked)
                {
                    AppendText("\r\n[RX]\r\n");
                }
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

        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
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
                        if (!checkBoxCOMMAND.Checked)
                        {
                            serialPort1.Write(comboBoxCOMMAND.Text + term);
                        }
                        else
                        {
                            int cmd_len = comboBoxCOMMAND.Text.Length;
                            if (cmd_len % 2 != 0)
                            {
                                MessageBox.Show("length is not multiple of 2.", "warning");
                                return;
                            }
                            bool is_hex = IsHexString(comboBoxCOMMAND.Text);
                            if (!is_hex)
                            {
                                MessageBox.Show("invalid character is included.\r\n"+"please use 0-9, A-F, or a-f.", "warning");
                                return;
                            }
                            byte[] buffer = new byte[cmd_len/2];
                            ConvertHexStringToByteArray(comboBoxCOMMAND.Text, buffer);
                            serialPort1.Write(buffer, 0, buffer.Length);
                        }
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
                            if (tXToolStripMenuItem.Checked)
                            {
                                AppendText("\r\n[" + dt.ToString("yyyy/MM/dd HH:mm:ss") + "][TX]\r\n");
                            }
                            else
                            {
                                AppendText("\r\n[" + dt.ToString("yyyy/MM/dd HH:mm:ss") + "]\r\n");
                            }
                        }
                        else
                        {
                            if (tXToolStripMenuItem.Checked)
                            {
                                AppendText("\r\n[TX]\r\n");
                            }
                        }
                        AppendText(comboBoxCOMMAND.Text + term);

                        int found_index = comboBoxCOMMAND.Items.IndexOf(comboBoxCOMMAND.Text);
                        if (found_index == -1)
                        {
                            // not found means new item
                            // insert at top
                            comboBoxCOMMAND.Items.Insert(0, comboBoxCOMMAND.Text);
                            if (comboBoxCOMMAND.Items.Count > MAX_COMMAND_HISTORY)
                            {
                                // remove the oldest histroy
                                comboBoxCOMMAND.Items.RemoveAt(comboBoxCOMMAND.Items.Count - 1);
                            }
                        }
                        else
                        {
                            // found means already exist
                            // remove the found
                            comboBoxCOMMAND.Items.RemoveAt(found_index);
                            // insert at top
                            comboBoxCOMMAND.Items.Insert(0, comboBoxCOMMAND.Text);
                        }
                        // prepare to resend same command or delete textbox easily.
                        // want to resend -> just push enter key
                        // want to delete -> just push delete or backspace key
                        comboBoxCOMMAND.SelectAll();
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
                        comboBoxCOMMAND.Focus();
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

        private void checkBoxCOMMAND_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCOMMAND.Checked)
            {
                comboBoxTERM.Enabled = false;
            }
            else
            {
                comboBoxTERM.Enabled = true;
            }
        }
    }
}

