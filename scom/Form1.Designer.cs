namespace scom
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.comboBoxCOM = new System.Windows.Forms.ComboBox();
            this.comboBoxBAUD = new System.Windows.Forms.ComboBox();
            this.comboBoxFRAME = new System.Windows.Forms.ComboBox();
            this.buttonCONNECT = new System.Windows.Forms.Button();
            this.textBoxCOMMAND = new System.Windows.Forms.TextBox();
            this.richTextBoxSCREEN = new System.Windows.Forms.RichTextBox();
            this.comboBoxTERM = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aSCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shiftJISToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uTF8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.identifierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeStampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // comboBoxCOM
            // 
            this.comboBoxCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCOM.FormattingEnabled = true;
            this.comboBoxCOM.Location = new System.Drawing.Point(13, 39);
            this.comboBoxCOM.Name = "comboBoxCOM";
            this.comboBoxCOM.Size = new System.Drawing.Size(80, 20);
            this.comboBoxCOM.TabIndex = 1;
            this.comboBoxCOM.DropDown += new System.EventHandler(this.comboBoxCOM_DropDown);
            // 
            // comboBoxBAUD
            // 
            this.comboBoxBAUD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBAUD.FormattingEnabled = true;
            this.comboBoxBAUD.Location = new System.Drawing.Point(99, 39);
            this.comboBoxBAUD.Name = "comboBoxBAUD";
            this.comboBoxBAUD.Size = new System.Drawing.Size(80, 20);
            this.comboBoxBAUD.TabIndex = 2;
            this.comboBoxBAUD.SelectedIndexChanged += new System.EventHandler(this.comboBoxBAUD_SelectedIndexChanged);
            // 
            // comboBoxFRAME
            // 
            this.comboBoxFRAME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFRAME.FormattingEnabled = true;
            this.comboBoxFRAME.Location = new System.Drawing.Point(185, 39);
            this.comboBoxFRAME.Name = "comboBoxFRAME";
            this.comboBoxFRAME.Size = new System.Drawing.Size(80, 20);
            this.comboBoxFRAME.TabIndex = 3;
            this.comboBoxFRAME.SelectedIndexChanged += new System.EventHandler(this.comboBoxFRAME_SelectedIndexChanged);
            // 
            // buttonCONNECT
            // 
            this.buttonCONNECT.Location = new System.Drawing.Point(271, 37);
            this.buttonCONNECT.Name = "buttonCONNECT";
            this.buttonCONNECT.Size = new System.Drawing.Size(144, 23);
            this.buttonCONNECT.TabIndex = 4;
            this.buttonCONNECT.Text = "connect";
            this.buttonCONNECT.UseVisualStyleBackColor = true;
            this.buttonCONNECT.Click += new System.EventHandler(this.buttonCONNECT_Click);
            // 
            // textBoxCOMMAND
            // 
            this.textBoxCOMMAND.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCOMMAND.Location = new System.Drawing.Point(13, 404);
            this.textBoxCOMMAND.Name = "textBoxCOMMAND";
            this.textBoxCOMMAND.Size = new System.Drawing.Size(333, 19);
            this.textBoxCOMMAND.TabIndex = 5;
            this.textBoxCOMMAND.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCOMMAND_KeyPress);
            // 
            // richTextBoxSCREEN
            // 
            this.richTextBoxSCREEN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxSCREEN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.richTextBoxSCREEN.Location = new System.Drawing.Point(13, 65);
            this.richTextBoxSCREEN.Name = "richTextBoxSCREEN";
            this.richTextBoxSCREEN.ReadOnly = true;
            this.richTextBoxSCREEN.Size = new System.Drawing.Size(402, 333);
            this.richTextBoxSCREEN.TabIndex = 6;
            this.richTextBoxSCREEN.Text = "";
            // 
            // comboBoxTERM
            // 
            this.comboBoxTERM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTERM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTERM.FormattingEnabled = true;
            this.comboBoxTERM.Location = new System.Drawing.Point(352, 404);
            this.comboBoxTERM.Name = "comboBoxTERM";
            this.comboBoxTERM.Size = new System.Drawing.Size(63, 20);
            this.comboBoxTERM.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.configToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(427, 26);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transferToolStripMenuItem,
            this.logToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // transferToolStripMenuItem
            // 
            this.transferToolStripMenuItem.Name = "transferToolStripMenuItem";
            this.transferToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.transferToolStripMenuItem.Text = "Transfer[text]";
            this.transferToolStripMenuItem.Click += new System.EventHandler(this.transferToolStripMenuItem_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.logToolStripMenuItem.Text = "Log";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.logToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encodeToolStripMenuItem,
            this.identifierToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(60, 22);
            this.configToolStripMenuItem.Text = "Screen";
            // 
            // encodeToolStripMenuItem
            // 
            this.encodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aSCIIToolStripMenuItem,
            this.shiftJISToolStripMenuItem,
            this.uTF8ToolStripMenuItem});
            this.encodeToolStripMenuItem.Name = "encodeToolStripMenuItem";
            this.encodeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.encodeToolStripMenuItem.Text = "Encode";
            // 
            // aSCIIToolStripMenuItem
            // 
            this.aSCIIToolStripMenuItem.Name = "aSCIIToolStripMenuItem";
            this.aSCIIToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.aSCIIToolStripMenuItem.Text = "ASCII";
            this.aSCIIToolStripMenuItem.Click += new System.EventHandler(this.aSCIIToolStripMenuItem_Click);
            // 
            // shiftJISToolStripMenuItem
            // 
            this.shiftJISToolStripMenuItem.Name = "shiftJISToolStripMenuItem";
            this.shiftJISToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.shiftJISToolStripMenuItem.Text = "ShiftJIS";
            this.shiftJISToolStripMenuItem.Click += new System.EventHandler(this.shiftJISToolStripMenuItem_Click);
            // 
            // uTF8ToolStripMenuItem
            // 
            this.uTF8ToolStripMenuItem.Checked = true;
            this.uTF8ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uTF8ToolStripMenuItem.Name = "uTF8ToolStripMenuItem";
            this.uTF8ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.uTF8ToolStripMenuItem.Text = "UTF-8";
            this.uTF8ToolStripMenuItem.Click += new System.EventHandler(this.uTF8ToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(46, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.versionToolStripMenuItem.Text = "Version";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(119, 22);
            this.toolStripMenuItem2.Text = "19.5.30";
            // 
            // identifierToolStripMenuItem
            // 
            this.identifierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tXToolStripMenuItem,
            this.rXToolStripMenuItem,
            this.timeStampToolStripMenuItem});
            this.identifierToolStripMenuItem.Name = "identifierToolStripMenuItem";
            this.identifierToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.identifierToolStripMenuItem.Text = "Identifier";
            // 
            // tXToolStripMenuItem
            // 
            this.tXToolStripMenuItem.Name = "tXToolStripMenuItem";
            this.tXToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tXToolStripMenuItem.Text = "TX";
            this.tXToolStripMenuItem.Click += new System.EventHandler(this.tXToolStripMenuItem_Click);
            // 
            // rXToolStripMenuItem
            // 
            this.rXToolStripMenuItem.Name = "rXToolStripMenuItem";
            this.rXToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rXToolStripMenuItem.Text = "RX";
            this.rXToolStripMenuItem.Click += new System.EventHandler(this.rXToolStripMenuItem_Click);
            // 
            // timeStampToolStripMenuItem
            // 
            this.timeStampToolStripMenuItem.Name = "timeStampToolStripMenuItem";
            this.timeStampToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.timeStampToolStripMenuItem.Text = "TimeStamp";
            this.timeStampToolStripMenuItem.Click += new System.EventHandler(this.timeStampToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 435);
            this.Controls.Add(this.comboBoxTERM);
            this.Controls.Add(this.richTextBoxSCREEN);
            this.Controls.Add(this.textBoxCOMMAND);
            this.Controls.Add(this.buttonCONNECT);
            this.Controls.Add(this.comboBoxFRAME);
            this.Controls.Add(this.comboBoxBAUD);
            this.Controls.Add(this.comboBoxCOM);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox comboBoxCOM;
        private System.Windows.Forms.ComboBox comboBoxBAUD;
        private System.Windows.Forms.ComboBox comboBoxFRAME;
        private System.Windows.Forms.Button buttonCONNECT;
        private System.Windows.Forms.TextBox textBoxCOMMAND;
        private System.Windows.Forms.RichTextBox richTextBoxSCREEN;
        private System.Windows.Forms.ComboBox comboBoxTERM;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aSCIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shiftJISToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uTF8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem transferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem identifierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timeStampToolStripMenuItem;
    }
}

