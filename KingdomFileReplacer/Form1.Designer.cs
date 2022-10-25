namespace KingdomFileReplacer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.InstructionButton = new System.Windows.Forms.Button();
            this.ModeComboBox = new System.Windows.Forms.ComboBox();
            this.SelectGameFileButton = new System.Windows.Forms.Button();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.SelectedGameFileLabel = new System.Windows.Forms.Label();
            this.SelectedFolderLabel = new System.Windows.Forms.Label();
            this.SelectFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SelectGameFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.InstructionButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ModeComboBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.SelectGameFileButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.SelectFolderButton, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.ApplyButton, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.SelectedGameFileLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.SelectedFolderLabel, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.77778F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.77778F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.77778F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(864, 797);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // InstructionButton
            // 
            this.InstructionButton.AutoSize = true;
            this.InstructionButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InstructionButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InstructionButton.Location = new System.Drawing.Point(3, 3);
            this.InstructionButton.Name = "InstructionButton";
            this.InstructionButton.Size = new System.Drawing.Size(858, 100);
            this.InstructionButton.TabIndex = 2;
            this.InstructionButton.Text = "Instructions";
            this.InstructionButton.UseVisualStyleBackColor = true;
            this.InstructionButton.Click += new System.EventHandler(this.InstructionButton_Click);
            // 
            // ModeComboBox
            // 
            this.ModeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModeComboBox.FormattingEnabled = true;
            this.ModeComboBox.Items.AddRange(new object[] {
            "GAME.PAC",
            "Sounds",
            "main.dol",
            "STAGEBASE.DAT",
            "Any"});
            this.ModeComboBox.Location = new System.Drawing.Point(3, 334);
            this.ModeComboBox.Name = "ModeComboBox";
            this.ModeComboBox.Size = new System.Drawing.Size(858, 56);
            this.ModeComboBox.TabIndex = 1;
            this.ModeComboBox.Text = "Select File/s to Replace";
            // 
            // SelectGameFileButton
            // 
            this.SelectGameFileButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SelectGameFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectGameFileButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SelectGameFileButton.Location = new System.Drawing.Point(3, 109);
            this.SelectGameFileButton.Name = "SelectGameFileButton";
            this.SelectGameFileButton.Size = new System.Drawing.Size(858, 171);
            this.SelectGameFileButton.TabIndex = 3;
            this.SelectGameFileButton.Text = "Select Game File";
            this.SelectGameFileButton.UseVisualStyleBackColor = true;
            this.SelectGameFileButton.Click += new System.EventHandler(this.SelectGameFileButton_Click);
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.AutoSize = true;
            this.SelectFolderButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SelectFolderButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectFolderButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SelectFolderButton.Location = new System.Drawing.Point(3, 396);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(858, 171);
            this.SelectFolderButton.TabIndex = 4;
            this.SelectFolderButton.Text = "Select Folder";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // ApplyButton
            // 
            this.ApplyButton.AutoSize = true;
            this.ApplyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ApplyButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ApplyButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ApplyButton.Location = new System.Drawing.Point(3, 621);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(858, 173);
            this.ApplyButton.TabIndex = 5;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // SelectedGameFileLabel
            // 
            this.SelectedGameFileLabel.AutoSize = true;
            this.SelectedGameFileLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedGameFileLabel.Location = new System.Drawing.Point(3, 283);
            this.SelectedGameFileLabel.Name = "SelectedGameFileLabel";
            this.SelectedGameFileLabel.Size = new System.Drawing.Size(858, 48);
            this.SelectedGameFileLabel.TabIndex = 6;
            this.SelectedGameFileLabel.Text = "File: ";
            this.SelectedGameFileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SelectedFolderLabel
            // 
            this.SelectedFolderLabel.AutoSize = true;
            this.SelectedFolderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedFolderLabel.Location = new System.Drawing.Point(3, 570);
            this.SelectedFolderLabel.Name = "SelectedFolderLabel";
            this.SelectedFolderLabel.Size = new System.Drawing.Size(858, 48);
            this.SelectedFolderLabel.TabIndex = 7;
            this.SelectedFolderLabel.Text = "Folder: ";
            this.SelectedFolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SelectGameFileDialog
            // 
            this.SelectGameFileDialog.Filter = "Wii Game Files|*.iso;*.wbfs|All Files|*.*";
            // 
            // Form1
            this.FormClosing += Form1_FormClosing;
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(20F, 48F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(864, 797);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Kingdom File Replacer";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_FormClosing1(object sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox ModeComboBox;
        private Button InstructionButton;
        private Button SelectGameFileButton;
        private Button SelectFolderButton;
        private Button ApplyButton;
        private FolderBrowserDialog SelectFolderDialog;
        private OpenFileDialog SelectGameFileDialog;
        private Label SelectedGameFileLabel;
        private Label SelectedFolderLabel;
    }
}