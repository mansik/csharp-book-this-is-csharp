namespace UsingControls
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpFont = new GroupBox();
            txtSampleText = new TextBox();
            chkItalic = new CheckBox();
            chkBold = new CheckBox();
            cboFont = new ComboBox();
            lblFont = new Label();
            grpBar = new GroupBox();
            tbDummy = new TrackBar();
            pgDummy = new ProgressBar();
            grpFrom = new GroupBox();
            btnMsgBox = new Button();
            btnModaless = new Button();
            btnModal = new Button();
            grpTreeList = new GroupBox();
            btnAddChild = new Button();
            btnAddRoot = new Button();
            tvDummy = new TreeView();
            lvDummy = new ListView();
            grpFont.SuspendLayout();
            grpBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbDummy).BeginInit();
            grpFrom.SuspendLayout();
            grpTreeList.SuspendLayout();
            SuspendLayout();
            // 
            // grpFont
            // 
            grpFont.Controls.Add(txtSampleText);
            grpFont.Controls.Add(chkItalic);
            grpFont.Controls.Add(chkBold);
            grpFont.Controls.Add(cboFont);
            grpFont.Controls.Add(lblFont);
            grpFont.Location = new Point(28, 16);
            grpFont.Name = "grpFont";
            grpFont.Size = new Size(317, 98);
            grpFont.TabIndex = 0;
            grpFont.TabStop = false;
            grpFont.Text = "ComboBox, CheckBox, TextBox";
            // 
            // txtSampleText
            // 
            txtSampleText.Location = new Point(11, 58);
            txtSampleText.Name = "txtSampleText";
            txtSampleText.Size = new Size(293, 23);
            txtSampleText.TabIndex = 4;
            txtSampleText.Text = "Hello, C#";
            // 
            // chkItalic
            // 
            chkItalic.AutoSize = true;
            chkItalic.Font = new Font("맑은 고딕", 9F, FontStyle.Italic, GraphicsUnit.Point);
            chkItalic.Location = new Point(253, 31);
            chkItalic.Name = "chkItalic";
            chkItalic.Size = new Size(51, 19);
            chkItalic.TabIndex = 3;
            chkItalic.Text = "Italic";
            chkItalic.UseVisualStyleBackColor = true;
            chkItalic.CheckedChanged += chkItalic_CheckedChanged;
            // 
            // chkBold
            // 
            chkBold.AutoSize = true;
            chkBold.Location = new Point(197, 31);
            chkBold.Name = "chkBold";
            chkBold.Size = new Size(50, 19);
            chkBold.TabIndex = 2;
            chkBold.Text = "Bold";
            chkBold.UseVisualStyleBackColor = true;
            chkBold.CheckedChanged += chkBold_CheckedChanged;
            // 
            // cboFont
            // 
            cboFont.FormattingEnabled = true;
            cboFont.Location = new Point(48, 29);
            cboFont.Name = "cboFont";
            cboFont.Size = new Size(143, 23);
            cboFont.TabIndex = 1;
            cboFont.SelectedIndexChanged += cboFont_SelectedIndexChanged;
            // 
            // lblFont
            // 
            lblFont.AutoSize = true;
            lblFont.Location = new Point(11, 33);
            lblFont.Name = "lblFont";
            lblFont.Size = new Size(31, 15);
            lblFont.TabIndex = 0;
            lblFont.Text = "Font";
            // 
            // grpBar
            // 
            grpBar.Controls.Add(tbDummy);
            grpBar.Controls.Add(pgDummy);
            grpBar.Location = new Point(29, 134);
            grpBar.Name = "grpBar";
            grpBar.Size = new Size(317, 110);
            grpBar.TabIndex = 1;
            grpBar.TabStop = false;
            grpBar.Text = "TrackBar && ProgressBar";
            // 
            // tbDummy
            // 
            tbDummy.Location = new Point(14, 22);
            tbDummy.Maximum = 20;
            tbDummy.Name = "tbDummy";
            tbDummy.Size = new Size(290, 45);
            tbDummy.TabIndex = 1;
            tbDummy.Scroll += tbDummy_Scroll;
            // 
            // pgDummy
            // 
            pgDummy.Location = new Point(18, 74);
            pgDummy.Maximum = 20;
            pgDummy.Name = "pgDummy";
            pgDummy.Size = new Size(278, 23);
            pgDummy.TabIndex = 0;
            // 
            // grpFrom
            // 
            grpFrom.Controls.Add(btnMsgBox);
            grpFrom.Controls.Add(btnModaless);
            grpFrom.Controls.Add(btnModal);
            grpFrom.Location = new Point(29, 259);
            grpFrom.Name = "grpFrom";
            grpFrom.Size = new Size(316, 71);
            grpFrom.TabIndex = 2;
            grpFrom.TabStop = false;
            grpFrom.Text = "Modal && Modaless";
            // 
            // btnMsgBox
            // 
            btnMsgBox.Location = new Point(189, 26);
            btnMsgBox.Name = "btnMsgBox";
            btnMsgBox.Size = new Size(107, 31);
            btnMsgBox.TabIndex = 2;
            btnMsgBox.Text = "MessageBox";
            btnMsgBox.UseVisualStyleBackColor = true;
            btnMsgBox.Click += btnMsgBox_Click;
            // 
            // btnModaless
            // 
            btnModaless.Location = new Point(103, 26);
            btnModaless.Name = "btnModaless";
            btnModaless.Size = new Size(80, 31);
            btnModaless.TabIndex = 1;
            btnModaless.Text = "Modaless";
            btnModaless.UseVisualStyleBackColor = true;
            btnModaless.Click += btnModaless_Click;
            // 
            // btnModal
            // 
            btnModal.Location = new Point(17, 26);
            btnModal.Name = "btnModal";
            btnModal.Size = new Size(80, 31);
            btnModal.TabIndex = 0;
            btnModal.Text = "Modal";
            btnModal.UseVisualStyleBackColor = true;
            btnModal.Click += btnModal_Click;
            // 
            // grpTreeList
            // 
            grpTreeList.Controls.Add(btnAddChild);
            grpTreeList.Controls.Add(btnAddRoot);
            grpTreeList.Controls.Add(tvDummy);
            grpTreeList.Controls.Add(lvDummy);
            grpTreeList.Location = new Point(28, 346);
            grpTreeList.Name = "grpTreeList";
            grpTreeList.Size = new Size(318, 254);
            grpTreeList.TabIndex = 3;
            grpTreeList.TabStop = false;
            grpTreeList.Text = "TreeView && ListView";
            // 
            // btnAddChild
            // 
            btnAddChild.Location = new Point(87, 225);
            btnAddChild.Name = "btnAddChild";
            btnAddChild.Size = new Size(75, 25);
            btnAddChild.TabIndex = 3;
            btnAddChild.Text = "자식 추가";
            btnAddChild.UseVisualStyleBackColor = true;
            btnAddChild.Click += btnAddChild_Click;
            // 
            // btnAddRoot
            // 
            btnAddRoot.Location = new Point(6, 225);
            btnAddRoot.Name = "btnAddRoot";
            btnAddRoot.Size = new Size(75, 25);
            btnAddRoot.TabIndex = 2;
            btnAddRoot.Text = "루트 추가";
            btnAddRoot.UseVisualStyleBackColor = true;
            btnAddRoot.Click += btnAddRoot_Click;
            // 
            // tvDummy
            // 
            tvDummy.Location = new Point(6, 26);
            tvDummy.Name = "tvDummy";
            tvDummy.Size = new Size(151, 193);
            tvDummy.TabIndex = 1;
            // 
            // lvDummy
            // 
            lvDummy.Location = new Point(164, 26);
            lvDummy.Name = "lvDummy";
            lvDummy.Size = new Size(146, 193);
            lvDummy.TabIndex = 0;
            lvDummy.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(372, 605);
            Controls.Add(grpTreeList);
            Controls.Add(grpFrom);
            Controls.Add(grpBar);
            Controls.Add(grpFont);
            Name = "MainForm";
            Text = "Control Test";
            Load += MainForm_Load;
            grpFont.ResumeLayout(false);
            grpFont.PerformLayout();
            grpBar.ResumeLayout(false);
            grpBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbDummy).EndInit();
            grpFrom.ResumeLayout(false);
            grpTreeList.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpFont;
        private TextBox txtSampleText;
        private CheckBox chkItalic;
        private CheckBox chkBold;
        private ComboBox cboFont;
        private Label lblFont;
        private GroupBox grpBar;
        private TrackBar tbDummy;
        private ProgressBar pgDummy;
        private GroupBox grpFrom;
        private Button btnMsgBox;
        private Button btnModaless;
        private Button btnModal;
        private GroupBox grpTreeList;
        private Button btnAddChild;
        private Button btnAddRoot;
        private TreeView tvDummy;
        private ListView lvDummy;
    }
}